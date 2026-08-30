using ImageMagick;

namespace CustomRouteHandler.Handlers
{
    public class ImageHandler  // buraya istediğin adı ver resimkucultucu
    {
        public RequestDelegate Handler(string filePath)
        // filePath resim dosyasının konumunu bize getirecek
        {// task dönüş tipi varsa async dönmeli
            return async c =>
            {
                // resimler image video static dosylara wwwroot da yer alır
                // wwwroot daki öğelere erişmek için  app.UseStaticFiles()   program.cs de çalışmalı
                // resim tipine göre type hazırlanmalı 

                FileInfo fileInfo = new FileInfo($"{filePath}\\{c.Request.RouteValues["imageName"].ToString()}");
                using MagickImage magick = new MagickImage(fileInfo);  //MagickImage  kullanacağımız kutuphane



                int width = (int)magick.Width, heigth = (int)magick.Height;

                if (!string.IsNullOrEmpty(c.Request.Query["w"].ToString())) 
                width = int.Parse(c.Request.Query["w"].ToString());
                if (!string.IsNullOrEmpty(c.Request.Query["h"].ToString())) 
                heigth = int.Parse(c.Request.Query["h"].ToString()); ;


                magick.Resize((uint)width, (uint)heigth);

                var buffer = magick.ToByteArray();
                c.Response.Clear();
                c.Response.ContentType = string.Concat("image/", fileInfo.Extension.Replace(".", ""));

                await c.Response.Body.WriteAsync(buffer, 0, buffer.Length);


            


            };

        }
    } 
}
