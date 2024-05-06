using System;
using System.Threading;

namespace Tema_4
{
    internal class Ejercicio6_lock
    {
        const int N_PERSONAS = 5;
        int personasMuseo = 0;
        Random random = new Random();
        public void Persona(object o)
        {
            int id = (int)o;
            Thread.CurrentThread.Name = "Hilo número " + id.ToString();

            object museo = new object();
            bool regalo = false;

            while (true)
            {
                lock (museo)
                {
                    regalo = (personasMuseo == 0);

                    WriteLine($"¡Hola a los {personasMuseo}!");
                    personasMuseo++;
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


                lock (museo)
                {
                    personasMuseo--;
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
            new Ejercicio6_lock().Exec();
        }
        
    }
}
