using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            





            var ats= new ATS();

            ats.GetCallHistory(1);

            Console.ReadKey();
        }
    }
}
