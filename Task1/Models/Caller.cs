using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task1
{
    partial class ATS
    {
        public class Caller
        {
            public int Id { get; set; }
            public Contract Contract { get; set; }
            public List<Port> Ports {get;set;}
        }
    }
}
