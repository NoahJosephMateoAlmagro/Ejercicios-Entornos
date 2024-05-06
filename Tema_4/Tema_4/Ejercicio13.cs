using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Serialization;

namespace Tema_4
{
    internal class Ejercicio13
    {
        //Constantes para los robots
        const int NUM_ROBOTS = 5;
        const int NUM_TIPOS_PIEZAS = 10;

        //Constantes para las máquinas
        const int MAX_PIEZAS = 15;


        void Exec()
        {
            new Fabrica(NUM_ROBOTS, NUM_TIPOS_PIEZAS, MAX_PIEZAS);
        }

        static void Main(String[] args)
        {
            new Ejercicio13().Exec();
        }
    }

    class Maquina
    {
        Almacen _almacen;
        int _tipoPieza;


        public Maquina(Almacen almacen, int tipoPieza)
        {
            _almacen = almacen;
            _tipoPieza = tipoPieza;
        }

        public void Exec()
        {
            while (true)
            {
                var pieza = FabricarPieza();
                AlmacenarPieza(_tipoPieza,pieza);
            }
        }
        public double FabricarPieza()
        {
            Thread.Sleep(Fabrica.random.Next(200,500));
            return (_tipoPieza + Fabrica.random.NextDouble()); //NextDouble devuelve un valor entre 0 y 1
        }

        public void AlmacenarPieza(int tipoPieza,double pieza)
        {
            Thread.Sleep(Fabrica.random.Next(350, 500));
            _almacen.Guardar(tipoPieza,pieza);
        }

    }

    class Robot
    {
        private readonly int _nTiposPiezas;
        Almacen _almacen;

        public Robot(int nTiposPiezas, Almacen almacen)
        {
            _nTiposPiezas = nTiposPiezas;
            _almacen = almacen; 
        }

        public void Exec()
        {

            while (true)
            {
                Mecha mecha = new Mecha(_nTiposPiezas);

                for (int tipoPieza = 0; tipoPieza < _nTiposPiezas; tipoPieza++)
                {
                    var pieza = RecogerPieza(tipoPieza);
                    MontarPieza(mecha, tipoPieza, pieza);
                }

                mecha.Ver();
            }
        }
        public double RecogerPieza(int i)
        {
            Thread.Sleep(Fabrica.random.Next(500, 1000));
            return _almacen.Sacar(i);
        }
        public void MontarPieza(Mecha  mecha, int tipoPieza, double pieza)
        {
            Thread.Sleep(Fabrica.random.Next(1000, 2000));
            mecha.Montar(tipoPieza, pieza);
        }
    }

    class Fabrica
    {
        public static readonly Random random = new Random();

        private readonly int _nRobots;
        private readonly int _nTipoPiezas;
        private readonly int _maxPiezas;

        private readonly Almacen _almacen;

        public Fabrica(int nRobots, int nTipoPiezas, int maxPiezas)
        {
            _nRobots = nRobots;
            _nTipoPiezas = nTipoPiezas;
            _maxPiezas = maxPiezas;

            _almacen = new Almacen(_maxPiezas);

            CreateMaquinas();
            CreateRobots();
        }

        private void CreateMaquinas()
        {
            for (int i = 0; i < _nTipoPiezas; i++)
            {
                new Thread(new Maquina(_almacen, i).Exec).Start();
            }
        }

        private void CreateRobots()
        {
            for (int i = 0; i < _nRobots; i++)
            {
                new Thread(new Robot(_nTipoPiezas,_almacen).Exec).Start();
            }
        }
    }

    class Almacen
    {
        private readonly int _maxPiezas;
        private IDictionary<int, BlockingCollection<double>> almacenaje;

        public Almacen(int maxPiezas)
        {
            _maxPiezas = maxPiezas;

            almacenaje = new Dictionary<int, BlockingCollection<double>>();

            for (int tipoPieza = 0; tipoPieza < _maxPiezas; tipoPieza++)
            {
               almacenaje.Add(tipoPieza,new BlockingCollection<double>(_maxPiezas));
            }

        }

        public void Guardar(int tipoPieza, double pieza)
        {
            almacenaje[tipoPieza].Add(pieza);
        }

        public double Sacar(int tipoPieza)
        {
            return almacenaje[tipoPieza].Take();
        }
    }

    class Mecha
    {
        private readonly int _numTiposPiezas;
        private double[] piezasMontadas;

        public Mecha(int numTiposPiezas)
        {
            _numTiposPiezas = numTiposPiezas;
            piezasMontadas = new double[numTiposPiezas];
        }
        public void Montar(int tipoPieza, double pieza)
        {
            piezasMontadas[tipoPieza] = pieza;
        }
        public void Ver()
        {
            Console.Write("Mecha preparado: [");

            for (int i = 0; i < _numTiposPiezas; i++)
            {
                Console.Write($"{piezasMontadas[i]}, ");
            }

            Console.WriteLine("]");
        }
    }
}

