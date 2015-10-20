using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Description;
using DataAccess;
using MVCAppTask.Models;
using MVCAppTask.App_Code;

namespace MVCAppTask.Controllers
{
    public class VirtualController : ApiController
    {
        #region Entities

        private MunicipalityDBEntities entities = new MunicipalityDBEntities();

        #endregion

        #region GET

        /// <summary>
        /// Non Action Help Method for Getting All Land Properties
        /// </summary>
        /// <returns></returns>
        [NonAction]
        private async Task<List<LandProperty>> GetAllLandPropertiesAsync()
        {
            return await this.entities.LandProperties.ToListAsync();
        }

        /// <summary>
        /// Gets The Landings Properties
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetLandingProperties")]
        [ResponseType(typeof(List<LandPropertiesDO>))]
        public async Task<IHttpActionResult> GetLandingProperties()
        {
            try
            {
                List<LandProperty> allLandProperties = await this.GetAllLandPropertiesAsync();

                if (allLandProperties == null)
                {
                    return this.NotFound();
                }

                return this.Ok(Utilities.WrapLandPropertiesCollection(allLandProperties));
            }
            catch (Exception ex)
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        #endregion

        #region POST

        #region Land Properties

        /// <summary>
        /// Non Action Help Method for adding a Land Property
        /// </summary>
        /// <returns></returns>
        [NonAction]
        private async Task<bool> AddLandPropertyAsync(dynamic data)
        {
            LandProperty newLandProperty = new LandProperty();
            newLandProperty.Area = data.Area;
            newLandProperty.UPI = data.UPI;

            if (!String.IsNullOrWhiteSpace(data.Image))
            {
                newLandProperty.Image = data.Image;
            }

            string _firstName = data.Owner.FirstName;
            string _lastName = data.Owner.LastName;
            Owner existingOwner = entities.Owners.FirstOrDefault(o => o.FirstName == _firstName && o.LastName == _lastName);

            if (existingOwner != null)
            {
                newLandProperty.Owner = existingOwner;
            }
            else
            {
                Owner newOwner = new Owner();
                newOwner.FirstName = data.Owner.FirstName;
                newOwner.LastName = data.Owner.LastName;
                newOwner.Address = data.Owner.Address;
                newOwner.UserID = data.Owner.UserID;

                if (!String.IsNullOrWhiteSpace(data.Owner.Image))
                {
                    newOwner.Image = data.Owner.Image;
                }
                newLandProperty.Owner = newOwner;
            }

            this.entities.LandProperties.Add(newLandProperty);
            await this.entities.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Async POST method for communication with the client side.
        /// The method is for adding a land property
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionName("AddLandProperty")]
        [ResponseType(typeof(LandPropertiesDO))]
        public async Task<IHttpActionResult> AddLandProperty([FromBody]dynamic data)
        {
            try
            {
                if (!ModelState.IsValid || data == null)
                {
                    return this.BadRequest(this.ModelState);
                }

                return this.Ok<bool>(await this.AddLandPropertyAsync(data));
            }
            catch (Exception ex)
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        /// <summary>
        /// Non Action Help Method for Updating existing Land Property
        /// </summary>
        /// <returns></returns>
        [NonAction]
        private async Task<bool> UpdateLandPropertyAsync(dynamic data)
        {

            int dataID = data.Id;
            LandProperty entity = entities.LandProperties.FirstOrDefault(l => l.Id == dataID);

            if (entity != null)
            {
                entity.Area = data.Area;
                entity.UPI = data.UPI;
                entity.Image = data.Image;

                entity.Owner.FirstName = data.Owner.FirstName;
                entity.Owner.LastName = data.Owner.LastName;
                entity.Owner.Address = data.Owner.Address;
                entity.Owner.UserID = data.Owner.UserID;
                entity.Owner.Image = data.Owner.Image;

                await this.entities.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Async POST method for communication with the client side.
        /// The method is for updating a land property
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionName("UpdateLandProperty")]
        [ResponseType(typeof(LandPropertiesDO))]
        public async Task<IHttpActionResult> UpdateLandProperty([FromBody]dynamic data)
        {
            try
            {
                if (!ModelState.IsValid || data == null)
                {
                    return this.BadRequest(this.ModelState);
                }

                return this.Ok<bool>(await this.UpdateLandPropertyAsync(data));
            }
            catch (Exception ex)
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        /// <summary>
        /// Non Action Help Method for deleting an existing Land Property
        /// </summary>
        /// <returns></returns>
        [NonAction]
        private async Task<bool> DeleteLandPropertiyAsync(dynamic data)
        {
            int dataID = data.Id;
            LandProperty entity = entities.LandProperties.FirstOrDefault(l => l.Id == dataID);

            if (entity != null)
            {
                if (entity.Mortgages != null)
                {
                    this.entities.Mortgages.RemoveRange(entity.Mortgages);
                }

                this.entities.LandProperties.Remove(entity);
                await this.entities.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Async POST method for communication with the client side.
        /// The method is for deleting a land property
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionName("DeleteLandPropertiy")]
        [ResponseType(typeof(LandPropertiesDO))]
        public async Task<IHttpActionResult> DeleteLandPropertiy([FromBody]dynamic data)
        {
            try
            {
                if (!ModelState.IsValid || data == null)
                {
                    return this.BadRequest(this.ModelState);
                }

                return this.Ok<bool>(await this.DeleteLandPropertiyAsync(data));
            }
            catch (Exception ex)
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        #endregion

        #region Mortgages

        /// <summary>
        /// Non Action Help Method for adding a mortage
        /// </summary>
        /// <returns></returns>
        [NonAction]
        private async Task<bool> AddMortageAsync(dynamic data)
        {
            Mortgage newMortgage = new Mortgage();
            newMortgage.LandPropertiyID = data.LandPropertiyID;
            newMortgage.Date = data.Date;
            newMortgage.MoneyRecieved = data.MoneyRecieved;

            this.entities.Mortgages.Add(newMortgage);
            await this.entities.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Async POST method for communication with the client side.
        /// The method is for adding a mortgage
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionName("AddMortage")]
        [ResponseType(typeof(LandPropertiesDO))]
        public async Task<IHttpActionResult> AddMortage([FromBody]dynamic data)
        {
            try
            {
                if (!ModelState.IsValid || data == null)
                {
                    return this.BadRequest(this.ModelState);
                }

                return this.Ok<bool>(await this.AddMortageAsync(data));
            }
            catch (Exception ex)
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        /// <summary>
        /// Non Action Help Method for updating a mortgage
        /// </summary>
        /// <returns></returns>
        [NonAction]
        private async Task<bool> UpdateMortageAsync(dynamic data)
        {

            int dataID = data.LandPropertiyID;
            Mortgage entity = entities.Mortgages.FirstOrDefault(m => m.Id == dataID);

            if (entity != null)
            {
                entity.Date = data.Date;
                entity.MoneyRecieved = data.MoneyRecieved;

                await this.entities.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Async POST method for communication with the client side.
        /// The method is for updating a mortgage
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionName("UpdateMortage")]
        [ResponseType(typeof(LandPropertiesDO))]
        public async Task<IHttpActionResult> UpdateMortage([FromBody]dynamic data)
        {
            try
            {
                if (!ModelState.IsValid || data == null)
                {
                    return this.BadRequest(this.ModelState);
                }

                return this.Ok<bool>(await this.UpdateMortageAsync(data));
            }
            catch (Exception ex)
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        /// <summary>
        /// Non Action Help Method for deleting a mortgage
        /// </summary>
        /// <returns></returns>
        [NonAction]
        private async Task<bool> DeleteMortgageAsync(dynamic data)
        {
            int dataID = data.LandPropertiyID;
            Mortgage entity = entities.Mortgages.FirstOrDefault(m => m.LandPropertiyID == dataID);

            if (entity != null)
            {
                this.entities.Mortgages.Remove(entity);
                await this.entities.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Async POST method for communication with the client side.
        /// The method is for deleting a mortgage
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionName("DeleteMortgage")]
        [ResponseType(typeof(LandPropertiesDO))]
        public async Task<IHttpActionResult> DeleteMortgage([FromBody]dynamic data)
        {
            try
            {
                if (!ModelState.IsValid || data == null)
                {
                    return this.BadRequest(this.ModelState);
                }

                return this.Ok<bool>(await this.DeleteMortgageAsync(data));
            }
            catch (Exception ex)
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        #endregion

        #endregion

        #region Destructor

        protected override void Dispose(bool DoDispose)
        {
            if (DoDispose)
            {
                entities.Dispose();
            }
            base.Dispose(DoDispose);
        }

        #endregion

    }
}
