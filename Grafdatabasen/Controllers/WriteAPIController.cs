using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Grafdatabasen.Controllers
{
    public class WriteAPIController : ApiController
    {
        // GET: api/Write
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Write/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Write
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Write/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Write/5
        public void Delete(int id)
        {
        }

    }
}
