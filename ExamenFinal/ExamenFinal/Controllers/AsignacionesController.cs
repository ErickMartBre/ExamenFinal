using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ExamenFinal;

namespace ExamenFinal.Controllers
{
    public class AsignacionesController : Controller
    {
        private ConstructoraUHEntities db = new ConstructoraUHEntities();

        // GET: Asignaciones
        public ActionResult Index()
        {
            var asignaciones = db.Asignaciones.Include(a => a.Colaboradores).Include(a => a.Proyectos);
            return View(asignaciones.ToList());
        }

        // GET: Asignaciones/Details/5
        public ActionResult Details(int? Carnet, int? CodigoProyecto)
        {
            if (Carnet == null || CodigoProyecto == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asignaciones asignaciones = db.Asignaciones.Find(Carnet, CodigoProyecto);
            if (asignaciones == null)
            {
                return HttpNotFound();
            }
            return View(asignaciones);
        }

        // GET: Asignaciones/Create
        public ActionResult Create()
        {
            ViewBag.Carnet = new SelectList(db.Colaboradores, "Carnet", "PrimerNombre");
            ViewBag.CodigoProyecto = new SelectList(db.Proyectos, "CodigoProyecto", "NombreProyecto");
            return View();
        }

        // POST: Asignaciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Carnet,CodigoProyecto,FechaAsignacion")] Asignaciones asignaciones)
        {
            if (ModelState.IsValid)
            {
                db.Asignaciones.Add(asignaciones);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Carnet = new SelectList(db.Colaboradores, "Carnet", "PrimerNombre", asignaciones.Carnet);
            ViewBag.CodigoProyecto = new SelectList(db.Proyectos, "CodigoProyecto", "NombreProyecto", asignaciones.CodigoProyecto);
            return View(asignaciones);
        }

        // GET: Asignaciones/Edit/5
        public ActionResult Edit(int? Carnet, int? CodigoProyecto)
        {
            if (Carnet == null || CodigoProyecto == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asignaciones asignaciones = db.Asignaciones.Find(Carnet, CodigoProyecto);
            if (asignaciones == null)
            {
                return HttpNotFound();
            }
            ViewBag.Carnet = new SelectList(db.Colaboradores, "Carnet", "PrimerNombre", asignaciones.Carnet);
            ViewBag.CodigoProyecto = new SelectList(db.Proyectos, "CodigoProyecto", "NombreProyecto", asignaciones.CodigoProyecto);
            return View(asignaciones);
        }

        public ActionResult Delete(int? Carnet, int? CodigoProyecto)
        {
            if (Carnet == null || CodigoProyecto == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asignaciones asignaciones = db.Asignaciones.Find(Carnet, CodigoProyecto);
            if (asignaciones == null)
            {
                return HttpNotFound();
            }
            return View(asignaciones);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int Carnet, int CodigoProyecto)
        {
            Asignaciones asignaciones = db.Asignaciones.Find(Carnet, CodigoProyecto);
            db.Asignaciones.Remove(asignaciones);
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
