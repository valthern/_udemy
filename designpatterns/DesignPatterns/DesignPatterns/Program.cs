using System;

namespace DesignPatterns
{
    class Program
    {
        static void Main(string[] args)
        {
            var log = Singleton.Log.Instance;
            log.Save("Mi primer Singletón Teletubi");
            log.Save("Mensajito de prueba");
            log.Save("Bomberito juárez");
            log.Save("y Manguerita Maza de Juárez");
            log.Save("Pequeño chas-carrillo");

            var a = Singleton.Log.Instance;
            var b = Singleton.Log.Instance;

            if (a == b)
            {
                Console.WriteLine("Son la misma instancia");
            }
            else
            {
                Console.WriteLine("No son la misma instancia");
            }
        }
    }
}