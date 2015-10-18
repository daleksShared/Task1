using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;


namespace Task1
{
  

    partial class ATS
    {
        public System.Timers.Timer billingGlobalTimer=new System.Timers.Timer();
        public static void billingGlobalTimerRaise(Object source, System.Timers.ElapsedEventArgs e)
        {
            //update customers
           
        }

        public ATS() {

            //timerInit
            billingGlobalTimer.Interval = (DateTime.Today.AddMonths(1).AddDays(-(DateTime.Today.Day - 1)) - DateTime.Now).TotalMilliseconds;
            billingGlobalTimer.Elapsed += billingGlobalTimerRaise;
            billingGlobalTimer.AutoReset = false;
            billingGlobalTimer.Enabled = true;
        }





        public List<CallerId> CallerIds{get;set;}
        public List<Port> Ports { get; set; }
        public List<Caller> Callers { get; set; }
        public List<Call> CallHistory { get; set; }
        public List<Billing> BillingCalls { get; set; }

        


        #region Методы

        public void LineUp(int portId,int callerId)
        {
            LineUpEventArgs e = new LineUpEventArgs(portId, callerId);
            OnLineUp(e);
            //EventHandler<LineUpEventArgs> lineUpHandler += LineUpEvent;
            //if (lineUpHandler != null)
            //{
            //    LineUpEventArgs e = new LineUpEventArgs(portId,callerId);
            //    lineUpHandler(this, e);
            //}
        }

        public void GetCallHistory(int callerId)
        {
            Console.WriteLine("Направление" + "\t" + "Источник" + "\t" + "Цель" + "\t" + "Время:" + "\t" +
                              "Продолжительность" + "\t" + "Цена");
            if (CallHistory == null || CallHistory.Count == 0) return;
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

                        cost = GetCallCost(x.Caller.Id, x.StarTime)
                    })
                    .ToList();
            foreach (var item in callHistory)
            {
                Console.WriteLine(item.direction + "\t" + item.callerID.ToString() + "\t" + item.calleID + "\t" +
                                  item.StarTime.ToString("yyyy-M-d hh:mm:ss") + "\t" + item.Duration.TotalSeconds + "\t" +
                                  item.cost.ToString("C"));
            }
        }

        public float GetCallCost(int callerId, DateTime starTime)
        {
            var cost =
                BillingCalls.Where(x => x.CallerId == callerId && x.StarTime == starTime)
                    .Select(x => new {cost = ((float) x.Duration.TotalSeconds*x.Contract.Tarrif.MinuteCost)})
                    .FirstOrDefault();
            return cost == null ? 0 : cost.cost;
        }

        #endregion
    

    #region Классы параметров АТС

    public class Call
    {
        public DateTime StarTime { get; set; }
        public TimeSpan Duration { get; set; }
        public Caller Caller { get; set; }
        public Caller Callee { get; set; }

    }

    public class Port
    {
        public int Id { get; set; }

        public Port()
        {
            Captured = false;
        }

        public bool Captured { get; set; }

    }

    public class CallerId
    {
        public int Id { get; set; }
        public Port Port { get; set; }
        public Caller Caller { get; set; }
    }

    #endregion

    }
}
