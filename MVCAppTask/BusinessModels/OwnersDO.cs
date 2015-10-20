using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess;

namespace MVCAppTask.Models
{
    public class OwnersDO
    {
        private int Id { get; set; }
        public string FirstName { get; internal set; }
        public string LastName { get; internal set; }
        public string Address { get; internal set; }
        public string UserID { get; internal set; }
        public string Image { get; internal set; }

        internal OwnersDO(Owner owner)
        {
            this.Id = owner.Id;
            this.FirstName = owner.FirstName;
            this.LastName = owner.LastName;
            this.Address = owner.Address;
            this.UserID = owner.UserID;
            this.Image = owner.Image;
        }

    }
}