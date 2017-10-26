using AutoMapper;
using ConsoleApplication1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ClientsController : ApiController
    {
        // GET: api/Clients
        private ProductsContext db = new ProductsContext();
        public HttpResponseMessage Get()
        {
            var result = new List<ClientModel>();
            Mapper.Initialize(r => r.CreateMap<Client, ClientModel>());
            foreach (Client c in db.Clients.ToList())
            {
                if (!c.IsDeleted)
                    result.Add(Mapper.Map<Client, ClientModel>(c));
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);   
        }

        // GET: api/Clients/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Clients
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Clients/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Clients/5
        public void Delete(int id)
        {
        }
    }
}
