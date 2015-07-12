using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace My_Site.Models
{
    public class SparePart
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите название игры")]
        [Display(Name = "Производитель")]
        public string Mark { get; set; }

        [Display(Name = "Модель")]
        public string Model { get; set; }

        [Required]
        [Display(Name = "Категория")]
        public string Category { get; set; }

        [Display(Name = "Подкатегория")]
        public string UnderCategory { get; set; }

        [Required]
        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Введите целое положительное число")]
        [Display(Name = "В наличии")]
        public int Quantity { get; set; }

        public byte[] ImageData { get; set; }

        public string MarkWithModel { get { return Mark + " " + Model; } }
    }
}