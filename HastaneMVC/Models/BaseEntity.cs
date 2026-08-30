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


        //yapılacaklar listesi

        // toast success, toast error  branscontrollerda ve hasta controllerda var saddece                 TempData["success"] = "işlem başarılı"; bu eklenecek post metodlara 

        // randevu tarih default da ona min değer atanabilir html ile  bu yapıldı emin nasıl uygulandığını gösterdi.

        //service mimarisini araştır                                            program.cs de build etmeyi unutma
        // n katmanlı mimarileri araştır  N-Tier Architecture / Service Layer
        // şuan sadece doktorcontroller ve randevucontroller services yapısını kullanıyor diğerlerinde yok basecontroller 

        // ajax kodunu   randevutahlil e gerekli   randevu da mevcut 

        // middleware request in response dönüştüğü yol üzerinde bulunur  burada katmanlar oluşturur   pipiline bu yolculuğun adı 
        // her katmanda  requestler değerlendirilir ve response olarak değerlendirilir  değerlnedirme true ise next ile diğer katmana yönlendirir
        // middle ware  iç içe deir   1-[ 2-[ -3[iç içeler ]3- ]2- ]1-
        // program.cs deki configure metodu içerisinde middleware ler çağrılır  sırayla çalışır  sırayla değerlendirilir  sırayla response döner
        //

        // middleware   son  olarak buna baK request ile response arasına farklı işlemler eklemek doğralama işlemi erişim işlemi 
        //logic  next()  more logic bunlar içe içe döngüler kendisi bitmeden bir sonrakini tetikler recursive fonksiyon benzeri
        // en sonda response döner 
        //configure metodu içerisinde middleware ler çağrılır asp.net core mimarisinde tumm midleware ler Use adiyla başlar tetiklenme sırası önemli 
        // ilk kim olduğunu öğrenme autechnation sonra yetkileri authorization
        // hazır middleware ler Run Use Map MapWhen  
        // run kendisinden sonraki middleware i teiklemez pipline devam etmez    akışı bitirir 
        // middlware lerin içine consel.writline yazıp consoleda gösterebilirsin    
        // use sonrakini çağırı 
        // map filtreleme için if bloğu var gelen talebe göre use a if açılıp da kullanılabilir bunun yerine 


        //datatable -- bir adet dto veya viewmodel oluşturup onun üzerinden datatable yapalım  o sayfada silinenler gözüksün ve oradan silinenleri geri getirelim ve mvc controller  with wiews with controller olanı aç  bu youtube kanalında var oraya
        // bütün hastalar ve doktorların gözüktüğü işlem yapamadığımız bir sayfa yapalım 
        // VM de KayitliKisiler aç yeni bir controller aç doktor hasta kim var kim yok gösterilecek güncelleme yapılcak bir yer açalım  toplam hasta sayisi  doktor sayisi belli olsun orada bunlarla ilgili işlemler yapalım viewsini oluştur sonra  burada isdeleted kısımı var true false  false olanları yeniden diriltelim ve 




    }
}








// Her sayfanın kendine özel bir ViewModel'iolsun
// alınan tc yi uniqe yap eşşiz olsun  mevcut kayıtlarla karşılaştır ona göre ekleme yaptırt controller da 
// Base Repository (Interface)
