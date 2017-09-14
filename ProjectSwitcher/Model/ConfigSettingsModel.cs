using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WCF.UTILS;

namespace ProjectSwitcher.Model
{
    [Serializable]
    public class ConfigSettingsModel : Core.NotifyProperty
    {
        public const string DefaultConfigName = "Введите название";
        private string configName = DefaultConfigName;
        /// <summary>
        /// Название конфига
        /// </summary>
        public string ConfigName
        {
            get { return configName; }
            set { configName = value; }
        }
        

        private Config cfg;
        [XmlIgnore]
        public Config CurrentConfig
        {
            get
            {
                return cfg ?? (cfg = new Config(WCF.UTILS.Config.Instance));
            }
            set
            {
                cfg = value;
            }
        }

        private string path = "";

        public string Path
        {
            get
            {
                return path;
            }
            set
            {
                path = value;
                FilesToCopyList.Clear();
                foreach (var file in System.IO.Directory.EnumerateFiles(path))
                {
                    System.IO.FileInfo fi = new System.IO.FileInfo(file);

                    FilesToCopyList.Add(new FilesToCopy(fi));
                }
                OnPropertyChanged(nameof(Path));
            }
        }


        private ObservableCollection<FilesToCopy> filePath;

        public ObservableCollection<FilesToCopy> FilesToCopyList
        {
            get { return filePath ?? (filePath = new ObservableCollection<FilesToCopy>()); }
            set
            {
                filePath = value;
                OnPropertyChanged(nameof(FilesToCopyList));
            }
        }


    }
}
