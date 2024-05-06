using System;
using System.Threading;

namespace Tema_4
{
    internal class Ejercicio02
    {
        public void Mensaje()
        {
            //Imprime sus mensajes

            String[] mensajes = { "La vida es bella", "Oh no...", "Los pájaros cantan", "Y molestan..." };
            foreach (String mes in mensajes)
            {
                WriteLine(mes);
                
                try
                {
                    Thread.Sleep(2000);
                }
                catch (ThreadInterruptedException) //Si le interrumpen imprime por pantalla
                {
                    WriteLine("Se acabó");
                    return;
                }
            }
        }
        public void Exec()
        {
            Thread.CurrentThread.Name = "MAIN";

            //Crea hilo de mensajes
            Thread mensajes = new Thread(Mensaje);
            mensajes.Name ="MENSAJES";
            mensajes.Start();

            //Espera a que el hilo finalice, y si no lo hace, imprime por pantalla
            int contador = 1;
            
            while (true)
            {
                mensajes.Join(1000);
                if (!mensajes.IsAlive)
                {
                    return;
                }
                contador++;

                if (contador < 5)
                {
                    WriteLine("Todavía esperando...");
                }
                else //Cuando han pasado 5 segundos se cansa de esperar y lo interrumpe.
                {
                    WriteLine("Cansado de esperar!");
                    mensajes.Interrupt();
                    mensajes.Join();
                    //Cuando acaba imprime por pantalla
                    WriteLine("Por fin!");
                    return ;
                }
            }
        }

        public static void Main(String[] args)
        {
            new Ejercicio02().Exec();
        }

        public void WriteLine(String mes)
        {
            Console.WriteLine("[{0}]: {1}", Thread.CurrentThread.Name, mes);
        }
    }
}
