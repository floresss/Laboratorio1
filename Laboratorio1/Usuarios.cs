using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Laboratorio1
{
    class Usuarios
    {
        static string bloc = "usuarios.txt";
        static StreamWriter escribirarchivo;
        static StreamReader leerarchivo;
        static void guardarusuario(string usuario, string contraseña, int tipo)
        {
            escribirarchivo = File.AppendText(bloc);
            escribirarchivo.WriteLine(usuario + "," + contraseña + "," + tipo);
            escribirarchivo.Close();
            Console.WriteLine("se ha guardado el ususario");
        }

        public void menuusaurio()
        {
            int op = 0;
            while (op != 3)
            {
                Console.Clear();
                Console.WriteLine("1. usuario nuevo\n2. mostrar usuarios\n3. regresar al menu principal");
                op = int.Parse(Console.ReadLine());
                if (op == 1)
                {
                    Console.WriteLine("ingrese usuario ");
                    string usuario = Console.ReadLine();
                    Console.WriteLine("ingrese contraseña ");
                    string contraseña = Console.ReadLine();
                    Console.WriteLine("ingrese rol del usuario 1=administrador 2=trabajador");
                    int tipousuario = int.Parse(Console.ReadLine());

                    guardarusuario(usuario, contraseña, tipousuario);
                    Console.WriteLine("usuario agregado");
                }
                else if (op == 2)
                {
                    mostrarusuarios();
                    Console.ReadKey();
                }

            }
            Console.Clear();
        }

        static void mostrarusuarios()
        {
            Console.WriteLine("usaurio, contraseña, tipo usuario");
            string linea;
            leerarchivo = new StreamReader(bloc);
            linea = leerarchivo.ReadToEnd();
            Console.WriteLine(linea);
            leerarchivo.Close();
        }

        public int Login()
        {
            string linea = "";
            int tipo = 0;
            Console.Clear();
            Console.WriteLine("Ingrese usuario: ");
            string usuario = Console.ReadLine();
            Console.WriteLine("Ingrese contraseña: ");
            string contraseña = Console.ReadLine();
            using (leerarchivo = new StreamReader(bloc))
            {
                while ((linea = leerarchivo.ReadLine()) != null)
                {
                    string[] vector = linea.Split(',');
                    if (vector[0] == usuario && vector[1] == contraseña)
                    {
                        tipo = int.Parse(vector[2]);
                    }
                }
            }
            return tipo;
        }
    }
}
