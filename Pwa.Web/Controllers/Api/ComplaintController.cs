using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Pwa.Web.Controllers.Api
{
    public class ComplaintController : ApiController
    {

        
        [HttpGet]
        public IHttpActionResult Hello()
        {
            
            return Ok(new
            {
                Success = true,
                Code = "200",
                Message = "Success",
                Result = "Hello world V1"
            });
        }
        
        [HttpGet]
        public async Task<IHttpActionResult> Hello2()
        {

            return await Task.FromResult(  Ok(new
            {
                Success = true,
                Code = "200",
                Message = "Success",
                Result = new string[] { "1","20","300"}
            }));
        }
    }
}
