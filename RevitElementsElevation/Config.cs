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
        public string paramBaseLevel;
        public string paramElevOnLevel;

        public List<string> namePrefixes;

        private static string configPath;

        public Config()
        {

        }

        public static Config Activate(bool forceShowWindow)
        {
            string programdataPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            string rbspath = Path.Combine(programdataPath, "RibbonBimStarter");
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
                        cfg = Config.GetDefault();
                    }
                    if (cfg == null)
                    {
                        throw new Exception("Не удалось сериализовать: " + configPath);
                    }
                }
            }
            else
            {
                cfg = Config.GetDefault();
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



        public static Config GetDefault()
        {
            Config es = new Config();
            es.namePrefixes = new List<string> { "230_", "231_", "232_", "235_" };
            es.paramBaseLevel = "Рзм.ВысотаБазовогоУровня";
            es.paramElevOnLevel = "Рзм.СмещениеОтУровня";
            return es;
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
