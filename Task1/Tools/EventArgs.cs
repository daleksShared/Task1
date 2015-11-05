using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Task1.Enums;
namespace Task1.EventArgs
{



    public class HangUpEventArgs : System.EventArgs
    {
        public Port Port
        {
            get; set;
        }
        public Terminal Terminal { get; set; }
    }

    public class CallerInsertSymbolEventArgs : System.EventArgs
        {
            public readonly char _symbol;
            public readonly Caller _caller;

            public CallerInsertSymbolEventArgs(Caller caller, char symbol)
            {
                _symbol = symbol;
                _caller = caller;
            }

            public int Symbol
            {
                get { return _symbol; }
            }

            public Caller Caller
            {
                get { return _caller; }
            }
        }

    public class CallEventArgs : System.EventArgs
    {
        public DateTime StartTime { get; set; }
        public Caller Caller { get; set; }
        public Caller Callee { get; set; }
        public ConnectCallerResultState ConnectCallerResultState { get; set; }
    }
    public class PortEventArgs : System.EventArgs
    {
        public TerminalState TerminalState { get; set; }
        public PortState PortState { get; set; }
        public ConnectPortResultState ConnectPortResultState { get; set; }
    }


    //public event EventHandler<LineUpEventArgs> LineUpEvent;
    //protected virtual void OnLineUp(LineUpEventArgs e)
    //{
    //    EventHandler<LineUpEventArgs> temp = Volatile.Read(ref LineUpEvent);
    //    // Если есть объекты, зарегистрированные для получения
    //    // уведомления о событии, уведомляем их
    //    if (temp != null) temp(this, e);

    //}

}



