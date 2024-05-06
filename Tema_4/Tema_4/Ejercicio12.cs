using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema_4
{
    internal class Ejercicio12
    {
        void Exec()
        {
            GestorViajes gestor = new GestorViajes();

            gestor.AddViaje("Madrid", "Barcelona", 1);
            gestor.AddViaje("Madrid", "Doha   ", 7);
            gestor.AddViaje("Madrid", "Tokio   ", 16);
            gestor.AddViaje("Madrid", "Los Angeles", 15);
            gestor.AddViaje("Barcelona", "Doha   ", 6);
            gestor.AddViaje("Barcelona", "Tokio   ", 15);
            gestor.AddViaje("Barcelona", "Los Angeles", 13);
            gestor.AddViaje("Doha   ", "Tokio   ", 11);
            gestor.AddViaje("Doha   ", "Los Angeles", 16);
            gestor.AddViaje("Tokio   ", "Los Angeles", 10);

            Console.WriteLine("Ciudades en las que hay viajes:\n");
            // imprime todas las ciudades que tienen viajes

            Print(gestor.GetCiudades());

            Console.WriteLine("\nViajes desde Madrid:\n");
            // imprime todos los viajes con origen Madrid

            Print(gestor.GetViajesPorOrigen("Madrid"));

            Console.WriteLine("\nViajes hacia Barcelona:\n");
            // imprime todos los viajes con destino Barcelona

            Print(gestor.GetViajesPorDestino("Barcelona"));

            Console.WriteLine("\nViajes:\n");
            // imprime todos los viajes

            Print(gestor.GetViajes());
        }

        void Print(IEnumerable<object> coleccion) //Como lo vamos a usar con HashSet y Listas utilizamos IEnumerable, ya que es la interfaz que comparten.\
        {
            foreach(var c in coleccion)
            {
                Console.WriteLine(c.ToString());
            }
        }
        static void Main()
        {
           new Ejercicio12().Exec();
        }
    }

    class GestorViajes
    {
        private readonly ISet<string> ciudades = new HashSet<string>();  //Podría ser hashset pero con ISet nos aseguramos de poder poner en otro momento cualquier tipo de lista
        private readonly IDictionary<string, List<Viaje>> viajesPorOrigen = new Dictionary<string, List<Viaje>>();
        private readonly IDictionary<string, List<Viaje>> viajesPorDestino = new Dictionary<string, List<Viaje>>();
        private readonly IList<Viaje> viajes = new List<Viaje>();

        public void AddViaje(string ciudad1, string ciudad2, int duracion)
        {
            var viajeOrigen = new Viaje(ciudad1, ciudad2, duracion);
            var viajeDestino = new Viaje(ciudad2, ciudad1, duracion);

            InsertViaje(viajesPorOrigen, viajeOrigen);
            InsertViaje(viajesPorOrigen, viajeDestino);
            InsertViaje(viajesPorDestino, viajeDestino);
            InsertViaje(viajesPorDestino, viajeOrigen);

            //Se permiten repetidos
            viajes.Add(viajeOrigen);
            viajes.Add(viajeDestino);

            //Solo añade si no están repetidos
            ciudades.Add(ciudad1);
            ciudades.Add(ciudad2);  

        }

        private void InsertViaje(IDictionary<string, List<Viaje>> coleccion, Viaje viaje)
        {
            //Comprobamos si en la colección existe ya una lista de viajes que salen de esa ciudad
            if(!coleccion.TryGetValue(viaje.Ciudad1, out List<Viaje> listaDeViajes)) // Esta variable es una referencia y la puedes usar fuera del método
            {
                listaDeViajes = new List<Viaje>(); //Si no existe la lista la creamos y la añadimos a la colección.
                coleccion[viaje.Ciudad1] = listaDeViajes;
            }

            listaDeViajes.Add(viaje);
        }

        public IReadOnlyCollection<string> GetCiudades()   //Podriamos poner HashSet<string> pero con la interfaz te aseguras de que sea inmutable (no se puede editar)
        { 
            return ciudades.ToList<string>(); //Como hemos usado ISet lo tenemos que pasar a lista
        }

        public IReadOnlyCollection<Viaje> GetViajesPorOrigen(string ciudad) //Podriamos poner List<Viaje> pero con la interfaz te aseguras de que sea inmutable (no se puede editar)
        {
            return viajesPorOrigen[ciudad];
        }

        public IReadOnlyCollection<Viaje> GetViajesPorDestino(string ciudad) //Podriamos poner List<Viaje> pero con la interfaz te aseguras de que sea inmutable (no se puede editar)
        {
            return viajesPorDestino[ciudad];
        }

        public IReadOnlyCollection<Viaje> GetViajes() //Podriamos poner List<Viaje> pero con la interfaz te aseguras de que sea inmutable (no se puede editar)
        {
            return viajes.ToList<Viaje>(); //Como hemos usado IList lo tenemos que pasar a lista 
        }


    }

    class Viaje
    {
        public string Ciudad1 { get; private set; }
        public string Ciudad2 { get; private set; }
        public int Duracion { get; private set; }

         public Viaje(string origen, string destino, int duracion)
        {
            Ciudad1 = origen;
            Ciudad2 = destino;
            Duracion = duracion;

        }

        public override string ToString()
        {
            return string.Format("[ORIGEN]: {0}\t[DESTINO]: {1}\t[DURACIÓN]: {2}", Ciudad1, Ciudad2, Duracion);
        }


    }
}
