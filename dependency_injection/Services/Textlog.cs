using dependency_injection.Services.Interfaces;
using dependency_injection.Services.Interfaces;
namespace dependency_injection.Services
{
    public class Textlog:ILog
    {

        public void Log()
        {
            Console.WriteLine("Text loglandı");

        }
    }
}
