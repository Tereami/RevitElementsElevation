#region License
/*Данный код опубликован под лицензией Creative Commons Attribution-ShareAlike.
Разрешено использовать, распространять, изменять и брать данный код за основу для производных в коммерческих и
некоммерческих целях, при условии указания авторства и если производные лицензируются на тех же условиях.
Код поставляется "как есть". Автор не несет ответственности за возможные последствия использования.
Зуев Александр, 2020, все права защищены.
This code is listed under the Creative Commons Attribution-ShareAlike license.
You may use, redistribute, remix, tweak, and build upon this work non-commercially and commercially,
as long as you credit the author by linking back and license your new creations under the same terms.
This code is provided 'as is'. Author disclaims any implied warranty.
Zuev Aleksandr, 2020, all rigths reserved.*/
#endregion
#region Usings
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
#endregion

namespace RevitElementsElevation
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        Result IExternalCommand.Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Debug.Listeners.Clear();
            Debug.Listeners.Add(new RbsLogger.Logger("ElementsElevation"));

            Document doc = commandData.Application.ActiveUIDocument.Document;

            Config cfg = Config.Activate(false);
            if (cfg == null)
            {
                Debug.WriteLine("Unable to read config xml file");
                return Result.Cancelled;
            }

            List<FamilyInstance> fams = new FilteredElementCollector(doc).OfClass(typeof(FamilyInstance)).Cast<FamilyInstance>().ToList();

            List<FamilyInstance> famsHoles = new List<FamilyInstance>();

            List<Element> ColumnsAndWalls = new FilteredElementCollector(doc)
                .WhereElementIsNotElementType()
                .OfClass(typeof(FamilyInstance))
                .OfCategory(BuiltInCategory.OST_StructuralColumns)
                .ToList();
            ColumnsAndWalls.AddRange(new FilteredElementCollector(doc).OfClass(typeof(Wall)).ToList());
            Debug.WriteLine("Columns and wall found: " + ColumnsAndWalls.Count);


            int ColumnAndWallsCount = 0;

            Parameter paramBaseLevelElev;
            Parameter paramElevOnLevel;
            foreach (FamilyInstance fi in fams)
            {
                string famname = fi.Symbol.FamilyName;
                if (string.IsNullOrEmpty(famname)) continue;
                int prefixLength = cfg.namePrefixes.First().Length;
                if (famname.Length <= prefixLength) continue;
                string firstSymbols = famname.Substring(0, prefixLength);
                if (cfg.namePrefixes.Contains(firstSymbols))
                {
                    paramBaseLevelElev = fi.LookupParameter(cfg.paramBaseLevel);
                    paramElevOnLevel = fi.LookupParameter(cfg.paramElevOnLevel);

                    if (paramBaseLevelElev != null && paramElevOnLevel != null)
                    {
                        famsHoles.Add(fi);
                    }
                }
            }
            Debug.WriteLine("Holes found: " + famsHoles.Count);

            Level baseLevel = null;
            double elev = 0;
            int count = 0;
            int err = 0;

            BasePoint projectBasePoint = new FilteredElementCollector(doc)
                .OfClass(typeof(BasePoint))
                .WhereElementIsNotElementType()
                .Cast<BasePoint>()
                .Where(i => i.IsShared == false)
                .First();
            double projectPointElevation = projectBasePoint.get_BoundingBox(null).Min.Z;
            Debug.WriteLine("Project base point elevation: " + (projectPointElevation * 304.8).ToString());

            using (Transaction t = new Transaction(doc))
            {
                t.Start("Transaction");
                if (famsHoles.Count == 0 && ColumnsAndWalls.Count == 0)
                {
                    TaskDialog.Show("Holes elevation", "Семейства не найдены. Проверьте настройки");
                    return Result.Failed;
                }
                else
                {
                    foreach (FamilyInstance fi in famsHoles)
                    {
                        Debug.WriteLine("Current family: " + fi.Id.IntegerValue);
                        baseLevel = LevelUtils.GetBaseLevelofElement(fi);

                        if (baseLevel != null) //обычные семейства на основе стены
                        {
                            elev = LevelUtils.GetOffsetFromLevel(fi);

                        }
                        else //семейства без основы - худший вариант; у семейства нет ни уровня, ни основы. ищу ближайший уровень через координаты
                        {
                            Debug.WriteLine("Family is without base level!");
                            LocationPoint lp = fi.Location as LocationPoint;
                            if (lp == null)
                            {
                                Debug.WriteLine("No location point");
                                continue;
                            }

                            XYZ point = lp.Point;
                            Debug.WriteLine("Location point: " + point.ToString());
                            baseLevel = LevelUtils.GetNearestLevel(point, doc, projectPointElevation);


                            if (baseLevel == null)
                            {
                                message += "Не удалось получить уровень для элемента " + fi.Name + " id " + fi.Id;
                                message += " на отметке " + (point.Z * 304.8).ToString("F0");
                                Debug.WriteLine("Unable to get level. " + message);
                                elements.Insert(fi);
                            }
                            elev = point.Z - baseLevel.Elevation - projectPointElevation;
                            Debug.WriteLine("Nearest level id: " + baseLevel.Id.IntegerValue + ", elev: " + elev.ToString("F1"));
                        }

                        GetParameter(fi, cfg.paramElevOnLevel, true).Set(elev);
                        GetParameter(fi, cfg.paramBaseLevel, true).Set(baseLevel.Elevation);
                        count++;
                    }
                }
                Debug.WriteLine("Families done: " + count);

                if (cfg.useWallAndColumns)
                {
                    Debug.WriteLine("Start wall and columns processing");
                    foreach (Element elem in ColumnsAndWalls)
                    {
                        Debug.WriteLine("Current elem id: " + elem.Id.IntegerValue);
                        Parameter baseLevelParam = null;
                        Parameter baseOffsetParam = null;
                        Parameter topLevelParam = null;
                        Parameter topOffsetParam = null;

                        if (elem is FamilyInstance)
                        {
                            Debug.WriteLine("It is column");
                            FamilyInstance col = elem as FamilyInstance;
                            baseLevelParam = col.get_Parameter(BuiltInParameter.SCHEDULE_BASE_LEVEL_PARAM);
                            baseOffsetParam = col.get_Parameter(BuiltInParameter.SCHEDULE_BASE_LEVEL_OFFSET_PARAM);
                            topLevelParam = col.get_Parameter(BuiltInParameter.SCHEDULE_TOP_LEVEL_PARAM);
                            topOffsetParam = col.get_Parameter(BuiltInParameter.SCHEDULE_TOP_LEVEL_OFFSET_PARAM);
                        }
                        else if (elem is Wall)
                        {
                            Debug.WriteLine("It is wall");
                            Wall w = elem as Wall;
                            baseLevelParam = w.get_Parameter(BuiltInParameter.WALL_BASE_CONSTRAINT);
                            baseOffsetParam = w.get_Parameter(BuiltInParameter.WALL_BASE_OFFSET);
                            topLevelParam = w.get_Parameter(BuiltInParameter.WALL_HEIGHT_TYPE);
                            topOffsetParam = w.get_Parameter(BuiltInParameter.WALL_TOP_OFFSET);
                        }

                        if (baseLevelParam == null || baseOffsetParam == null || topLevelParam == null || topOffsetParam == null)
                        {
                            Debug.WriteLine("Incorrect level or height parameters");
                            continue;
                        }

                        Level baseLev = doc.GetElement(baseLevelParam.AsElementId()) as Level;
                        if (baseLev == null)
                        {
                            Debug.WriteLine("No base level");
                            continue;
                        }
                        double baseLevElev = baseLev.Elevation;
                        double baseOffset = baseOffsetParam.AsDouble();
                        double baseElev = baseLevElev + baseOffset;

                        Level topLev = doc.GetElement(topLevelParam.AsElementId()) as Level;
                        double topElev = 0;
                        if (topLev == null)
                        {
                            if (elem is Wall)
                            {
                                double heigth = elem.get_Parameter(BuiltInParameter.WALL_USER_HEIGHT_PARAM).AsDouble();
                                topElev = baseElev + heigth;
                            }
                            else
                                continue;
                        }
                        else
                        {
                            double topLevElev = topLev.Elevation;
                            double topOffset = topOffsetParam.AsDouble();
                            topElev = topLevElev + topOffset;
                        }

                        SetElevParamValue(elem, baseElev, cfg.paramBottomElevName, cfg);
                        SetElevParamValue(elem, topElev, cfg.paramTopElevName, cfg);

                        ColumnAndWallsCount++;
                        Debug.WriteLine("Walls and columns done: " + ColumnAndWallsCount);

                        //заполняю сокращенную марку
                        //Parameter shortMarkParam = elem.LookupParameter("МаркаСокращенная");
                        //if (shortMarkParam == null) continue;
                        //Parameter markParam = elem.get_Parameter(BuiltInParameter.ALL_MODEL_MARK);
                        //if (markParam == null) continue;
                        //if (!markParam.HasValue) continue;
                        //string mark = markParam.AsString();
                        //if (string.IsNullOrEmpty(mark)) continue;
                        //string[] markArray = mark.Split('-');
                        //if (markArray.Length < 3) continue;
                        //string shortMark = markArray[0] + "-" + markArray[2];
                        //shortMarkParam.Set(shortMark);
                    }
                }

                t.Commit();
            }

            string msg = "Найдено проемов и отверстий: " + famsHoles.Count.ToString()
                + "\nОбработано семейств: " + count
                + "\nНе удалось обработать: " + err;
            if (cfg.useWallAndColumns)
            {
                msg += "\nЕще обработано колонн и стен: " + ColumnAndWallsCount;
            }
            Debug.WriteLine(msg);
            BalloonTip.Show("Holes elevation", msg);

            cfg.Save();
            return Result.Succeeded;
        }

        private Parameter GetParameter(Element elem, string paramname, bool checkForWritable = false)
        {
            Parameter param = elem.LookupParameter(paramname);
            if (param == null)
            {
                ElementType etype = elem.Document.GetElement(elem.GetTypeId()) as ElementType;
                param = etype.LookupParameter(paramname);
                if (param == null)
                {
                    Debug.WriteLine("No parameter: " + paramname);
                }
            }
            if (checkForWritable && param != null && param.IsReadOnly)
            {
                Debug.WriteLine("Parameter is readonly: " + paramname);
            }
            return param;
        }

        private bool SetElevParamValue(Element elem, double elevation, string paramName, Config cnf)
        {
            Parameter userParam = GetParameter(elem, paramName);
            if (userParam == null)
            {
                Debug.WriteLine("Нет параметра " + paramName + " в элементе " + elem.Id.IntegerValue);
                return false;
            }
            if (userParam.IsReadOnly)
            {
                Debug.WriteLine("Невозможно заполнить " + paramName + " в элементе " + elem.Id.IntegerValue);
                return false;
            }

            double elevMm = elevation * 304.8;

            if (cnf.elevIsCurrency)
                userParam.Set(elevMm);
            else
                userParam.Set(elevation);

            Debug.WriteLine(paramName + " = " + elevMm.ToString("F2"));
            return true;
        }
    }
}
