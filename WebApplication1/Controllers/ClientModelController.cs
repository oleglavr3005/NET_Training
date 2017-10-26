using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ConsoleApplication1;
using WebApplication1.Models;
using AutoMapper;
using BusinessLogic.Logic;

namespace WebApplication1.Controllers
{
    public class ClientModelController : Controller
    {
        private ProductsContext db = new ProductsContext();

        // GET: ClientModel
        public ActionResult Index()
        {
            IList<ClientModel> clients = new List<ClientModel>();
            Mapper.Initialize(r => r.CreateMap<Client, ClientModel>());
            foreach (Client c in db.Clients.ToList())
            {
                if (!c.IsDeleted)
                clients.Add(Mapper.Map<Client, ClientModel>(c));
            }
            return View(clients);
        }

        // GET: ClientModel/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientModel clientModel = Mapper.Map < Client, ClientModel> (db.Clients.Find(id));
            if (clientModel == null)
            {
                return HttpNotFound();
            }
            return View(clientModel);
        }

        // GET: ClientModel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClientModel/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] ClientModel clientModel)
        {
            if (ModelState.IsValid)
            {
          
                Mapper.Initialize(r => r.CreateMap<ClientModel,Client>());
                Client client = Mapper.Map<ClientModel, Client>(clientModel);
                ClientValidation valid = new ClientValidation();
              if ( ! valid.IsValidate(client))
                {
                    ViewBag.Message = valid.ErrorMessage;
                    return View();
                }
                ViewBag.Message = valid.SuccessMessage;
                db.Clients.Add(client);
                db.SaveChanges();  
            }

            return View(clientModel);
        }

        // GET: ClientModel/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientModel clientModel = Mapper.Map < Client, ClientModel>( db.Clients.Find(id));
            if (clientModel == null)
            {
                return HttpNotFound();
            }
            return View(clientModel);
        }

        // POST: ClientModel/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] ClientModel clientModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clientModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(clientModel);
        }

        // GET: ClientModel/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientModel clientModel = Mapper.Map < Client, ClientModel>( db.Clients.Find(id));
            if (clientModel == null)
            {
                return HttpNotFound();
            }
            return View(clientModel);
        }

        // POST: ClientModel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClientModel clientModel = Mapper.Map < Client, ClientModel> (db.Clients.Find(id));
            if (clientModel == null)
            {
                return HttpNotFound();
            }
            clientModel.IsDeleted = true;
            db.Clients.Find(id).IsDeleted = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
