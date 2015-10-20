using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess;

namespace MVCAppTask.Models
{
    public class MortgagesDO
    {
        private int Id { get; set; }
        public int LandPropertiyID { get; internal set; }
        public DateTime Date { get; internal set; }
        public decimal MoneyRecieved { get; internal set; }

        internal MortgagesDO (Mortgage mortgage)
        {
            this.Id = mortgage.Id;
            this.LandPropertiyID = mortgage.LandPropertiyID;
            this.Date = mortgage.Date;
            this.MoneyRecieved = mortgage.MoneyRecieved;
        }
    }
}