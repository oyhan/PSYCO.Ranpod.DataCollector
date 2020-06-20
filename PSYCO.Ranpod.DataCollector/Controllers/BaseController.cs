using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PSYCO.Ranpod.DataCollector.Controllers
{
    [Route("api/[controller]/[action]/{id?}")]
    [ApiController]
    public class BaseController : ControllerBase
    {
       



        public ActionResult HandleException(Exception ex)
        {
            if (ex is DbUpdateException)
            {
                return StatusCode(500, "این آیتم به دلیل وابستگی در سیستم قابل حذف نمی باشد");
            }
            return StatusCode(500, ex.ToString());

        }
        public BaseController()
        {

        }
    }
}