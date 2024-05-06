using System;
using System.Threading;

namespace Tema_4
{
    internal class Ejercicio04
    {
        public static void Main(String[] args)
        {
            new ProdCons().Exec();
        }

        class ProdCons
        {
            volatile float producto;
            SincCond sinConsumido;
            SincCond sinProducido;
            

            const int NUM_PRODUCTOS = 10;

            public void Productor()
            {
                Random random = new Random();
                for (int i = 0; i < NUM_PRODUCTOS; i++)
                {
                    sinConsumido.Await();
                    producto = random.Next(1, 10);
                    sinProducido.Signal();
                }

            }


            public void Consumidor()
            {
                for (int i = 0; i < NUM_PRODUCTOS; i++)
                {
                sinProducido.Await();
                Console.WriteLine("Producto: {0}", producto);
                sinConsumido.Signal();
                }
            }


            public void Exec()
            {
                sinProducido = new SincCond(false);
                sinConsumido = new SincCond(true);
                

                new Thread(() => Productor()).Start();
                new Thread(() => Consumidor()).Start();
            }

        }


        class SincCond
        {
            volatile bool condicion;

            public SincCond(bool value)
            {
                condicion = value;  
            }
           
            public void Await()
            {
                while (!condicion) ;
                condicion = false;
            }

            public void Signal()
            {
                condicion = true;
            }
        }
    }
}
