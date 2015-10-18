using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Task1
{
    class Program
    {
        public static ModelAts ats { get; set; }
        static void Main(string[] args)
        {
            ats= new ModelAts(@"data source=(localdb)\MSSQLLocalDB;Initial Catalog=Task1;Integrated Security=True;");
            Database.SetInitializer<ModelAts>(new DropCreateDatabaseAlways<ModelAts>());
            ;
            for (int i = 0; i < 999; i++)
            {
                using (var port = new Port
                {
                    PortNum = 1000 + i,
                    Captured = false
                })
                {
                   ats.Ports.Add(port);
                    using (var caller = new Caller
                    {
                        CallerId = 2000 + i
                    })
                    {
                        ats.Callers.Add(caller);
                        port.Caller = caller;
                    }
                }
            }

            ats.SaveChanges();
            
            ats.GetCallHistory(1);

            Console.ReadKey();
        }
    }
}
