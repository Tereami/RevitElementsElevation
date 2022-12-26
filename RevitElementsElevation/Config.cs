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
using System.IO;
using System.Xml.Serialization;
#endregion

namespace RevitElementsElevation
{
    [Serializable]
    public class Config
    {

        public string paramBaseLevel = MyStrings.ParameterBaseLevelElev;
        public string paramElevOnLevel = MyStrings.ParameterElevFromLevel;

        public bool useWallAndColumns = true;

        public string paramTopElevName = MyStrings.ParameterTopElev;
        public string paramBottomElevName = MyStrings.ParameterBottomElev;
        
        public bool elevIsCurrency = false;

        private static string configPath;

        public List<string> namePrefixes;
        public void setStandardPrefixes()
        {
            namePrefixes = new List<string> { "230_", "231_", "232_", "235_" };
        }

        public Config()
        {

        }

        public static Config Activate(bool forceShowWindow)
        {
            string appdataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string rbspath = Path.Combine(appdataPath, "bim-starter");
            if (!Directory.Exists(rbspath)) Directory.CreateDirectory(rbspath);
            string solutionName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            string localFolder = Path.Combine(rbspath, solutionName);
            if (!Directory.Exists(localFolder)) Directory.CreateDirectory(localFolder);
            configPath = Path.Combine(localFolder, "config.xml");

            XmlSerializer serializer = new XmlSerializer(typeof(Config));
            Config cfg = null;
            bool checkCfgFile = File.Exists(configPath);
            if (checkCfgFile)
            {
                using (StreamReader reader = new StreamReader(configPath))
                {
                    try
                    {
                        cfg = (Config)serializer.Deserialize(reader);
                    }
                    catch
                    {
                        cfg = new Config();
                    }
                    if (cfg == null)
                    {
                        throw new Exception("Failed to serialize: " + configPath);
                    }
                }
            }
            else
            {
                cfg = new Config();
            }

            if (cfg != null && (cfg.namePrefixes == null || cfg.namePrefixes.Count == 0))
            {
                cfg.setStandardPrefixes();

            }

            if (!checkCfgFile || forceShowWindow)
            {
                using (FormConfig form = new FormConfig(ref cfg))
                {
                    if (form.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                        return null;
                }
            }

            return cfg;
        }

        public void Save()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Config));
            if (File.Exists(configPath)) File.Delete(configPath);
            using (FileStream writer = new FileStream(configPath, FileMode.OpenOrCreate))
            {
                serializer.Serialize(writer, this);
            }
        }
    }
}
