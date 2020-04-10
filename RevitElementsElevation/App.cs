﻿#region License
/*Данный код опубликован под лицензией Creative Commons Attribution-NonCommercial-ShareAlike.
Разрешено использовать, распространять, изменять и брать данный код за основу для производных в некоммерческих целях,
при условии указания авторства и если производные лицензируются на тех же условиях.
Код поставляется "как есть". Автор не несет ответственности за возможные последствия использования.
Зуев Александр, 2020, все права защищены.
This code is listed under the Creative Commons Attribution-NonCommercial-ShareAlike license.
You may use, redistribute, remix, tweak, and build upon this work non-commercially,
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
using Autodesk.Revit.ApplicationServices;
#endregion

namespace RevitElementsElevation
{
    public class App : IExternalApplication
    {
        public static string assemblyPath = "";
        public static string assemblyFolder = "";
        public Result OnStartup(UIControlledApplication application)
        {
            assemblyPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            assemblyFolder = System.IO.Path.GetDirectoryName(assemblyPath);

            string tabName = "Weandrevit";
            string solutionName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            try { application.CreateRibbonTab(tabName); } catch { }
            RibbonPanel panel1 = application.CreateRibbonPanel(tabName, solutionName);

            string btn1name = solutionName + ".Command";
            PushButton btn = panel1.AddItem(new PushButtonData(
                btn1name,
                btn1name,
                assemblyPath,
                btn1name)
                ) as PushButton;

            string btn2name = solutionName + ".CommandConfig";
            PushButton btn2 = panel1.AddItem(new PushButtonData(
                btn2name,
                btn2name,
                assemblyPath,
                btn2name)
                ) as PushButton;


            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

    }
}
