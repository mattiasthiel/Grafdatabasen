using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Grafdatabasen.Controllers
{
    public class GrafAPIController : ApiController
    {
        // GET: api/Graf
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Graf/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Graf
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Graf/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Graf/5
        public void Delete(int id)
        {
        }
    }
}
