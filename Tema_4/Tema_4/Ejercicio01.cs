using System;
using System.Threading;


namespace Tema_4
{
    class Ejercicio01
    {

        SemaphoreSlim semaforo = new SemaphoreSlim(0);
        static volatile int producto;

    public void Productor()
    {
        Random random = new Random();
        producto = random.Next(10);
        semaforo.Release();
    }

    public void Consumidor()
    {
        semaforo.Wait();
        Console.WriteLine("Producto: {0}", producto);
    }

     public void Exec()
    {
            new Thread(Productor).Start();
            new Thread(Consumidor).Start();
    }

        public static void Main(String[] args)
    {
            new Ejercicio01().Exec();
    }


    }
}
