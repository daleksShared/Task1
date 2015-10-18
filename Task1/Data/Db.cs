using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class GetDb
    {
        public ModelAts ats {
            get
            {
                return new ModelAts(@"data source=(localdb)\MSSQLLocalDB;Initial Catalog=Task1;Integrated Security=True;");
            }

        }

    }
}
