namespace dependency_injection;

using dependency_injection.Services;
using Microsoft.Extensions.DependencyInjection; // IServiceCollection için gerekli kutup
    public class Example
    {  // services koleksiyonu bir container
        public Example()
        { // services koleksiyonu bir container
            // ıos'i yapılandırmasını barındıran bir türdür
            
            IServiceCollection servisler = new ServiceCollection();  // built - in IoC  
        // bunun içerisine sistemde hazır gelen servisleri ekleyebiliriz   ilgili servisin  sınıflarını kullanabiliriz böylece mimariye eklenir  container a dahil olur
        servisler.AddAuthentication();
        servisler.Add(new ServiceDescriptor(typeof(Consolelog), new Consolelog(2))); // kendi yazdığımız servisler böyle eklenir  
        servisler.Add(new ServiceDescriptor(typeof(Textlog), new Textlog()));  // koyulacak nesnenin çalışacak ilk nesnenin  tipi belirtilmeli 

        // bunları nasıl elde edicez
        ServiceProvider provider=  servisler.BuildServiceProvider();  // somut container / provider / sağlayıcı
        provider.GetService < Consolelog >();
        provider.GetService<Textlog>();



    }

}

