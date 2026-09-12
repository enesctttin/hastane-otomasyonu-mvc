# Hastane Otomasyonu ve ASP.NET Core Çalışmaları

İTÜ Bilgi İşlem Daire Başkanlığı — Yazılım Geliştirme Grubu (YGG) Yazılım
Geliştirme Proje Sınıfı'nın altıncı ve yedinci aşamaları kapsamında geliştirilen
beş projelik bir solution.

Ana proje `HastaneMVC`: hasta kaydından tahlil takibine kadar hastane işleyişini
yöneten bir web uygulaması. Yanındaki dört proje ise bu uygulamayı geliştirmeden
önce ASP.NET Core'un temel mekanizmalarını ayrı ayrı öğrenmek için yazıldı —
middleware, dependency injection, yönlendirme ve ortam yapılandırması.

## Solution yapısı

| Proje | Konu |
|---|---|
| **`HastaneMVC`** | Ana uygulama — randevu ve tahlil otomasyonu |
| `MiddlewareExample.Web` | Middleware pipeline ve özel middleware yazımı |
| `dependency_injection` | Bağımlılık enjeksiyonu ve servis yaşam döngüleri |
| `CustomRouteHandler` | Controller dışına özel yönlendirme |
| `environment` | Ortam yapılandırması ve ortama göre davranış |

---

# HastaneMVC — Ana Proje

Doktorlar branşlarına göre tanımlanıyor, hastalara randevu oluşturuluyor ve her
randevuya birden fazla tahlil bağlanabiliyor.


## Kullanılan teknolojiler

