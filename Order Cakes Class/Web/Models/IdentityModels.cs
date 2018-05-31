using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
//using Web.Migrations;



namespace OrderCakes.Web.Models
{
    // Чтобы добавить данные профиля для пользователя, можно добавить дополнительные свойства в класс ApplicationUser. Дополнительные сведения см. по адресу: http://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Здесь добавьте утверждения пользователя
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<DbOrder> Orders { get; set; }
        public DbSet<DbCake> Cakes { get; set; }
        public DbSet<DbDecoration> Decorations { get; set; }
    }

    public class DbOrder
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FullName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime OrderDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Deadline { get; set; }
        public virtual Collection<DbCake> TypeCakes { get; set; }
        public string Price { get; set; }
    }

    public class DbCake
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public Shapes CakeShape { get; set; }

        public Shells ShellType { get; set; }

        public int NumberTiers { get; set; }

        public Filling FillingType { get; set; }

        public virtual Collection<DbDecoration> DecorationType { get; set; }
    }

    public class DbDecoration
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public Decoration Decoration { get; set; }
    }

    public enum Shapes
    {
        Circle,
        Diamond,
        Elipse,
        Heart,
        Rectangle,
        Square,
        Star,
        Triangle
    }

    public enum Shells
    {
        Biscuit,
        Curd,
        Short,
        Waffle
    }

    public enum Filling
    {
        Chocolate,
        CondensedMilk,
        Custard,
        Fruit,
        Nuts,
        SourCream,
        Vanilla,
        Yoghurt
    }

    public enum Decoration
    {
        Cream,
        CulinaryMastic,
        Fondant,
        Glaze
    }
}