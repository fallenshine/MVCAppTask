using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCAppTask.Models;
using DataAccess;

namespace MVCAppTask.App_Code
{
    public class Utilities
    {
        /// <summary>
        /// Creates a Collection of the LandProperties Domain  Objects.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static List<LandPropertiesDO> WrapLandPropertiesCollection(IEnumerable<LandProperty> data)
        {
            List<LandPropertiesDO> returnResult = new List<LandPropertiesDO>();
            foreach (LandProperty d in data)
            {
                if(d != null)
                {
                    returnResult.Add(new LandPropertiesDO(d));
                }
            }

            return returnResult;
        }
    }
}