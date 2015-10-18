using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{


   public partial class ModelAts
    {
        public System.Timers.Timer billingGlobalTimer = new System.Timers.Timer();

        public void LineUp(int portId, int callerId)
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

        public static void billingGlobalTimerRaise(Object source, System.Timers.ElapsedEventArgs e)
        {
            //update customers

        }

    }
}
