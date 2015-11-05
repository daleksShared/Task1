namespace Task1
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using Microsoft.Win32.SafeHandles;
    using System.Threading;
    using System.ComponentModel;
    using Enums;
    using Task1.EventArgs;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class ModelAts : DbContext
    {

        public void DoCallStart(Caller caller,Caller callee)
        {



        }
        



       
        public System.Timers.Timer billingGlobalTimer = new System.Timers.Timer();



        // Your context has been configured to use a 'ModelAts' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Task1.ModelAts' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'ModelAts' 
        // connection string in the application configuration file.
        public ModelAts()
            : base("name=ModelAts")
        {
        }
        public ModelAts(string sqlcon)
            : base(sqlcon)
        {
            //  billingGlobalTimer.Interval = (DateTime.Today.AddMonths(1).AddDays(-(DateTime.Today.Day - 1)) - DateTime.Now).TotalMilliseconds;

            billingGlobalTimer.Interval = (DateTime.Today.AddDays(1)- DateTime.Now.AddSeconds(-1)).TotalMilliseconds;
            billingGlobalTimer.AutoReset = false;
            billingGlobalTimer.Enabled = true;
        }

        public DbSet<Port> Ports { get; set; }
        public DbSet<Caller> Callers { get; set; }
        public DbSet<Call> CallHistory { get; set; }
        public DbSet<Billing> BillingCalls { get; set; }
        public DbSet<AtsLog> AtsLog { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Tarrif> CallTarrifs { get; set; }
        public DbSet<Terminal> Terminals{ get; set; }

        public void GetCallHistory(int callerId)
        {
            Console.WriteLine("Направление" + "\t" + "Источник" + "\t" + "Цель" + "\t" + "Время:" + "\t" +
                              "Продолжительность" + "\t" + "Цена");
            if (CallHistory == null || CallHistory.Count() == 0) return;
            var callHistory =
                CallHistory.Where(
                    x =>
                        (
                            (x.Caller.Id == callerId || x.Callee.Id == callerId)
                            & x.StarTime.Date >= DateTime.Today.AddDays(-30)
                            )
                    )
                    .OrderByDescending(x => x.StarTime)
                    .Select(x => new
                    {
                        direction = x.Caller.Id == callerId ? "output" : "input",
                        callerID = x.Caller.Id,
                        calleID = x.Callee.Id,
                        x.StarTime,
                        x.Duration,
                        cost = BillingCalls.Where(y => y.CallerId == callerId && y.StarTime == x.StarTime)
                                .Select(y => new { cost = ((float)y.Duration.TotalSeconds * y.Contract.Tarrif.MinuteCost) })
                                .FirstOrDefault().cost
                    })
                    .ToList();
            foreach (var item in callHistory)
            {
                var cost = BillingCalls.Where(y => y.CallerId == callerId && y.StarTime == item.StarTime)
                                .Select(y => new { cost = ((float)y.Duration.TotalSeconds * y.Contract.Tarrif.MinuteCost) })
                                .FirstOrDefault().cost;
                Console.WriteLine(item.direction + "\t" + item.callerID.ToString() + "\t" + item.calleID + "\t" +
                                  item.StarTime.ToString("yyyy-M-d hh:mm:ss") + "\t" + item.Duration.TotalSeconds + "\t" +
                                 item.cost.ToString("C"));
            }
        }
    }


    public class Terminal : Idispose
    {
        public int Id { get; set; }
        public Port TerminalPort { get; set; }
        private TerminalState _terminalState;
        public TerminalState TerminalState { get { return _terminalState; }
            set { _terminalState = value; }
        }
        private string _StoredDialText = "";
        public string StoredDialText { get { return _StoredDialText; } set { _StoredDialText = value; } }

        public void KeyPressed(char key)
        {
            switch (key)
            {
                case (char)12:
                    StoredDialText = "";
                    break;
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                case '0':
                    break;
                case (char)10:
                    makeCall(StoredDialText);
                    break;
                default:
                    break;
            }
        }

        public void makeCall(string phoneNumber)
        {

        }


    }

    public partial class Caller : Idispose
    {
        private int _canChangeTarrif = 0;
        public int Id { get; set; }
        private int _CallerId;
        public int CallerId
        {
            get { return _CallerId; }
            set { _CallerId = value; }
        }

        public Terminal Terminal
        {
            get; set;
        }
        public Nullable<int> CanChangeTarrif
        {
            get
            {
                return _canChangeTarrif;
            }
            set
            {

                _canChangeTarrif = value == null ? 0 : (int)value;
            }
        }


        private static object CallerSync = new object();

        public void ConnectTerminalToPort()
        {
            Terminal.TerminalPort.State = PortState.Plugged;
        }
        public void DisconnectTerminalFromPort()
        {
            Terminal.TerminalPort.State = PortState.Unplugged;
        }

     
    }
    public class Billing
    {
        public int Id { get; set; }
        public DateTime StarTime { get; set; }
        public int CallerId { get; set; }
        public Contract Contract { get; set; }
        public TimeSpan Duration { get; set; }


    }
    public class AtsLog
    {
        public int Id { get; set; }
        public DateTime StarTime { get; set; }
        public Port Port { get; set; }
        public Caller Caller {get;set;}
        public Caller Callee { get; set; }
        public LogAction LogAction { get; set; }
    }
    
    public class Contract
    {
        public int Id { get; set; }
        public string number = "";
        public Caller Caller { get; set; }
        public string Number
        {
            get { return number; }
            set
            {
                if (value == string.Empty)
                {
                    throw new System.ArgumentException("Номер договора не может быть пустым!!!");
                }
                else
                {
                    number = value;
                }
            }
        }

        public DateTime ContractDate { get; set; }
        public ContractType AgreementType { get; set; }
        public Tarrif Tarrif { get; set; }

    }

    public class Tarrif:Idispose
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float MinuteCost { get; set; }
    }











    public partial class Call
    {
        public int Id { get; set; }
        public DateTime StarTime { get; set; }
        public TimeSpan Duration { get; set; }
        public Caller Caller { get; set; }
        public Caller Callee { get; set; }

        [NotMapped]
        public Timer Timer { get; set; }

        
        public void OnCallStart(Object sender, System.EventArgs args)
        {
           
            StarTime = DateTime.Now;
            Timer = new Timer(TimerCallback, null, 0, 1000);
            if (args is EventArgs.CallEventArgs)
                _callId = (args as CallingEventArgs).Id;
        }

       
        private EventHandler<ConnectCallerResultState> ConnectingHandler;

        public event EventHandler<ConnectCallerResultState> AfterCallEnded;

    }

    public class Port:Idispose
    {
        public int Id { get; set; }
        public int PortNum { get; set; }
        public Port()
        {
            Captured = false;
        }
        public Caller Caller { get; set; }
        public bool Captured { get; set; }
        private PortState _state;
        public PortState State { get { return State; }

            set
            {
                // try to subscribe
                switch (value)
                {
                    case PortState.Free:
                        break;
                    case PortState.Unplugged:
                        Program.listeners.AddListenerCallFromAtsToPort(this, OnIncomingCall);
                        break;
                    case PortState.Plugged:
                        break;
                    case PortState.Opened:
                        break;
                    case PortState.Busy:
                        break;
                    default:
                        break;
                }

                if (value == PortState.Unplugged)
                {
                    
                }
                else if (value == (PortStateForAts.Plugged | PortStateForAts.Free))
                {
                    //Program.Listners.AddCallFromAtsToPortListener(this, OnIncomingCall);
                    Program.Listners.DelHangUpFromTerminalToPortListener(this, OnHangUpFromTerminal);
                }
                else if (value == (PortStateForAts.Plugged | PortStateForAts.Busy))
                {
                    //Program.Listners.DelCallFromAtsToPortListener(this, OnIncomingCall);
                    Program.Listners.AddHangUpFromTerminalToPortListener(this, OnHangUpFromTerminal);
                }
                else if (value == PortStateForAts.UnPlugged)
                {
                    Program.Listners.DelCallFromTerminalToPortListener(this, OnIncomingCall);
                    Program.Listners.DelHangUpFromTerminalToPortListener(this, OnHangUpFromTerminal);
                    Program.Listners.DelCallFromAtsToPortListener(this, OnIncomingCall);
                }

                _portStateForAts = value;
            } }
        }
    }
   


}
