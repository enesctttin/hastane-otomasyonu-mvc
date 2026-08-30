namespace CustomRouteHandler.Handlers
{
    public class ExampleHandler
    {

        public RequestDelegate Handler()
        {

            // fonksiyon dönecek bu method  RequestDelegate  bu tiptir

            return async c =>
            {
               await c.Response.WriteAsync("hello world");
              

            };

        }



    }
}
