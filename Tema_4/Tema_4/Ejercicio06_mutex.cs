using System;
using System.Threading;

namespace Tema_4
{
    internal class Ejercicio06_mutex
    {
        const int N_PERSONAS = 5;
        int personasMuseo = 0;
        Random random = new Random();
        public void Persona(object o)
        {
            int id = (int)o;
            Thread.CurrentThread.Name = "Hilo número " + id.ToString();

            Mutex mutex = new Mutex();

            object museo = new object();
            bool regalo = false;


            while (true)
            {
                mutex.WaitOne();
                try { 
                
                    regalo = (personasMuseo == 0);
                    WriteLine($"¡Hola a los {personasMuseo}!");
                    personasMuseo++;
                
                }
                finally
                {
                    mutex.ReleaseMutex();
                }

                if (regalo == true)
                {
                    WriteLine("¡Ala, un REGALO!");
                    Thread.Sleep(1000);
                }
                else
                {
                    WriteLine("Yo no tengo regalo :(( ");
                }

                WriteLine("¡Qué bonito!");
                WriteLine("¡Alucinante!");
                WriteLine("¡Adiós!");


                mutex.WaitOne();
                try
                {
                    personasMuseo--;
                }
                finally
                {
                    mutex.ReleaseMutex();
                }

                WriteLine("Paseando");
            }
        }

        public void Exec()
        {
            InitThreads(Persona, N_PERSONAS);
  
        }

        public void WriteLine(string mes)
        {
            Thread.Sleep(random.Next(100));
            Console.WriteLine(Thread.CurrentThread.Name + ": " + mes);
            Thread.Sleep(random.Next(100));
        }

        public void InitThreads(ParameterizedThreadStart thread, int n_elements) {
        int id = 0;

            for (int i = 0; i < N_PERSONAS; i++)
            {
                id=i;
                new Thread(() => thread(id)).Start();
                Thread.Sleep(random.Next(1000));
            }

        }
        public static void Main(String[] args)
        {
            new Ejercicio06_mutex().Exec();
        }
        
    }
}
