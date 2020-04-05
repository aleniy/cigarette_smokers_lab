using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cigarette_smokers_lab
{
    class Program
    {
        static void Main(string[] args)
        {
            CSwithMutex swithMutex = new CSwithMutex();
            swithMutex.Beginning();
            Console.ReadKey();
        }
    }
}
