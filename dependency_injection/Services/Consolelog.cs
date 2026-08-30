using dependency_injection.Services.Interfaces;
using dependency_injection.Services;
namespace dependency_injection.Services
{
    public class Consolelog : ILog
    {

        public Consolelog(int a) { }  // constructer services çalışırken parametre alıyorsa bu işlem burada yapılır program.cs de değil


        public void Log()
        {
            Console.WriteLine("Console loglandı");
        }
    }
}
