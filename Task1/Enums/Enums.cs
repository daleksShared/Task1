using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Enums
{
    public enum LogAction
    {
        LineUp,
        LineClose,
        LineBusy,
        CallStarted,
        CallEnded
    }

    public enum ContractType
    {
        Debet,
        Credit
    }

    public enum PortState
    {
        Free,
        Unplugged,
        Default = Unplugged,
        Plugged,
        Opened,
        Busy
    }
    public enum ConnectPortResultState
    {

        StationBusy,
        Default = StationBusy,
        Closed,
        Listening
    }
    public enum ConnectCallerResultState
    {
        TargetBusy,
        Default= TargetBusy,
        NoSuchCaller,
        NoAnswer,
        Ok
    }
    public enum TerminalState
    {
        On,
        Default=On,
        Off
    }


}
