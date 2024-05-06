using System;
using System.Threading;

namespace Tema_4
{
    internal class Ejercicio08
    {
        const int N_PROCESOS = 5;
        Random random = new Random();

        Barrier barreraA;
        Barrier barreraB;

        void Proceso(object o)
        {
            int id = (int)o;
            Thread.CurrentThread.Name = $"[PROCESO {o}]";
            while (true)
            {
                try 
                {
                    //Escribe la letra A
                    Console.Write("A");
                    //Espera a que todos los procesos hayan escrito la letra A y escribe el guión
                    barreraA.SignalAndWait();
                    //Escribe la letra B
                    Console.Write("B");
                    //Espera a que todos los procesos hayan escrito la letra B y escribe el guión
                    barreraB.SignalAndWait();
                }
                catch (Exception e)
                {
                    WriteLine("Excepción ocurrida: " + e.Message);
                }
            }
        }
        void Exec()
        {
            barreraA = new Barrier(N_PROCESOS, (a) => Console.Write(" -- "));
            barreraB = new Barrier(N_PROCESOS, (b) => Console.Write(" -- "));

            InitThreads(Proceso, N_PROCESOS);
        }

        void InitThreads(ParameterizedThreadStart thread, int numThreads)
        {
            for (int i = 0; i < numThreads; i++)
            {
                new Thread(() => thread(i)).Start();
            }
        }

        void WriteLine(String mes)
        {
            Thread.Sleep(random.Next(100));
            Console.WriteLine(Thread.CurrentThread.Name + ": " + mes);
            Thread.Sleep(random.Next(100));
        }
        public static void Main(String[] args)
        {
            new Ejercicio08().Exec();
        }
    }
}
