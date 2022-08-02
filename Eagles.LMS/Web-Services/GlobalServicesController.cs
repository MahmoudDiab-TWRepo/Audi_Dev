using Eagles.LMS.BLL;
using Eagles.LMS.Helper;
using Eagles.LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Eagles.LMS.Web_Services
{
    public class GlobalServicesController : ApiController
    {

        [HttpPost]
        [Route("api/GlobalServices/RemoveCarImg")]
        public async Task<IHttpActionResult> EditUser(int id)
        {
            UnitOfWork ctx = new UnitOfWork();
            var CarImg = ctx.CarImagesManager.GetBy(id);
            if (CarImg != null)
            {
                ctx.CarImagesManager.Delete(CarImg);
                return Ok();
            }
            return NotFound();
        }


    }






}
