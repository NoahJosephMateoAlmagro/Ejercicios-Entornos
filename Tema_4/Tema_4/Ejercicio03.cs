using System;
using System.Threading;

namespace Tema_4
{
    internal class Ejercicio03
    {
        public static void Main(String[] args)
        {
            new ProdCons().Exec();
        }

        class ProdCons
        {
            volatile float producto;
            SincCond sin;

            public void Productor()
            {
                Random random = new Random();
                producto = random.Next(1, 10);
                sin.Signal();
            }


            public void Consumidor()
            {
                sin.Await();
                Console.WriteLine("Producto: {0}", producto);
            }


            public void Exec()
            {
                sin = new SincCond();

                new Thread(() => Productor()).Start();
                new Thread(() => Consumidor()).Start();
            }

        }


        class SincCond
        {
            volatile bool condicion = false;

            public void Await()
            {
                while (!condicion) ;
            }

            public void Signal()
            {
                condicion = true;
            }
        }
    }
}
