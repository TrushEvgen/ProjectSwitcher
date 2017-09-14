using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WCF.UTILS;

namespace ProjectSwitcher.Model
{
    [Serializable]
    public class Config
    {
        public Config(WCF.UTILS.Config cfg)
        {
            ApplyBorderConditions = cfg.ApplyBorderConditions;
            AuditTimeOut = cfg.AuditTimeOut;
            CachePath = cfg.CachePath;
            DataSource = cfg.DataSource;
            DesktopApp = cfg.DesktopApp;
            DocTransferPath = cfg.DocTransferPath;
            DocTransferTimeOut = cfg.DocTransferTimeOut;
            FailoverPartner = cfg.FailoverPartner;
            InitialCatalog = cfg.InitialCatalog;
            IntegratedSecurity = cfg.IntegratedSecurity;
            Provider = cfg.Provider;
            UserDB = cfg.UserDB;
            UserPassword = cfg.UserPassword;
            UseTemplateTime = cfg.UseTemplateTime;
            СompressSendignData = cfg.СompressSendignData;
        }
        public Config()
        {

        }
        private string _provider = string.Empty;

        /// <summary>
        /// Provider. (например SQLOLEDB.1)
        /// </summary>
        [WCF.UTILS.DisplayName("Провайдер данных")]
        [Description("Provider. (например SQLOLEDB.1)")]
        [Category("Настройка подключения к БД")]
        public string Provider
        {
            get { return _provider; }
            set { _provider = value; }
        }

        private string _dataSource = string.Empty;
        /// <summary>
        /// DataSource. (например ST_INS\\ST_INS)
        /// </summary>
        [WCF.UTILS.DisplayName("Источник данных")]
        [Description("DataSource. (например ST_INS\\ST_INS)")]
        [Category("Настройка подключения к БД")]
        public string DataSource
        {
            get { return _dataSource; }
            set { _dataSource = value; }
        }

        private string _initialCatalog = string.Empty;
        /// <summary>
        /// InitialCatalog. (например Insurance)
        /// </summary>
        [WCF.UTILS.DisplayName("Имя базы данных")]
        [Description("InitialCatalog. (например Insurance)")]
        [Category("Настройка подключения к БД")]
        [TypeConverter(typeof(DataBaseValuesTypeConverter))]
        public string InitialCatalog
        {
            get { return _initialCatalog; }
            set { _initialCatalog = value; }
        }

        private string _integratedSecurity = string.Empty;
        /// <summary>
        /// IntegratedSecurity. (например SSPI)
        /// </summary>
        [WCF.UTILS.DisplayName("Настройка подключения к БД (безопасность)")]
        [Description("IntegratedSecurity. (например SSPI)")]
        [Category("Настройка подключения к БД")]
        public string IntegratedSecurity
        {
            get { return _integratedSecurity; }
            set { _integratedSecurity = value; }
        }

        private string _failoverPartner = string.Empty;
        /// <summary>
        /// FailoverPartner. (активизируется в случае выхода из строя первичного)
        /// </summary>
        [WCF.UTILS.DisplayName("Источник данных (Вторичных сервер БД)")]
        [Description("FailoverPartner. (активизируется в случае выхода из строя первичного)")]
        [Category("Настройка подключения к БД")]
        public string FailoverPartner
        {
            get { return _failoverPartner; }
            set { _failoverPartner = value; }
        }

        private bool _compressSendignData = false;
        /// <summary>
        /// Сжимать ли пересылаемые данные (com+ to com+) в случаем медленного соединения ( например, модемного)
        /// </summary>
        [WCF.UTILS.DisplayName("Архиварование пересылаемых query-компонентом данных")]
        [Description("Сжимать ли пересылаемые данные (com+ to com+) в случаем медленного соединения ( например, модемного)")]
        [Category("Настройка com+ компонентов")]
        [DefaultValue(false)]
        public bool СompressSendignData
        {
            get { return _compressSendignData; }
            set { _compressSendignData = value; }
        }

