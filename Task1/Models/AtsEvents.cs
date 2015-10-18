using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Task1
{
    partial class ATS
    {
        public class LineUpEventArgs : System.EventArgs
        {
            public readonly int _portId, _callerId;

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
        public event EventHandler<LineUpEventArgs> LineUpEvent;


        public class CallerInsertSymbolEventArgs : System.EventArgs
        {
            public readonly char _symbol;
            public readonly int _callerId;

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
        protected virtual void OnLineUp(LineUpEventArgs e)
        {
            // Сохранить ссылку на делегата во временной переменной
            // для обеспечения безопасности потоков
            EventHandler<LineUpEventArgs> temp = Volatile.Read(ref LineUpEvent);
            // Если есть объекты, зарегистрированные для получения
            // уведомления о событии, уведомляем их
            if (temp != null) temp(this, e);
        }
    
    }

  
}
