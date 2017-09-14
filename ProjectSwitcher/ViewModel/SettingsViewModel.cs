using ProjectSwitcher.Core;
using ProjectSwitcher.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectSwitcher.ViewModel
{
    public class SettingsViewModel : ViewModelBase
    {
        //public Settings Settings;
        public SettingsViewModel(Settings settings)
        {
            Settings = settings;
        }
        public SettingsViewModel(Settings settings,Window window = null)
        {
            Settings = settings;
            SettingsWindow = window;
        }
        public SettingsViewModel() : this(new Settings())
        {

        }
        private Window settingsWindow;

        public Window SettingsWindow
        {
            get { return settingsWindow; }
            set { settingsWindow = value; }
        }

        private Settings settings;

        public Settings Settings
        {
            get { return settings; }
            set { settings = value; }
        }

        RelayCommand _saveAndExit;

        public RelayCommand SaveAndExit
        {
            get
            {
                return _saveAndExit ?? (_saveAndExit = new RelayCommand(SaveAndExitClick, CanSaveAndExit));
            }
        }

        private bool CanSaveAndExit(object arg)
        {
            if (string.IsNullOrEmpty(Settings.DataSource))
                return false;
            return true;
        }

        private void SaveAndExitClick(object obj)
        {
            SettingsWindow.Close();
        }
    }
}
