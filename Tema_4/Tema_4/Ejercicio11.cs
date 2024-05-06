using System;
using System.Collections.Generic;
using System.Threading;

namespace Tema_4
{
    internal class Ejercicio11
    {
        void Exec()
        {
            var aereopuertos = new Dictionary<string, Aereopuerto>();

            var madrid = new Aereopuerto("MAD", "Barajas", "Madrid", 4);
            var barcelona = new Aereopuerto("BCN", "El Prat", "Barcelona", 3);
            var mallorca = new Aereopuerto("PMI", "Son Sant Joan", "Palma de Mallorca", 2);

            AddAereopuerto(aereopuertos, madrid);
            AddAereopuerto(aereopuertos, barcelona);
            AddAereopuerto(aereopuertos, mallorca);

            Console.WriteLine(aereopuertos["MAD"]);
        }

        static void AddAereopuerto(Dictionary<string, Aereopuerto> aer, Aereopuerto a)
        {
            aer.Add(a.IdIATA, a);
        }

        static void Main(String[] args)
        {
            new Ejercicio11().Exec();   
        }

    }

    class Aereopuerto
    {
        public String IdIATA { get; private set; }
        public String Nombre { get; private set; }
        public String Localidad { get; private set; }
        public int N_pistas { get; private set; }

        public Aereopuerto(String idIATA, String nombre, String localidad, int n_pistas)
        {
            IdIATA = idIATA;
            Nombre = nombre;
            Localidad = localidad;  
            N_pistas = n_pistas;
        }

        public override string ToString()
        {
            return String.Format("{0} [Nombre]: {1} [Localidad]: {2} [Número de pistas]: {3}", IdIATA, Nombre, Localidad, N_pistas);
        }
    }
}
