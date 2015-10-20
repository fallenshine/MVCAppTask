using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace MVCAppTask.Models
{
    public static class Wrappers
    {
        /// <summary>
        /// Help method to Wrap Objects that come from DataBase to Domain Objects
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static List<MortgagesDO> WrapMortgages (IEnumerable<Mortgage> data)
        {
            List<MortgagesDO> mortgageCollection = new List<MortgagesDO>();

            foreach(Mortgage item in data)
            {
                mortgageCollection.Add(new MortgagesDO(item));
            }

            return mortgageCollection;
        }
    }
}
