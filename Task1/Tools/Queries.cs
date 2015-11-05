using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{

   

    public partial class ModelAts
    {
        
        public string AtsConnectionString(string sqlConString
        )
        {

            var entityBuilder = new EntityConnectionStringBuilder
            {
                Provider = "System.Data.SqlClient",
                ProviderConnectionString = sqlConString
                //,Metadata = @"res://*/Trade.csdl|res://*/Trade.ssdl|res://*/Trade.msl"
            };

            return entityBuilder.ToString();
        }
        

       
    }
}
