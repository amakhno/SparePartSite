using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace My_Site.Models
{
    public class Adress
    {
        [Required(ErrorMessage = "Введите страну")]
        [Display(Name = "Страна")]
        public string Country { get; set; }

        [Display(Name = "Область")]
        public string Region { get; set; }

        [Required(ErrorMessage = "Введите город")]
        [Display(Name = "Город")]
        public string City { get; set; }

        [Required(ErrorMessage = "Введите индекс")]
        [Range(1, int.MaxValue, ErrorMessage = "Ошибка ввода индекса")]
        [Display(Name = "Индекс")]
        public int ZipCode { get; set; }

        [Required(ErrorMessage = "Введите улицу")]
        [Display(Name = "Улица")]
        public string House { get; set; }

        [Required(ErrorMessage = "Введите номер дома")]
        [Display(Name = "Дом")]
        public string House { get; set; }

        [Display(Name = "Квартира")]
        public string Appartments { get; set; }
    }
}