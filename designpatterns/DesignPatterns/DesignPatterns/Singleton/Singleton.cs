using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Singleton
{
    public class Singleton
    {
        private readonly static Singleton _instance = new Singleton();
        public static Singleton Instance => _instance;
        private Singleton() { }
    }
}
