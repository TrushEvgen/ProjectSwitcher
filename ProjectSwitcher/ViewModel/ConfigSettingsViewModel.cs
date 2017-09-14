using System;
using DevExpress.Mvvm;
using ProjectSwitcher.Model;
using ProjectSwitcher.Core;
using System.Collections.ObjectModel;

namespace ProjectSwitcher.ViewModel
{
    public class ConfigSettingsViewModel : ViewModelBase
    {
        public ConfigSettingsViewModel()
        {
           
        }
        public ConfigSettingsViewModel(ConfigSettingsModel model): this()
        {
            currentConfig = model.CurrentConfig.ToConfig();
            ConfigSetttingModel = model;
            //config = model.CurrentConfig.ToConfig();
        }

        private ConfigSettingsModel configSettingsModel;

        public ConfigSettingsModel ConfigSetttingModel
        {
            get { return configSettingsModel; }
            set
            {
                configSettingsModel = value;
                OnPropertyChanged(nameof(ConfigSetttingModel));
            }
        }
        private WCF.UTILS.Config currentConfig;
        public WCF.UTILS.Config CurrentConfig
        {
            get { return currentConfig; }
            set
            {
                currentConfig = value;
                OnPropertyChanged(nameof(CurrentConfig));
            }
        }


        private RelayCommand saveAndExit;

        public RelayCommand SaveAndExit
        {
            get { return saveAndExit ?? (saveAndExit = new RelayCommand(SaveAndExitClick, SaveAndExitClickCanExecute)); }           
        }              

        private void SaveAndExitClick(object obj)
        {
            ConfigSetttingModel.CurrentConfig = new Config(CurrentConfig);
            DialogResult = true;
          
        }

        private bool SaveAndExitClickCanExecute(object arg)
        {
            return !(string.IsNullOrEmpty(configSettingsModel?.ConfigName) || (configSettingsModel?.ConfigName == ConfigSettingsModel.DefaultConfigName));
        }


        private RelayCommand openFileDialog;

        public RelayCommand OpenFileDialog
        {
            get { return openFileDialog ?? (openFileDialog = new RelayCommand(OpenFileDialogClick)); }          
        }

        private void OpenFileDialogClick(object obj)
        {
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            fbd.SelectedPath = configSettingsModel.Path; //проверить
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                configSettingsModel.Path = fbd.SelectedPath;               
            }            
        }       
    }
}