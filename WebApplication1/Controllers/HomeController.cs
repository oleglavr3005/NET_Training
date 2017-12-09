using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using ConsoleApplication1;
using Web.Support;
using System.Data;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Mapper.Initialize(r=>r.CreateMap<Client, ClientModel>());
            var dbContext = new ProductsContext();
            //   var clientList = SimpleInjectorService.dbctx.Clients; 
            var clientList = from Clients in dbContext.Clients select Clients;
               var clients = new List<ClientModel>();
         
                foreach (var client in clientList)
                {
                    ClientModel clientModel =
                      Mapper.Map<Client,ClientModel>(client);
                    clients.Add(clientModel);
                }
     //       return View(clients);
            return View("../ClientModel/ManageClients");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Create(ClientModel clientDetails)
        {
            try
            {
                Mapper.Map<ClientModel,Client>(clientDetails);
                var dbContext = new ProductsContext();
                var client = Mapper.Map<ClientModel,Client>(clientDetails);
                dbContext.Clients.Add(client);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}