using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tema_4
{
    internal class Ejercicio14
    {
        const int N_TAREAS = 3;
        Task<string>[] _tasks = new Task<string>[N_TAREAS];
        Random _random = new Random();
        void Exec()
        {

            for (int i = 0; i < N_TAREAS; i++)
            {
                _tasks[i] = Task.Run(Metodo);
            }

            int counter = N_TAREAS;

            while (counter > 0)
            {
               int taskIndex = Task.WaitAny(_tasks);

                try
                {
                    Console.WriteLine($"Tarea {taskIndex} finalizada: {_tasks[taskIndex].Result}");

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Tarea {taskIndex} errónea: {ex.InnerException.Message}");
                    throw ex;
                }
                finally
                {
                    counter--;
                }
                
            }

        }

        public string Metodo()
        {
            //Espera un tiempo entre 0 y 500 ms
            Thread.Sleep(_random.Next(0,500));

            //Decidimos aleatoriamente si va a ser correcto o erróneo

            if(_random.Next(10) > 2){

                return("¡Tarea completada con éxito!\n");

            } else
            {
                throw new Exception("Tarea errónea");
            }
        }
    

        public static void Main(string[] args) {
        new Ejercicio14().Exec();
        }
    }
}
