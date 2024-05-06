using System;
using System.Diagnostics;
using System.Threading;


namespace Tema_4
{
    internal class Ejercicio10
    {
        Random random = new Random();
        CountdownEvent countdownAyF = new CountdownEvent(2);
        CountdownEvent countdownB = new CountdownEvent(1);
        CountdownEvent countdownD = new CountdownEvent(1);
        CountdownEvent countdownE = new CountdownEvent(1);

        void Proceso1()
        {
            Thread.CurrentThread.Name = "PROCESO 1";

            //ESCRIBE A
            WriteLine("A");
            countdownAyF.Signal();
            //ESPERA A QUE SE ESCRIBA D
            countdownD.Wait();
            //ESCRIBE B
            WriteLine("B");
            countdownB.Signal();
            //ESPERA A QUE SE ESCRIBA E
            countdownE.Wait();
            //ESCRIBE C
            WriteLine("C");
        }

        void Proceso2()
        {
            Thread.CurrentThread.Name = "PROCESO 2";

            //ESPERA QUE SE ESCRIBAN A Y F
            countdownAyF.Wait();
            countdownAyF.Wait();
            //ESCRIBE D
            WriteLine("D");
            countdownD.Signal();
            //ESPERA A QUE SE ESCRIBA B
            countdownB.Wait();
            //ESCRIBE E
            WriteLine("E");
            countdownE.Signal();
        }

        void Proceso3()
        {
            Thread.CurrentThread.Name = "PROCESO 3";

            //ESCRIBE F
            WriteLine("F");
            countdownAyF.Signal();
            //ESPERA A QUE SE ESCRIBA D
            countdownD.Wait();
            //ESCRIBE G
            WriteLine("G");
            //ESPERA A QUE SE ESCRIBA B
            countdownB.Wait();
            //ESCRIBE H
            WriteLine("H");         
        }

        void Exec()
        {
            InitThreads();
        }

        void InitThreads()
        {
            new Thread(Proceso1).Start();
            new Thread(Proceso2).Start();
            new Thread(Proceso3).Start();
        }

        void WriteLine(String mes)
        {
            Thread.Sleep(random.Next(1000));
            Console.WriteLine(Thread.CurrentThread.Name + ":" + mes);
            Thread.Sleep(random.Next(1000));
        }

        static void Main(String[] args)
        {
            new Ejercicio10().Exec();
        }
    }
}
