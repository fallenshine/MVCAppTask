using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess;

namespace MVCAppTask.Models
{
    public class LandPropertiesDO
    {
        public int Id { get; set; }
        public string Area { get; internal set; }
        public string UPI { get; internal set; }
        public string Image { get; internal set; }
        public OwnersDO Owner { get; internal set; }
        public List<MortgagesDO> Mortgages { get; internal set; }

        public LandPropertiesDO(LandProperty landProperty)
        {
            this.Id = landProperty.Id;
            this.Area = landProperty.Area;
            this.UPI = landProperty.UPI;
            this.Image = landProperty.Image;
            this.Owner = landProperty.Owner == null ? null : new OwnersDO(landProperty.Owner);
            this.Mortgages = landProperty.Mortgages == null ? null : Wrappers.WrapMortgages(landProperty.Mortgages);
        }
    }
}