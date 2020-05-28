using System;
using System.Threading;

namespace testa
{

    using System;
    using System.Threading;

    class Program
    {
        static object padLock = new object();

        string shareme;
        static Program p = new Program();

        static void Main()
        {
            Program p = new Program();

            Thread t1 = new Thread(p.A);
            Thread t2 = new Thread(p.B);
            t1.Start();
            t1.Join();
            t2.Start();
            t2.Join();
            p.shareme = "Updated in main";
            Console.WriteLine(p.shareme);
            Console.ReadKey();
        }

        void A()
        {
            lock (padLock)
            {
                p.shareme = "Updated in A";
                Console.WriteLine(p.shareme);
            }
        }

        void B()
        {
            p.shareme = "Updated in B";
            Console.WriteLine(p.shareme);
        }
    }
}
