using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderCakes
{
    public class CakeRequest
    {

        /// <summary>
        /// ФИО заказчика
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Дата заказа
        /// </summary>
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// День к которому нужно выполнить заказ
        /// </summary>
        public DateTime Deadline { get; set; }

        /// <summary>
        /// Тип торта
        /// </summary>
        public Types TypeCakes { get; set; }

        /// <summary>
        /// Стоимость
        /// </summary>
        public double Price { get; set; }
    }

    /// <summary>
    /// Типы тортов
    /// </summary>
    public class Types
    {
        /// <summary>
        /// Форма торта
        /// </summary>
        public Shapes CakeShape { get; set; }
        /// <summary>
        /// Тип коржа
        /// </summary>
        public Shells ShellType { get; set; }
        /// <summary>
        /// Количество ярусов
        /// </summary>
        public int NumberTiers { get; set; }
        /// <summary>
        /// Тип начинки
        /// </summary>
        public Filling FillingType { get; set; }
        /// <summary>
        /// Оформление торта
        /// </summary>
        public Decoration DecorationType { get; set; }

    }

    /// <summary>
    /// Виды форм
    /// </summary>
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

    /// <summary>
    /// Виды коржей
    /// </summary>
    public enum Shells
    {
        Biscuit,
        Curd,
        Short,
        Waffle
    }
    /// <summary>
    /// Виды начинки
    /// </summary>
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
    /// <summary>
    /// Виды оформления
    /// </summary>
    public enum Decoration
    {
        Cream,
        CulinaryMastic,
        Fondant,
        Glaze
    }
}
