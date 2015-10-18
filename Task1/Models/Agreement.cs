using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Task1
{
    public partial class Contract
    {
        public int Id { get; set; }
        private string number="";
        public string Number
        {
            get { return number; }
            set
            {
                if (value == string.Empty)
                {
                    throw new System.ArgumentException("Номер договора не может быть пустым!!!");
                }
                else
                {
                    number = value;
                }
            }
        }

        public DateTime ContractDate { get; set; }
        public ContractType AgreementType { get; set; }
        public Tarrif Tarrif { get; set; }

    }

    public class CallTarrif
    {
        public string Name { get; set; }
        public float MinuteCost{ get; set; }
    }

    public class Customer
    {
        
    }
    public enum ContractType
    {
        Debet,
        Credit
    }

    public class Tarrif
    {
        public int Id { get; set; }
        public string Name { get; set;}
        public float MinuteCost { get; set; }
    }

}
