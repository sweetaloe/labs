using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrderCakes.Web.Models;
using System.IO;
using System.Web.Hosting;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Data.Entity;

namespace Web.Controllers
{
    public class DonwloadController : Controller
    {
        public ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View(db.Orders.ToList());
        }

        // GET: Donwload

        public ActionResult Donwload(int? id)
        {

            // var g = ctx.YourTableName.Find(groupid);
            DbOrder dbOrder = db.Orders.Find(id);
            var g = db.Orders.Find(dbOrder.Id);

            ExcelPackage pkg;
            using (var stream = System.IO.File.OpenRead(HostingEnvironment.ApplicationPhysicalPath + "template.xlsx"))
            {
                pkg = new ExcelPackage(stream);
                stream.Dispose();
            }


            var worksheet = pkg.Workbook.Worksheets[1];

            worksheet.Name = g.FullName;

            worksheet.Cells[1, 2].Value = "Заказчик";
            worksheet.Cells[1, 3].Value = "Дата заказа";
            worksheet.Cells[1, 4].Value = "Дата выполнения";
            worksheet.Cells[1, 5].Value = "Цена";
            worksheet.Cells[1, 2, 1, 5].Style.Font.Bold = true;

            worksheet.Cells[2, 2].Value = g.FullName;
            worksheet.Cells[2, 3].Value = g.OrderDate.Date.ToShortDateString();
            worksheet.Cells[2, 4].Value = g.Deadline.Date.ToShortDateString();
            worksheet.Cells[2, 5].Value = g.Price;

            worksheet.Cells[4, 2].Value = "Заказ:";
            worksheet.Cells[4, 2].Style.Font.Bold = true;

            worksheet.Cells[5, 2].Value = "Форма Торта";
            worksheet.Cells[5, 3].Value = "Тип коржа";
            worksheet.Cells[5, 4].Value = "Кол-во ярусов";
            worksheet.Cells[5, 5].Value = "Начинка";
            worksheet.Cells[5, 6].Value = "Декорации";
            worksheet.Cells[5, 2, 5, 6].Style.Font.Bold = true;
            int str = 7;

            foreach (var cake in g.TypeCakes)
            {
                worksheet.Cells[str, 2].Value = cake.CakeShape;
                worksheet.Cells[str, 3].Value = cake.ShellType;
                worksheet.Cells[str, 4].Value = cake.NumberTiers;
                worksheet.Cells[str, 5].Value = cake.FillingType;

                foreach (var dec in cake.DecorationType)
                {
                    worksheet.Cells[str, 6].Value = dec.Decoration;
                    str++;
                }
                str++;

            }

            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

            // Заполнение файла Excel вышими данными
            var ms = new MemoryStream();
            pkg.SaveAs(ms);

            return File(ms.ToArray(), "application/ooxml", (g.FullName ?? "Без Названия").Replace(" ", "") + ".xlsx");
        }


        public ActionResult DonwloadAll()
        {
            var orders = db.Orders.ToList();
            //DbOrder dbOrder = db.Orders;
            //var g = db.Orders.Find(dbOrder.Id);

            ExcelPackage pkg;
            using (var stream = System.IO.File.OpenRead(HostingEnvironment.ApplicationPhysicalPath + "template.xlsx"))
            {
                pkg = new ExcelPackage(stream);
                stream.Dispose();
            }


            var worksheet = pkg.Workbook.Worksheets[1];

            worksheet.Name = "All Orders";

            int str = 1;


            foreach (var order in orders)
            {
                int id = order.Id;

                worksheet.Cells[str, 2].Value = "Заказчик";
                worksheet.Cells[str, 3].Value = "Дата заказа";
                worksheet.Cells[str, 4].Value = "Дата выполнения";
                worksheet.Cells[str, 5].Value = "Цена";

                worksheet.Cells[str,2,str,5].Style.Font.Bold = true;
                

                str++;
                worksheet.Cells[str, 2].Value = order.FullName;
                worksheet.Cells[str, 3].Value = order.OrderDate.Date.ToShortDateString();
                worksheet.Cells[str, 4].Value = order.Deadline.Date.ToShortDateString();
                worksheet.Cells[str, 5].Value = order.Price;
                
                str = str + 2;
               

                worksheet.Cells[str, 2].Value = "Заказ:";
                worksheet.Cells[str, 2].Style.Font.Bold = true;
                str++;
                
                worksheet.Cells[str, 2].Value = "Форма Торта";
                worksheet.Cells[str, 3].Value = "Тип коржа";
                worksheet.Cells[str, 4].Value = "Кол-во ярусов";
                worksheet.Cells[str, 5].Value = "Начинка";
                worksheet.Cells[str, 6].Value = "Декорации";
                worksheet.Cells[str, 2, str, 6].Style.Font.Bold = true;
                str = str + 2;

               

                foreach (var cake in order.TypeCakes)
                {
                    worksheet.Cells[str, 2].Value = cake.CakeShape;
                    worksheet.Cells[str, 3].Value = cake.ShellType;
                    worksheet.Cells[str, 4].Value = cake.NumberTiers;
                    worksheet.Cells[str, 5].Value = cake.FillingType;

                    foreach (var dec in cake.DecorationType)
                    {
                        worksheet.Cells[str, 6].Value = dec.Decoration;
                        str++;
                    }
                    str++;

                }

                str = str + 2;

            }

            

            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

            // Заполнение файла Excel вышими данными
            var ms = new MemoryStream();
            pkg.SaveAs(ms);

            return File(ms.ToArray(), "application/ooxml", ("Все_заказы").Replace(" ", "") + ".xlsx");
        }




    }
}
