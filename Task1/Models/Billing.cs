using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task1
{
    partial class ATS
    {
        public class Billing
        {
            public DateTime StarTime { get; set; }
            public int CallerId { get; set; }
            public Contract Contract { get; set; }
            public TimeSpan Duration { get; set; }


        }
    }
}
