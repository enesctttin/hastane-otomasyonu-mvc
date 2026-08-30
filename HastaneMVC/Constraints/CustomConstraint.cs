namespace HastaneMVC.Constraints
{
    public class CustomConstraint : IRouteConstraint
    {
        // bunu çalıştırmak için program cs de ayağa kaldıralım   
        // bu kısım gelen requesti ilk olarak tutan yerdir controllerden önce buraya uğrar    midddleware filtre gibi araya girer
        public bool Match(HttpContext? httpContext, 
            IRouter? route, 
            string routeKey, 
            RouteValueDictionary values, 
            RouteDirection routeDirection)
        {
            //   throw new NotImplementedException();  hata fırlatmasın 

            var idvalue = values[routeKey];

            return true;

        }
    }
}
