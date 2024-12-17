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
    public class ColaboradoresController : Controller
    {
        private ConstructoraUHEntities db = new ConstructoraUHEntities();

        // GET: Colaboradores
        public ActionResult Index()
        {
            return View(db.Colaboradores.ToList());
        }

        // GET: Colaboradores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Colaboradores colaboradores = db.Colaboradores.Find(id);
            if (colaboradores == null)
            {
                return HttpNotFound();
            }
            return View(colaboradores);
        }

        // GET: Colaboradores/Create
        public ActionResult Create()
        {
            ViewBag.Asignaciones = new SelectList(new List<string> { "Administrador", "Operario", "Peón" });
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Carnet,PrimerNombre,SegundoNombre,PrimerApellido,SegundoApellido,FechaNacimiento,Direccion,Telefono,Email,Salario,Categoria")] Colaboradores colaboradores)
        {
            if (colaboradores.Salario < 250000 || colaboradores.Salario > 500000)
            {
                ModelState.AddModelError("Salario", "El salario debe estar entre 250,000 y 500,000.");
            }

            if (ModelState.IsValid)
            {
                db.Colaboradores.Add(colaboradores);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Asignaciones = new SelectList(new List<string> { "Administrador", "Operario", "Peón" });
            return View(colaboradores);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Colaboradores colaboradores = db.Colaboradores.Find(id);
            if (colaboradores == null)
            {
                return HttpNotFound();
            }
            ViewBag.Asignaciones = new SelectList(new List<string> { "Administrador", "Operario", "Peón" });
            return View(colaboradores);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Carnet,PrimerNombre,SegundoNombre,PrimerApellido,SegundoApellido,FechaNacimiento,Direccion,Telefono,Email,Salario,Categoria")] Colaboradores colaboradores)
        {
            if (ModelState.IsValid)
            {
                db.Entry(colaboradores).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Asignaciones = new SelectList(new List<string> { "Administrador", "Operario", "Peón" });
            return View(colaboradores);
        }

        // GET: Colaboradores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Colaboradores colaboradores = db.Colaboradores.Find(id);
            if (colaboradores == null)
            {
                return HttpNotFound();
            }
            return View(colaboradores);
        }

        // POST: Colaboradores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Colaboradores colaboradores = db.Colaboradores.Find(id);
            db.Colaboradores.Remove(colaboradores);
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
