using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSwitcher.Model
{
    public class Settings
    {
        private string _dataSource = string.Empty;
        /// <summary>
        /// DataSource. (например ST_INS\\ST_INS)
        /// </summary>
        [DisplayName("Источник данных")]
        [Description("DataSource. (например ST_INS\\ST_INS)")]
        [Category("Настройка подключения к БД")]
        public string DataSource
        {
            get { return _dataSource; }
            set
            {
                _dataSource = value;
                //INotifyPropertyChanged
            }
        }

        public string ConnectingString
        {
            get
            {
               return string.Format("Data Source='{0}';"
                          + "Integrated Security=SSPI;Connection Timeout={1}", DataSource, 5);

            }
        }
    }
}
