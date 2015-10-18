namespace Task1
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using Microsoft.Win32.SafeHandles;

    public partial class ModelAts : DbContext
    {
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
            billingGlobalTimer.Interval = (DateTime.Today.AddMonths(1).AddDays(-(DateTime.Today.Day - 1)) - DateTime.Now).TotalMilliseconds;
            billingGlobalTimer.Elapsed += billingGlobalTimerRaise;
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

    }
    // Add a DbSet for each entity type that you want to include in your model. For more information 
    // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

    // public virtual DbSet<MyEntity> MyEntities { get; set; }


    public class Caller:IDisposable
    {
        public int Id { get; set; }
        public int CallerId { get; set; }



        bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //
            disposed = true;
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
    public enum LogAction
    {
        LineUp,
        LineClose,
        LineBusy,
        CallStarted,
        CallEnded
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

    public class Tarrif
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float MinuteCost { get; set; }
    }


    public enum ContractType
    {
        Debet,
        Credit
    }
    












    public class Call
    {
        public int Id { get; set; }
        public DateTime StarTime { get; set; }
        public TimeSpan Duration { get; set; }
        public Caller Caller { get; set; }
        public Caller Callee { get; set; }

    }

    public class Port:IDisposable
    {
        public int Id { get; set; }
        public int PortNum { get; set; }
        public Port()
        {
            Captured = false;
        }
        public Caller Caller { get; set; }
        public bool Captured { get; set; }


        bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //
            disposed = true;
        }
    }

  
}
