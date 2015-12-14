using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fiddler;

namespace FiddlerCoreClient
{
    class Program
    {
        static void Main(string[] args)
        {
            FiddlerApplication.Startup(9898, FiddlerCoreStartupFlags.Default);
        }
    }
}
