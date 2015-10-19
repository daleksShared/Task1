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
        Busy,
        Default= Busy,
        NoSuchCaller,
        NoAnswer,
        Ok
    }
    public enum CallerTerminalState
    {
        On,
        Default=On,
        Off
    }


}
