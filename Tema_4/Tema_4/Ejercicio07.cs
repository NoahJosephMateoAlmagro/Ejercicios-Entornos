using System;
using System.Globalization;
using System.Threading;

namespace Tema_4
{
    internal class Ejercicio07
    {
        const int N_LECTORES = 5;
        const int N_ESCRITORES = 3;

        Random random = new Random();
        ReaderWriterLockSlim myLock = new ReaderWriterLockSlim();


        void Lector(object o)
        {
            int id = (int)o;

            Thread.CurrentThread.Name = "\t\tLECTOR " + id;

            while (true)
            {
                myLock.EnterReadLock();

                try
                {
                    WriteLine("Leyendo...\n");
                }
                catch (ThreadInterruptedException e)
                {
                    WriteLine("Excepción encontrada: " + e.Message);
                }
                finally
                {
                    myLock.ExitReadLock();
                }

            }
        }
        void Escritor(object o)
        {
            int id = (int) o;

            Thread.CurrentThread.Name = "ESCRITOR " + id;

            while (true)
            {
                myLock.EnterWriteLock();

                try
                {
                    WriteLine("Escribiendo... \n");
                }
                catch (ThreadInterruptedException e)
                {
                    WriteLine("Excepción encontrada: " + e.Message);
                }
                finally
                {
                    myLock.ExitWriteLock();
                }
            }
        }
        void Exec()
        {
            InitThreads(Escritor, N_ESCRITORES);
            InitThreads(Lector, N_LECTORES);
        }

        void WriteLine(String mes)
        {
            Thread.Sleep(random.Next(1000));
            Console.WriteLine(Thread.CurrentThread.Name + ": " + mes);
            Thread.Sleep(random.Next(1000));
        }

        void InitThreads(ParameterizedThreadStart thread, int num)
        {
            int id;
            for (int i = 0; i < num; i++)
            {
                id = i;
                new Thread(() => thread(id)).Start();
            }
        }
        private static void Main()
        {
            new Ejercicio07().Exec();
        }

        
    }
}
