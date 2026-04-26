using System.ComponentModel.DataAnnotations;

namespace HastaneMVC.Models
{
    public abstract class BaseEntity // abstract classından öğe oluşutma sadece miras verdir bu soyut bir şey böyle bir şey yok bundan bir öğe oluşturamayız 
    {
        [Key]
        public int Id { get; set; }
        public Boolean IsDeleted { get; set; }=false;

        public DateTime CreateTime { get; private set; } = DateTime.Now;

        public DateTime? DeleteTime { get; set; }


        // ajax kodunu güncelle randevu kısmı  tag helpersa bak

        // VM de KayitliKisiler ver yeni bir controller aç doktor hasta kim var kim yok gösterilecek güncelleme yapılcak bir yer açalım  toplam hasta sayisi  doktor sayisi belli olsun orada bunlarla ilgili işlemler yapalım viewsini oluştur sonra  burada isdeleted kısımı var true false  false olanları yeniden diriltelim ve 

        //  date lerde sıkıntı var ekranda gözüken ve kayıt kısmında modelde kayıt kısmını zaten dolduruyoruz ekle bir daha girmeye gerek yok ,
        //  constructer mantığı ile oto doldurması lazım ama kontrol et

        // bütün hastalar ve doktorların gözüktüğü işlem yapamadığımız bir sayfa yapalım 

        // toast success, toast error  EN SON BU 

        //datatable 

        //service mimarisini araştır

        // n katmanlı mimarileri araştır  N-Tier Architecture / Service Layer

        // Her sayfanın kendine özel bir ViewModel'iolsun

        // middleware   son  olarak buna baK

        // Base Repository (Interface)

    }
}
