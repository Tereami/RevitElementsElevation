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
            //TaskDialog.Show("Проверка", "Запуск работы отметок отверстий");

            Document doc = commandData.Application.ActiveUIDocument.Document;

            Config cfg = Config.Activate(false);
            if (cfg == null) return Result.Cancelled;


            List<FamilyInstance> fams = new FilteredElementCollector(doc).OfClass(typeof(FamilyInstance)).Cast<FamilyInstance>().ToList();

            List<FamilyInstance> famsHoles = new List<FamilyInstance>();

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

            if (famsHoles.Count == 0)
            {
                TaskDialog.Show("Holes elevation", "Семейства не найдены. Проверьте настройки");
                return Result.Failed;
            }
            else
            {
                using (Transaction t = new Transaction(doc))
                {
                    t.Start("Transaction");

                    foreach (FamilyInstance fi in famsHoles)
                    {
                        baseLevel = LevelUtils.GetBaseLevelofElement(fi);

                        if (baseLevel != null) //обычные семейства на основе стены
                        {
                            elev = LevelUtils.GetOffsetFromLevel(fi);
                        }
                        else //семейства без основы - худший вариант; у семейства нет ни уровня, ни основы. ищу ближайший уровень через координаты
                        {
                            LocationPoint lp = fi.Location as LocationPoint;
                            if (lp == null) continue;

                            XYZ point = lp.Point;
                            baseLevel = LevelUtils.GetNearestLevel(point, doc, projectPointElevation);

                            if (baseLevel == null)
                            {
                                message += "Не удалось получить уровень для элемента " + fi.Name + " id " + fi.Id ;
                                message += " на отметке " + (point.Z * 304.8).ToString("F0");
                                elements.Insert(fi);
                            }

                            elev = point.Z - baseLevel.Elevation - projectPointElevation;
                        }

                        fi.LookupParameter(cfg.paramElevOnLevel).Set(elev);

                        fi.LookupParameter(cfg.paramBaseLevel).Set(baseLevel.Elevation);
                        count++;
                    }
                    t.Commit();
                }
            }

            TaskDialog.Show("Holes elevation", "Найдено семейств: " + famsHoles.Count.ToString()
                + "\nОбработано семейств: " + count
                + "\nНе удалось обработать: " + err);

            cfg.Save();
            return Result.Succeeded;
        }
    }
}
