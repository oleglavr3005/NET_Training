using AutoMapper;
using ConsoleApplication1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Routing;
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
        public HttpResponseMessage Get(int id)
        {
            Client client = db.Clients.ToList().Find(c => c.ID == id);
            ClientModel cl;
            if (client == null) return Request.CreateResponse(HttpStatusCode.NotFound);
            cl =Mapper.Map<Client, ClientModel>(client);
            var result= new { ID = cl.ID,Name=cl.Name, Order= "<a href =" + "/api/clients/"+id+"/orders"+ ">"+Url.Request.RequestUri.GetLeftPart(UriPartial.Authority) + "/api/clients /"+id+"/orders</a>" };
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // POST: api/Clients
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Clients/5
        public void Put(int id, [FromBody]string value)
        {
        }
        [Route("api/clients/{id}/orders")]
        [HttpGet]
        public HttpResponseMessage GetOrders(int id)
        {
            Client client = db.Clients.ToList().Find(c => c.ID == id);
            IQueryable<Order> orders = (from od in db.Orders where od.ClientID == id orderby od.ID select od)
               .Select(o => o);
            ClientModel cl;

            return Request.CreateResponse(HttpStatusCode.OK, orders);
        }
        // DELETE: api/Clients/5
        public void Delete(int id)
        {
        }
    }
}