        private FolderPath _cachePath = new FolderPath();
        /// <summary>
        /// При работе с БД com+ оптимизирует работу, путем создания файлов для хранения локального cache'а
        /// </summary>
        [WCF.UTILS.DisplayName("Путь для хранения cache-файлов")]
        [Description("При работе с БД com+ оптимизирует работу, путем создания файлов для хранения локального cache'а")]
        [Category("Настройка com+ компонентов")]
        public FolderPath CachePath
        {
            get { return _cachePath; }
            set { _cachePath = value; }
        }

        private FolderPath _docTransferPath = new FolderPath();
        /// <summary>
        /// Путь, который необходим для получения, сканированных изображений документов кредитных досье
        /// </summary>
        [WCF.UTILS.DisplayName("Путь, к файлам сканированных документов")]
        [Description("Путь, который необходим для получения, сканированных изображений документов кредитных досье (Слеш на конце убрать!!!)")]
        [Category("Настройка com+ компонентов")]
        public FolderPath DocTransferPath
        {
            get { return _docTransferPath; }
            set { _docTransferPath = value; }
        }


        private int _docTransferTimeOut = 120000;
        /// <summary>
        /// Время ожидания появления документов сканирования
        /// </summary>
        [WCF.UTILS.DisplayName("Таймаут получания файлов сканированных документов в миллисекундах")]
        [Description("Таймаут получания файлов сканированных документов в миллисекундах")]
        [Category("Настройка com+ компонентов")]
        public int DocTransferTimeOut
        {
            get { return _docTransferTimeOut; }
            set { _docTransferTimeOut = value; }
        }

        /// <summary>
        /// Строка подключения к базеданных для выплнения запросов через SQLXML библиотеку
        /// </summary>
        [Browsable(false)]
        public string ConnectingStringSQLXML
        {
            get
            {
                //return string.Format("Provider={0};Data Source='{1}';Initial Catalog='{2}';Integrated Security='{3}';Failover Partner='{4}'",
                //    this.Provider, this.DataSource, this.InitialCatalog, this.IntegratedSecurity, this.FailoverPartner);

                if (string.IsNullOrEmpty(this.IntegratedSecurity))
                {
                    return string.Format("Provider='{0}';Initial Catalog='{1}';Data Source='{2}';User Id='{3}';Password='{4}';Failover Partner='{5}'", // Persist Security Info=False;
                                    this.Provider,
                                    this.InitialCatalog,
                                    this.DataSource,
                                    this.UserDB,
                                    this.UserPassword,
                                    this.FailoverPartner);
                }
                else
                {
                    return string.Format("Provider='{0}';Integrated Security='{1}';Initial Catalog='{2}';Data Source='{3}';Failover Partner='{4}'", // Persist Security Info=False;
                                                    this.Provider,
                                                    this.IntegratedSecurity,
                                                    this.InitialCatalog,
                                                    this.DataSource,
                                                    this.FailoverPartner);
                }

            }
        }

        /// <summary>
        /// Строка подключения к базе данных через SQLConnection
        /// </summary>
        [Browsable(false)]
        public string ConnectingString
        {
            get
            {
                if (string.IsNullOrEmpty(this.IntegratedSecurity))
                {
                    return string.Format("Server={0};DataBase='{1}';User Id='{2}'; Password='{3}'",
                            this.DataSource, this.InitialCatalog, this.UserDB, this.UserPassword);
                }
                else
                {
                    return string.Format("Server={0};DataBase='{1}';Integrated Security='{2}'",
                        this.DataSource, this.InitialCatalog, this.IntegratedSecurity);
                }
            }
        }

        private uint auditTimeOut = 6000;
        /// <summary>
        /// Интервал записи данных аудита в базу
        /// </summary>
        [WCF.UTILS.DisplayName("Интервал записи данных аудита в базу")]
        [Description("Промежуток времени (в миллисекундах), через которое данные аудита сбрасываются с сервера приложений в базу данных")]
        [Category("Настройка com+ компонентов")]
        [DefaultValue(6000)]
        public uint AuditTimeOut
        {
            get { return auditTimeOut; }
            set { auditTimeOut = value; }
        }

