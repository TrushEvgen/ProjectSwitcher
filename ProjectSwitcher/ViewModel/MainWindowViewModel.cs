using Microsoft.Win32;
using ProjectSwitcher.Core;
using ProjectSwitcher.Model;
using ProjectSwitcher.StatePattern;
using ProjectSwitcher.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WCF.UTILS;

namespace ProjectSwitcher.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        public const string ConfigPath = "ProjectSwitcherConfig.xml";

        public MainWindowViewModel()
        {
            LoadConfigs();        
            //_observalCollectionConfigs = new ObservableCollection<ConfigSettingsModel>()
            //{
            //    new ConfigSettingsModel() {ConfigName = "TeST1", CurrentConfig = new Model.Config() },
            //    new ConfigSettingsModel() {ConfigName = "Work1", CurrentConfig = new Model.Config(WCF.UTILS.Config.Instance) }
            //};
            
            //var clone = new PropDispNameWrapper(WCF.UTILS.Config.GetClone());
            RegistryKey regSessionKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\System Technologies\Insurance2", false);
            string configFilePath = regSessionKey.GetValue("WCFConfigPath", 0).ToString();
            //curConfig.Save();            
        }

        private void LoadConfigs()
        {
            if (File.Exists(ConfigPath))
            {
                using (FileStream fs =
                                  new FileStream(ConfigPath, FileMode.Open))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(ObservableCollection<ConfigSettingsModel>));
                    Configs = (ObservableCollection<ConfigSettingsModel>)xs.Deserialize(fs);
                }
            }
            else
            {
                Configs = new ObservableCollection<ConfigSettingsModel>();
                var newConfig = new ConfigSettingsModel() { ConfigName = "Default", CurrentConfig = new Model.Config(WCF.UTILS.Config.Instance) };
                Configs.Add(newConfig);
                SelectedConfig = newConfig;
            }
        }


        #region Описание конфигов


        private ObservableCollection<ConfigSettingsModel> _observalCollectionConfigs;
        public ObservableCollection<ConfigSettingsModel> Configs
        {
            get
            {
                return _observalCollectionConfigs ?? (_observalCollectionConfigs = new ObservableCollection<ConfigSettingsModel>());
            }
            set
            {
                _observalCollectionConfigs = value;
                OnPropertyChanged(nameof(Configs));
            }
        }
        private ConfigSettingsModel selectedConfig;
        public ConfigSettingsModel SelectedConfig
        {
            get
            {
                return selectedConfig;
            }
            set
            {
                selectedConfig = value;
                OnPropertyChanged(nameof(SelectedConfig));
            }
        }



        #endregion

        #region Добавление конфигов
        private RelayCommand _AddConfig;

        public RelayCommand AddConfig
        {
            get { return _AddConfig ?? (_AddConfig = new RelayCommand(AddConfigClick)); }
        }

        private void AddConfigClick(object obj)
        {
            
            ConfigSettingsViewModel csvm = new ConfigSettingsViewModel(new ConfigSettingsModel());
            ConfigSettingsView csv = new ConfigSettingsView();
            csvm.ConfigSetttingModel.Path = "C:\\Users\\Trush_EG\\Desktop";            
            csv.DataContext = csvm;
            var result = csv.ShowDialog();
            

            using (FileStream fs =
                          new FileStream(ConfigPath, FileMode.Create))
            {
                XmlSerializer xs = new XmlSerializer(typeof(ObservableCollection<ConfigSettingsModel>));
                xs.Serialize(fs, Configs);
            }
        }

        private RelayCommand _EditConfig;

        public RelayCommand EditConfig
        {
            get { return _EditConfig ?? (_EditConfig = new RelayCommand(EditConfigClick,EditOrDeleteConfigCanExecute)); }
        }

        private bool EditOrDeleteConfigCanExecute(object arg)
        {
            return false;
        }

        private void EditConfigClick(object obj)
        {
            throw new NotImplementedException();
        }

        private RelayCommand _DeleteConfig;

        public RelayCommand DeleteConfig
        {
            get { return _DeleteConfig ?? (_DeleteConfig = new RelayCommand(DeleteConfigClick, EditOrDeleteConfigCanExecute));}
        }

        private void DeleteConfigClick(object obj)
        {
            throw new NotImplementedException();
        }



        #endregion


        #region ButtonPart
        private string runButtonText = "Запустить WCF";
        public string RunButtonText
        {
            get { return runButtonText; }
            set
            {
                runButtonText = value;
                OnPropertyChanged(nameof(RunButtonText));
            }
        }

        private RelayCommand runWCFCommand;

        public RelayCommand RunWCFCommand
        {
            get { return runWCFCommand ?? (runWCFCommand = new RelayCommand(StartStopWcf,StartStopWCFCanExecute)); }           
        }
        private WCFButton _rwcfbsp;
        public WCFButton Rwcfbsp
        {
            get
            {
                if (SelectedConfig == null)
                {
                    return null;
                }
                return _rwcfbsp??(_rwcfbsp = new WCFButton(SelectedConfig?.CurrentConfig));
            }
            set
            {
                _rwcfbsp = value;
            }
        }
        /// <summary>
        /// Метод execute
        /// </summary>
        /// <param name="obj"></param>
        private void StartStopWcf(object obj)
        {
            Rwcfbsp.RunWCFButton();            
            RunButtonText = Rwcfbsp.State.ButtonName;
        }

        private bool StartStopWCFCanExecute(object parameter)
        {
            if (Rwcfbsp == null)
                return false;
            return Rwcfbsp.State.Executed;
        }
        #endregion
    }
}
