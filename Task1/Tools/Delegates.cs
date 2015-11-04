using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task1.EventArgs;

namespace Task1
{

    // callerId <-> port <-> ATS
    //caller -> calle = caller terminal state -> callerPort state -> callee port state -> callee terminal state
    public delegate void PortStatusChangeEventHandler(object sender, HangUpEventArgs e);
    public delegate void HangUpDelegate(object sender, HangUpEventArgs e);
    public delegate void CallerDelegate(object sender, System.EventArgs e);
    public class Listeneners
    {

        public Dictionary<Port, CallerDelegate> ListenerCallFromAtsToPort { get; set; }
        public void AddListenerCallFromAtsToPort(Port port, CallerDelegate listener)
        {
            if (listener == null) return;
            if (ListenerCallFromAtsToPort.ContainsKey(port))
            {
                ListenerCallFromAtsToPort[port] += listener;
            }
            else
                ListenerCallFromAtsToPort[port] = listener;
        }
        public void DelListenerCallFromAtsToPort(Port port, CallerDelegate listener)
        {
            if (listener == null) return;
            if (ListenerCallFromAtsToPort.ContainsKey(port))
            {
                ListenerCallFromAtsToPort[port] -= listener;
            }
        }

        public Dictionary<Port, CallerDelegate> ListenerCallFromPortToAts { get; set; }
        public void AddListenerCallFromPortToAts(Port port, CallerDelegate listener)
        {
            if (listener == null) return;
            if (ListenerCallFromPortToAts.ContainsKey(port))
            {
                ListenerCallFromPortToAts[port] += listener;
            }
            else
                ListenerCallFromPortToAts[port] = listener;
        }
        public void DelListenerCallFromPortToAts(Port port, CallerDelegate listener)
        {
            if (listener == null) return;
            if (ListenerCallFromPortToAts.ContainsKey(port))
            {
                ListenerCallFromPortToAts[port] -= listener;
            }
        }

        public Dictionary<Port, CallerDelegate> ListenerCallFromPortToCaller { get; set; }
        public void AddListenerCallFromPortToCaller(Port port, CallerDelegate listener)
        {
            if (listener == null) return;
            if (ListenerCallFromPortToCaller.ContainsKey(port))
            {
                ListenerCallFromPortToCaller[port] += listener;
            }
            else
                ListenerCallFromPortToCaller[port] = listener;
        }
        public void DelListenerCallFromPortToCaller(Port port, CallerDelegate listener)
        {
            if (listener == null) return;
            if (ListenerCallFromPortToCaller.ContainsKey(port))
            {
                ListenerCallFromPortToCaller[port] -= listener;
            }
        }

        public Dictionary<Port, CallerDelegate> ListenerCallFromCallerToAts { get; set; }
        public void AddListenerCallFromCallerToAts(Port port, CallerDelegate listener)
        {
            if (listener == null) return;
            if (ListenerCallFromCallerToAts.ContainsKey(port))
            {
                ListenerCallFromCallerToAts[port] += listener;
            }
            else
                ListenerCallFromCallerToAts[port] = listener;
        }
        public void DelListenerCallFromCallerToAts(Port port, CallerDelegate listener)
        {
            if (listener == null) return;
            if (ListenerCallFromCallerToAts.ContainsKey(port))
            {
                ListenerCallFromCallerToAts[port] -= listener;
            }
        }

        public Dictionary<ModelAts, HangUpDelegate> ListenerHangUpFromTerminalToPort { get; set; }
        public void AddListenerHangUpFromTerminalToPort(ModelAts ats, HangUpDelegate listener)
        {
            if (listener == null) return;
            if (ListenerHangUpFromTerminalToPort.ContainsKey(ats))
            {
                ListenerHangUpFromTerminalToPort[ats] += listener;
            }
            else
                ListenerHangUpFromTerminalToPort[ats] = listener;
        }
        public void DelListenerHangUpFromTerminalToPort(ModelAts ats, HangUpDelegate listener)
        {
            if (listener == null) return;
            if (ListenerHangUpFromTerminalToPort.ContainsKey(ats))
            {
                ListenerHangUpFromTerminalToPort[ats] -= listener;
            }
        }

        public Dictionary<ModelAts, HangUpDelegate> ListenerHangUpFromPortToTerminal { get; set; }
        public void AddListenerHangUpFromPortToTerminal(ModelAts ats, HangUpDelegate listener)
        {
            if (listener == null) return;
            if (ListenerHangUpFromPortToTerminal.ContainsKey(ats))
            {
                ListenerHangUpFromPortToTerminal[ats] += listener;
            }
            else
                ListenerHangUpFromPortToTerminal[ats] = listener;
        }
        public void DelListenerHangUpFromPortToTerminal(ModelAts ats, HangUpDelegate listener)
        {
            if (listener == null) return;
            if (ListenerHangUpFromPortToTerminal.ContainsKey(ats))
            {
                ListenerHangUpFromPortToTerminal[ats] -= listener;
            }
        }


        public Listeneners()
        {
            ListenerCallFromAtsToPort = new Dictionary<Port, CallerDelegate>();
            ListenerCallFromPortToAts = new Dictionary<Port, CallerDelegate>();
            ListenerCallFromPortToCaller = new Dictionary<Port, CallerDelegate>();
            ListenerCallFromCallerToAts = new Dictionary<Port, CallerDelegate>();
            ListenerHangUpFromTerminalToPort = new Dictionary<ModelAts, HangUpDelegate>();
        }

        public void ClearListeners()
        {
            ListenerCallFromAtsToPort.Clear();
            ListenerCallFromPortToAts.Clear();
            ListenerCallFromPortToCaller.Clear();
            ListenerCallFromCallerToAts.Clear();
            ListenerHangUpFromTerminalToPort.Clear();
        }


    }
}
