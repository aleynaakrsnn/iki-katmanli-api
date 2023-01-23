using System.ComponentModel.DataAnnotations;

namespace webmvcclientuyg.Models
{
    public class Musteriler
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Lütfen isim giriniz")]
        public string AdSoyad { get; set; }

        [Required(ErrorMessage = "Lütfen adres giriniz")]
        public string Adres { get; set; }

        [Required(ErrorMessage = "Lütfen telefon giriniz")]
        public string Telefon { get; set; }

        [Required(ErrorMessage = "Lütfen email giriniz")]
        public string Email { get; set; }
    }
}
