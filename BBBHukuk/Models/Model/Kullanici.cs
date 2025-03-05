using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BBBHukuk.Models.Model
{
    [Table("Kullanici")]
    public class Kullanici
    {

        [Key]
        public int KullaniciId { get; set; }
        [Required]
        public string KullaniciAd {  get; set; }
        [Required]
        public string Sifre { get; set; }

    }
}