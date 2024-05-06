using System;
using System.Threading;

namespace Tema_4
{
    internal class Ejercicio09
    {
        static Random _random = new Random();

        private const int N_FRAGMENTOS = 10;
        private const int N_HILOS = 3;
        private const int N_FICHEROS = 10;

        private static volatile int[] fichero = new int[N_FRAGMENTOS];

        Barrier barrier;
        readonly object nextFragmentLock = new object();
        int nextFragment = 0;

        void Downloader()
        {
            for (int i = 0; i < N_FICHEROS; i++)
            {
                DownloadFragments();

                WriteLine("Esperando al siguiente fichero...");
                barrier.SignalAndWait();
            }
        }

        void DownloadFragments()
        {
            int myNextFragment = 0;

            while (true)
            {
                
                lock (nextFragmentLock)
                {

                    if (nextFragment < N_FRAGMENTOS)
                    {
                        myNextFragment = nextFragment;
                        nextFragment++;
                    }
                    else
                    {
                        break;
                    }

                }

                WriteLine($"Se va a descargar el fragmento {myNextFragment}");
                int data = DescargaDatos(myNextFragment);
                WriteLine($"Ha finalizado la descarga del fragmento {myNextFragment}");
                fichero[myNextFragment] = data;

            }
        }

        void Exec()
        {
            Thread.CurrentThread.Name = "MAIN";

            Thread[] threads = new Thread[N_HILOS]; 
            barrier = new Barrier(N_HILOS, (a) => EndFile());

            InitThreads(threads);
            JoinThreads(threads);
            MostrarFichero();
        }
        private int DescargaDatos(int numFragmento)
        {
            _random.Next(1000);
            return numFragmento * 2;
        }

        public static void MostrarFichero()
        {
            Console.Write($"\n{Thread.CurrentThread.Name}:--------------------------------------------------\n");
            Console.Write($"\n{Thread.CurrentThread.Name}: File = [");
            for (int i = 0; i < N_FRAGMENTOS; i++)
            {
                Console.Write(fichero[i] + ",");
            }
            Console.Write("]\n\n");
            WriteLine("--------------------------------------------------\n");
        }

        static void WriteLine(String mes)
        {
            Thread.Sleep(_random.Next(100));
            Console.WriteLine(Thread.CurrentThread.Name + ": " + mes);
            Thread.Sleep(_random.Next(100));
        }

        void InitThreads(Thread[] threads)
        {
            for (int i = 0; i < N_HILOS; i++)
            {
                threads[i] = new Thread(Downloader);
                threads[i].Name = "HILO " + i;
                threads[i].Start();
            }
        }

        void JoinThreads(Thread[] threads)
        {
            for (int i = 0; i < N_HILOS; i++)
            {
                threads[i].Join();
            }
        }

        void EndFile()
        {
            MostrarFichero();

            nextFragment = 0;
            fichero = new int[N_FRAGMENTOS];
        }

        private static void Main(String[] args)
        {
            new Ejercicio09().Exec();
        }




    }
}
