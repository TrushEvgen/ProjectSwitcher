using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WCF.UTILS;
using WCF;
using System.Diagnostics;

namespace ProjectSwitcher.StatePattern
{
    public class WCFButton
    {
       public IWCFButton State { get; set; }

        public WCFButton (IWCFButton state)
        {
            State = state;
        }

        public void RunWCFButton()
        {
            State.Run(this);
        }

        public WCFButton(Model.Config config):this (new StartWCFButton())
        {
            cfg = config;
        }
        private ServiceHost sh = new ServiceHost(typeof(LoanService));
        private Model.Config cfg;
        public void ForceStart()
        {            
            Config.NewConfig(cfg.ToConfig());
            Config.Instance.Save();
            switch (sh.State)
            {
                case CommunicationState.Created:                   
                    sh.Open();
                    break;
                case CommunicationState.Closed:
                    sh = new ServiceHost(typeof(WCF.LoanService));
                    sh.Open();
                    break;              
            }
            var z = EventLog.GetEventLogs();
        }
        public void ForceStop()
        {
            var z = EventLog.GetEventLogs();
            System.Threading.Thread.Sleep(1000);
            
            switch (sh.State)
            {                
                case CommunicationState.Opened:
                    sh.Close();
                    AuditMngr.CloseThread();
                    GC.Collect();
                    GC.WaitForFullGCComplete();
                    break;
            }
        }
    }

    public interface IWCFButton
    {
        void Run(WCFButton wcfButton);
        string ButtonName { get;}

        bool Executed { get; }
    }

   class StartWCFButton : IWCFButton
    {
        public string ButtonName
        {
            get
            {
                return "Запустить WCF";                
            }
        }
        bool _executed = true;
        public bool Executed
        {
            get
            {
                return _executed;
            }
        }

        public void Run(WCFButton wcfButton)
        {
            //стартуем WCF   
            _executed = false;
            wcfButton.ForceStart();
            _executed = true;
            wcfButton.State = new RestartWCFButton();
        }
    }

    class RestartWCFButton : IWCFButton
    {
        public string ButtonName
        {
            get
            {
                return "Перезапустить WCF";
            }
        }

        bool _executed = true;
        public bool Executed
        {
            get
            {
                return _executed;
            }
        }
        public void Run(WCFButton wcfButton)
        {
            _executed = false;;
            wcfButton.ForceStop();
            wcfButton.ForceStart();
            _executed = true;
            wcfButton.State = new StopWCFButton();
        }
    }
    class StopWCFButton : IWCFButton
    {
        public string ButtonName
        {
            get
            {
                return "Остановить WCF";
            }
        }

        bool _executed = true;
        public bool Executed
        {
            get
            {
                return _executed;
            }
        }
        public void Run(WCFButton wcfButton)
        {
            _executed = false;
            wcfButton.ForceStop();
            _executed = true;
            wcfButton.State = new StartWCFButton();
        }
    }
}
