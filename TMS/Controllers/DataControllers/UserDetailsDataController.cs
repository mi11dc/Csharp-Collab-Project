using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TMS.Models;

namespace TMS.Controllers.DataControllers
{
    public class UserDetailsDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        [ResponseType(typeof(UserDetailDto))]
        public IHttpActionResult GetUserDetails(string Id)
        {
            UserDetail userDetail = db.UserDetails.Where(u => u.UserId == Id).FirstOrDefault();
            
            if (userDetail == null || String.IsNullOrEmpty(userDetail.Id.ToString()))
                return Ok();

            UserDetailDto userDetailDtos = new UserDetailDto() 
            {
                Id = userDetail.Id,
                FName = userDetail.FName,
                LName = userDetail.LName,
                Country = userDetail.Country,
                BasePrice = userDetail.BasePrice,
                DOB = userDetail.DOB,
                UserId = userDetail.UserId
            };
            return Ok(userDetailDtos);
        }

        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateUserDetail(int Id, UserDetail userDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (Id != userDetail.Id)
            {
                return BadRequest();
            }

            db.Entry(userDetail).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserDetailExists(Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [ResponseType(typeof(UserDetail))]
        public IHttpActionResult PostUserDetail(UserDetail userDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UserDetails.Add(userDetail);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { Id = userDetail.Id }, userDetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserDetailExists(int Id)
        {
            return db.UserDetails.Count(e => e.Id == Id) > 0;
        }
    }
}