- .NET 8 / ASP.NET Core MVC
- Entity Framework Core (Code First, migration'larla)
- MS SQL Server (LocalDB)
- Razor Views, Bootstrap, jQuery, AJAX

## Öne çıkan özellikler

**Katmanlı yapı.** Controller'lar veritabanına doğrudan gitmiyor; `BaseService`
üzerinden türeyen servis sınıfları veri erişimini üstleniyor ve `Program.cs`
içinde dependency injection ile kaydediliyor. Birden fazla controller'ın
ihtiyaç duyduğu ortak veri — branş, doktor, hasta listeleri — `BaseController`
içinde toplanarak tekrar önlendi.

**Soft delete.** Tüm entity'ler `BaseEntity` sınıfından türüyor; `IsDeleted`,
`CreateTime` ve `DeleteTime` alanları sayesinde silme işlemi kaydı
veritabanından kaldırmıyor, pasife çekiyor. Listeleme sorguları
`IsDeleted == false` filtresiyle çalıştığı için silinen kayıtlar geri
getirilebilir durumda kalıyor.

**DTO ve ViewModel ayrımı.** Entity'ler doğrudan view'a taşınmıyor. Sorgular
LINQ projeksiyonu ile doğrudan DTO'ya map ediliyor, böylece veritabanından
yalnızca ihtiyaç duyulan kolonlar çekiliyor. Form sayfaları için ayrıca
`RandevuEkleVM`, `DoktorEkleVM` gibi ViewModel'ler kullanılıyor.

**Özel yönlendirme.** `CustomConstraint` sınıfıyla route parametrelerine kısıt
tanımlanıyor, ayrıca Türkçe URL'ler için ek route tanımları yapıldı.

**Kullanıcı geri bildirimi.** İşlem sonuçları `TempData` üzerinden taşınan Toast
bildirimleriyle gösteriliyor. Randevu ekranlarında branş seçimine göre doktor
listesi AJAX ile güncelleniyor.

## Veri modeli

| Tablo | Açıklama |
|---|---|
| `Branslar` | Doktor branşları |
| `Doktorlar` | Branşa bağlı doktor kayıtları |
| `Hastalar` | Hasta kayıtları |
| `Randevular` | Hasta, doktor, tarih/saat ve doktor notu |
| `TahlilTurleri` | Tanımlı tahlil türleri |
| `RandevuTahlilleri` | Randevu ile tahlil türü arasındaki ara tablo |

Randevu ile tahlil türü arasındaki çoka-çok ilişki `RandevuTahlilleri` ara
tablosu üzerinden kuruldu. Şema beş migration boyunca geliştirildi. Altı
varlığın tamamı için tam CRUD ekranları mevcut: listeleme, ekleme, güncelleme,
silme.


## Kurulum

```bash
git clone https://github.com/enesctttin/hastane-otomasyonu-mvc.git
cd hastane-otomasyonu-mvc/HastaneMVC
dotnet restore
dotnet ef database update
dotnet run
```

Bağlantı `appsettings.json` içindeki `HastaneBaglantisi` anahtarında tanımlı ve
LocalDB'yi işaret ediyor.

## Geliştirilebilecek yönler

- Kimlik doğrulama ve rol bazlı yetkilendirme (doktor / sekreter / yönetici)
- Repository pattern ile veri erişiminin tam soyutlanması
- Aynı doktora aynı saatte ikinci randevu verilmesini engelleyen iş kuralı
- Silinen kayıtların listelendiği ve geri alınabildiği yönetim ekranı
- Servis katmanının tüm controller'lara yaygınlaştırılması


---

# Kavram Çalışmaları

Ana projeye geçmeden önce ASP.NET Core'un temel mekanizmalarını izole biçimde
öğrenmek için yazılan projeler. Her biri tek bir konuya odaklanıyor ve
`Program.cs` dosyaları konuyu açıklayan yorum satırlarıyla birlikte tutuluyor.

## MiddlewareExample.Web

Request'in response'a dönüşürken geçtiği pipeline'ın incelendiği proje.
`Use`, `Run`, `Map` ve `MapWhen` arasındaki farklar denendi; middleware'lerin
tetiklenme sırasının neden önemli olduğu — önce kimlik doğrulama, sonra
yetkilendirme — pratikte görüldü.

Çalışmanın somut çıktısı `Middlewares/WhiteIpAddressControlMiddleware.cs`:
gelen isteğin IP adresini tanımlı beyaz listeyle karşılaştıran, listede yoksa
isteği pipeline'ın devamına geçirmeyen özel bir middleware.

## dependency_injection

Servislerin container'a kaydedilmesi ve yaşam döngülerinin karşılaştırılması.

`ILog` arayüzü tanımlandı, `Consolelog` ve `Textlog` olmak üzere iki
implementasyon yazıldı. Böylece controller'ın somut sınıfa değil arayüze bağlı
kalması sağlandı — loglamanın konsola mı dosyaya mı yapılacağı yalnızca
`Program.cs` içindeki kayıt satırı değiştirilerek belirlenebiliyor.

Üç yaşam döngüsü de ayrı ayrı denendi: `AddSingleton` uygulama boyunca tek
nesne, `AddScoped` her istek için ayrı nesne, `AddTransient` her talepte yeni
nesne üretiyor. `ServiceDescriptor` ile manuel kayıt yöntemi de denenip
kısayol metotlarıyla karşılaştırıldı.

## CustomRouteHandler

Gelen isteğin controller yerine başka bir sınıfa yönlendirilmesi. `Handlers/`
klasöründe `ExampleHandler` ve `ImageHandler` sınıfları bulunuyor; endpoint
tanımları doğrudan `Program.cs` içinde yapılıyor.

View render etmeyen, veritabanından veri döndürmeyen basit uç noktalarda
controller mekanizmasının gereksiz ağır kaldığı durumlara bir alternatif olarak
incelendi.

## environment

Uygulamanın hangi ortamda çalıştığının (`Development`, `Staging`, `Production`)
belirlenmesi ve ortama göre farklı davranış tanımlanması. `IWebHostEnvironment`
üzerinden ortam kontrolü ve `launchSettings.json` ile ortam değişkeni
yapılandırması denendi.

## Çalıştırma

Her proje bağımsız çalışıyor:

```bash
dotnet run --project MiddlewareExample.Web
dotnet run --project dependency_injection
dotnet run --project CustomRouteHandler
dotnet run --project environment
```
