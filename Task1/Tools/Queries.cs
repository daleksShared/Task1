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
        public void GetCallHistory(int callerId)
        {
            Console.WriteLine("Направление" + "\t" + "Источник" + "\t" + "Цель" + "\t" + "Время:" + "\t" +
                              "Продолжительность" + "\t" + "Цена");
            if (CallHistory == null || CallHistory.Count() == 0) return;
            var callHistory =
                CallHistory.Where(
                    x =>
                        (
                            (x.Caller.Id == callerId || x.Callee.Id == callerId)
                            & x.StarTime.Date >= DateTime.Today.AddDays(-30)
                            )
                    )
                    .OrderByDescending(x => x.StarTime)
                    .Select(x => new
                    {
                        direction = x.Caller.Id == callerId ? "output" : "input",
                        callerID = x.Caller.Id,
                        calleID = x.Callee.Id,
                        x.StarTime,
                        x.Duration,
                        cost = BillingCalls.Where(y => y.CallerId == callerId && y.StarTime == x.StarTime)
                                .Select(y => new { cost = ((float)y.Duration.TotalSeconds * y.Contract.Tarrif.MinuteCost) })
                                .FirstOrDefault().cost
                    })
                    .ToList();
            foreach (var item in callHistory)
            {
                var cost = BillingCalls.Where(y => y.CallerId == callerId && y.StarTime == item.StarTime)
                                .Select(y => new { cost = ((float)y.Duration.TotalSeconds * y.Contract.Tarrif.MinuteCost) })
                                .FirstOrDefault().cost;
                Console.WriteLine(item.direction + "\t" + item.callerID.ToString() + "\t" + item.calleID + "\t" +
                                  item.StarTime.ToString("yyyy-M-d hh:mm:ss") + "\t" + item.Duration.TotalSeconds + "\t" +
                                 item.cost.ToString("C"));
            }
        }

       
    }
}
