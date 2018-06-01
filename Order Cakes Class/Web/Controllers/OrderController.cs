using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using OrderCakes.Web.Models;

namespace Web.Controllers
{
    public class OrderController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Order
        public ActionResult Index()
        {
            return View(db.Orders.ToList());
        }

        // GET: Order/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DbOrder dbOrder = db.Orders.Find(id);
            if (dbOrder == null)
            {
                return HttpNotFound();
            }
            return View(dbOrder);
        }


        // GET: Order/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DbOrder dbOrder = db.Orders.Find(id);
            if (dbOrder == null)
            {
                return HttpNotFound();
            }
            return View(dbOrder);
        }

        // POST: Order/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FullName,OrderDate,Deadline,Price")] DbOrder dbOrder)
        {

            if (ModelState.IsValid)
            {
                db.Entry(dbOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dbOrder);
        }


        // GET: Order/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DbOrder dbOrder = db.Orders.Find(id);
            if (dbOrder == null)
            {
                return HttpNotFound();
            }
            return View(dbOrder);
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            DbOrder dbOrder = db.Orders.Find(id);


           // db.Decorations.Remove(dbDec);
//db.Cakes.Remove(dbCake);
            db.Orders.Remove(dbOrder);

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
