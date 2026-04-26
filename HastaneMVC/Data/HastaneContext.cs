using Microsoft.EntityFrameworkCore;
using HastaneMVC.Models; // Modellerimizi kullanabilmek için klasörün yolunu ekledik

namespace HastaneMVC.Data
{
    // Entity Framework'ün  Veri tabanı yöneticisi olması için "DbContext" sınıfından miras (kalıtım) alması şarttır.
    public class HastaneContext : DbContext
    {
        // Constructor (Yapıcı Metot). 
        // Sistemin "Hangi SQL Server'a, hangi şifreyle bağlanacağım?" ayarlarını (options) 
        // dışarıdan alıp içeriye (base) ilettiği yerdir.
        public HastaneContext(DbContextOptions<HastaneContext> options) : base(options)
        {
        }  

        // Dbsetler modelde oluşturduğumuz  structları veri tabanında verdiğimiz adla tabloya döker 

        public DbSet<BransModel> Branslar { get; set; }
        public DbSet<DoktorModel> Doktorlar { get; set; }
        public DbSet<HastaModel> Hastalar { get; set; }
        public DbSet<TahlilTurModel> TahlilTurleri { get; set; }
        public DbSet<RandevuModel> Randevular { get; set; }
        public DbSet<RandevuTahlilModel> RandevuTahlilleri { get; set; }
    }
}