        private bool useTemplateTime = false;
        /// <summary>
        /// Проверять установленное время работы бизнес-правила
        /// </summary>
        [WCF.UTILS.DisplayName("Проверять установленное время работы бизнес-правила")]
        [Description("Если true, то будет проверяться установленное время работы каждой темплаты")]
        [Category("Настройка com+ компонентов")]
        public bool UseTemplateTime
        {
            get { return useTemplateTime; }
            set { useTemplateTime = value; }
        }

        private bool _applyBorderConditions = false;
        /// <summary>
        /// Накладывать ли при выполнении темлат на входную xml'ну граничные условия.
        /// </summary>
        [WCF.UTILS.DisplayName("Применять граничные условия.")]
        [Description("Будут ли накладываться на xml темплаты со входными параметрами граничные условия.")]
        [Category("Настройка com+ компонентов")]
        [DefaultValue(false)]
        public bool ApplyBorderConditions
        {
            get { return _applyBorderConditions; }
            set { _applyBorderConditions = value; }
        }

        private bool _desktopApp = false;
        /// <summary>
        /// Возможность поддержи БД тонкого клиента
        /// </summary>
        /// //true - толстый клиент; false - тонкий клиент
        [WCF.UTILS.DisplayName("Поддержка тонкого клиента")]
        [Description("Настройка меню операций клиента")]
        [Category("Настройка com+ компонентов")]
        [DefaultValue(false)]
        public bool DesktopApp
        {
            get { return _desktopApp; }
            set { _desktopApp = value; }
        }


        private string _userDB = string.Empty;
        /// <summary>
        /// Возможность поддержи БД тонкого клиента
        /// </summary>
        /// //true - толстый клиент; false - тонкий клиент
        [WCF.UTILS.DisplayName("Имя пользователя")]
        [Description("Имя пользователя")]
        [Category("Настройка подключения к БД")]
        [DefaultValue("")]
        public string UserDB
        {
            get { return _userDB; }
            set { _userDB = value; }
        }

        private string _userPassword = string.Empty;
        /// <summary>
        /// Возможность поддержи БД тонкого клиента
        /// </summary>
        /// //true - толстый клиент; false - тонкий клиент
        [WCF.UTILS.DisplayName("Пароль пользователя")]
        [Description("Пароль пользователя")]
        [Category("Настройка подключения к БД")]
        [DefaultValue("")]
        public string UserPassword
        {
            get { return _userPassword; }
            set { _userPassword = value; }
        }

        //private string _cachePath = string.Empty;
        //[DisplayName("Путь для хранения cache-файлов")]
        //[Description("При работе с БД com+ оптимизирует работу, путем создания файлов для хранения локального cache'а")]
        //[Category("Настройка com+ компонентов")]
        //public string CachePath
        //{
        //    get {return _cachePath;}
        //    set {_cachePath = value;}
        //}


        // public enum CodeStyle
        // {
        // Good,
        // Normal,
        // Bad,
        // Ugly
        // }

        //private CodeStyle _test = CodeStyle.Bad;
        // [DisplayName("Архиварование пересылаемых query-компонентом данных")]
        // [Description("Сжимать ли пересылаемые данные (com+ to com+) в случаем медленного соединения ( например, модемного)")]
        // [Category("Настройка com+ компонентов")]
        // public CodeStyle Test2
        // {
        //     get {return _test;}
        //     set {_test = value;}
        // }
        public WCF.UTILS.Config ToConfig()
        {
            WCF.UTILS.Config config = WCF.UTILS.Config.Instance;

            config.ApplyBorderConditions = ApplyBorderConditions;
            config.AuditTimeOut = AuditTimeOut;
            config.CachePath = CachePath;
            config.DataSource = DataSource;
            config.DesktopApp = DesktopApp;
            config.DocTransferPath = DocTransferPath;
            config.DocTransferTimeOut = DocTransferTimeOut;
            config.FailoverPartner = FailoverPartner;
            config.InitialCatalog = InitialCatalog;
            config.IntegratedSecurity = IntegratedSecurity;
            config.Provider = Provider;
            config.UserDB = UserDB;
            config.UserPassword = UserPassword;
            config.UseTemplateTime = UseTemplateTime;
            config.СompressSendignData = СompressSendignData;
            return config;
        }       
    }  
}
