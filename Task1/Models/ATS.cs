using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;


namespace Task1
{
    internal class LineUpEventArgs : System.EventArgs
    {
        private readonly int _portId, _callerId;

        public LineUpEventArgs(int portId, int callerId)
        {
            _portId = portId;
            _callerId = callerId;
        }

        public int PortId
        {
            get { return _portId; }
        }

        public int CallerId
        {
            get { return _callerId; }
        }
    }
    internal class CallerInsertSymbolEventArgs : System.EventArgs
    {
        private readonly char _symbol;
        private readonly int _callerId;

        public CallerInsertSymbolEventArgs(int callerId, char symbol)
        {
            _symbol = symbol;
            _callerId = callerId;
        }

        public int Symbol
        {
            get { return _symbol; }
        }

        public int CallerId
        {
            get { return _callerId; }
        }
    }

    internal class ATS
    {
        public event EventHandler<LineUpEventArgs> LineUpEvent;

        public delegate void EventHandler(Object sender, LineUpEventArgs e);


        public ATS()
        {
        } // для xml сериализации пустой конструктор




        public List<CallerId> CallerIds;
        public List<Port> Ports;
        public List<Caller> Callers;
        public List<Call> CallHistory;
        public List<Billing> Billing;

        


        #region Методы

        public void LineUp(int portId,int callerId)
        {
            EventHandler<LineUpEventArgs> handler = LineUpEvent;
            if (handler != null)
            {
                LineUpEventArgs e = new LineUpEventArgs(portId,callerId);
                handler(this,e);
            }
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

        private float GetCallCost(int callerId, DateTime starTime)
        {
            var cost =
                Billing.Where(x => x.CallerId == callerId && x.StarTime == starTime)
                    .Select(x => new {cost = ((float) x.Duration.TotalSeconds*x.Contract.Tarrif.MinuteCost)})
                    .FirstOrDefault();
            return cost == null ? 0 : cost.cost;
        }

        #endregion
    }

    #region Классы параметров АТС

    public class Call
    {
        public DateTime StarTime;
        public TimeSpan Duration;
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
