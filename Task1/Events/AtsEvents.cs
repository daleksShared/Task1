using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Task1
{
    public partial class ModelAts
    {
        public System.Timers.Timer billingGlobalTimer = new System.Timers.Timer();

    }


        public class LineUpEventArgs : System.EventArgs
        {
            public readonly Port _port;
            public readonly Caller _caller;

            public LineUpEventArgs(Port port)
            {
                _port = port;

            }

            public Port PortId
            {
                get { return _port; }
            }


        }

        public event EventHandler<LineUpEventArgs> LineUpEvent;
        protected virtual void OnLineUp(LineUpEventArgs e)
        {
            EventHandler<LineUpEventArgs> temp = Volatile.Read(ref LineUpEvent);
            // Если есть объекты, зарегистрированные для получения
            // уведомления о событии, уведомляем их
            if (temp != null) temp(this, e);

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




    }

  

