using OrderCakes.Web.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrderCakes.Web.Controllers
{
    public class UploadController : Controller
    {
        // GET: Upload
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Print(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                var dto = OrderCakes.FileHelper.LoadFromStream(file.InputStream);

                using (var db = new ApplicationDbContext())
                {

                    var row = new DbOrder
                    {
                        FullName = dto.FullName,
                        OrderDate = dto.OrderDate,
                        Deadline = dto.Deadline,
                        Price = dto.Price,
                    };

                    row.TypeCakes = new Collection<DbCake>();

                   List< Collection<DbDecoration>> decorations = new List<Collection<DbDecoration>>();
                    
                    foreach (var cake in dto.TypeCakes)
                    {
                        decorations.Add(new Collection<DbDecoration>());

                        foreach (var dec in cake.DecorationType)
                        {
                            decorations[decorations.Count-1].Add(new DbDecoration {Decoration = (Models.Decoration) dec });
                        }

                        row.TypeCakes.Add(new DbCake
                        {
                            CakeShape = (Models.Shapes)cake.CakeShape,
                            FillingType = (Models.Filling)cake.FillingType,
                            NumberTiers = cake.NumberTiers,
                            ShellType = (Models.Shells)cake.ShellType,
                            DecorationType = decorations[decorations.Count-1]
                         });  
                    }



                    db.Orders.Add(row);
                    db.SaveChanges();
                }


                return View(dto);
            }

            return RedirectToAction("Index");
        }
    }
}