using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Laboratorio1
{
    class Program
    {
        static Usuarios usuarios = new Usuarios();
        static productosfactura inventario = new productosfactura();
        static productosfactura factura = new productosfactura();
        static void Main(string[] args)
        {
            int tipousuario = 0;
            int op = 0;
            while (tipousuario < 1 || tipousuario > 2)
            {
                tipousuario = usuarios.Login();
            }

            while (op != 3)
            {
                if (tipousuario == 1) // 1 = admin
                {
                    Console.WriteLine("1. usuarios\n2. inventarios \n3. facturas");
                    op = int.Parse(Console.ReadLine());

                    if (op == 1)
                    {
                        usuarios.menuusaurio();
                    }
                    if (op == 2)
                    {
                        inventario.meuinventario();
                    }
                    if (op == 3)
                    {
                        factura.menufactura();
                    }
                }
                if (tipousuario == 2) // 2 = trabajador
                {
                    Console.WriteLine("2. inventarios\n3. facturas");
                    op = int.Parse(Console.ReadLine());
                    if (op == 1)
                    {
                        inventario.meuinventario();
                    }
                    else if (op == 2)
                    {
                        factura.menufactura();
                    }
                }
            }
            Console.ReadKey();
        }
    }
}
