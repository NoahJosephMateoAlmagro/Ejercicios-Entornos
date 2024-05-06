using System;
using System.Threading;

namespace Tema_4
{
    internal class Ejercicio05
    {
        const int N_PRODUCTORES = 3;
        const int N_CONSUMIDORES = 4;
        const int N_PRODUCTOS = 20;

        class ProdConsBuffer
        {

            static Random rnd = new Random();
            Buffer buffer;

            public void Productor(object o)
            {
                int id = (int)o;
                Thread.CurrentThread.Name = "PRODUCTOR " + id;
                WriteLine("Iniciando productor ", id);

                try
                {
                    for(int i=0; i < N_PRODUCTOS; i++)
                    {
                        Thread.Sleep(rnd.Next(500));
                        int producto = rnd.Next(10) + (id * 10);
                        WriteLine("Producido: " + producto.ToString("00"), id);
                        buffer.Insert(producto);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Se ha encontrado la excepción: {0}", ex.Message);
                }
            }

            public void Consumidor(object o)
            {
                int id = (int)o;
                Thread.CurrentThread.Name = "CONSUMIDOR " + id;
                WriteLine("Iniciando consumidor ", id);

                try
                {
                    while (true)
                    {
                        int dato = buffer.Extract();
                        Thread.Sleep(rnd.Next(500));
                        WriteLine("Consumido: " + dato, id);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Se ha encontrado la excepción: {0}", ex.Message);
                }

            }

            public void Exec()
            {
                buffer = new Buffer();
                InitThreads(Productor, N_PRODUCTORES);
                InitThreads(Consumidor, N_CONSUMIDORES);
            }
        }


       static void InitThreads(ParameterizedThreadStart th, int numThreads)
        {
            for (int i = 0; i < numThreads; i++)
            {
                int id = i;
                new Thread(() => th(id)).Start();
            }
        }

        static void WriteLine(string s, int i)
        {
            Console.WriteLine("[{0}]: {1}", Thread.CurrentThread.Name, s);
        }

        class Buffer
        {
            const int BUFFER_SIZE = 10;

            private int[] bufferData = new int[BUFFER_SIZE];
            private int posOut = 0;
            private int posIn = 0;

            SemaphoreSlim emPosOut = new SemaphoreSlim(1);
            SemaphoreSlim emPosIn  = new SemaphoreSlim(1);  

            SemaphoreSlim nHuecosLibres = new SemaphoreSlim(BUFFER_SIZE);
            SemaphoreSlim nProductosSinConsumir = new SemaphoreSlim(0);

            
            public void Insert(int data)
            {
                //Esperamos a que haya huecos libres en el buffer
                nHuecosLibres.Wait();
                //Realizamos exclusión mutua sobre la variable de la posición de inserción.
                emPosIn.Wait();
                //Insertamos
                bufferData[posIn] = data;
                //Actualizamos la posición de inserción
                posIn = (posIn++) % BUFFER_SIZE;
                //Liberamos la exclusión mutua
                emPosIn.Release();
                //Sumamos un producto
                nProductosSinConsumir.Release();
            }

            public int Extract()
            {
                //Restamos un producto
                nProductosSinConsumir.Wait();
                //Realizamos exclusión mutua sobre la variable de la posición de extracción.
                emPosOut.Wait();
                //Extraemos
                int data = bufferData[posOut];
                //Actualizamos la posición de extracción
                posOut = (posOut++) % BUFFER_SIZE;
                //Liberamos la exclusión mutua
                emPosOut.Release();
                //Aumentamos un hueco libre
                nHuecosLibres.Release();

                return data;
            }

            
        }
        public static void Main(String[] args)
        {
            new ProdConsBuffer().Exec();
        }

    }
}
