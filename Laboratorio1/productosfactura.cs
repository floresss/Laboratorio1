using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Laboratorio1
{
    class productosfactura
    {
        static StreamWriter escribirarchivo;
        static StreamReader leerarchivo;
        static StreamReader leer2;

        static string bloc = "productos.txt";
        static string temporal = "temporal.txt";
        static string bloc2 = "facturas.txt";
        static string bloc3 = "descripcion.txt";

        static void mostrarproductos()
        {
            string linea;
            leerarchivo = new StreamReader(bloc);
            linea = leerarchivo.ReadToEnd();
            Console.WriteLine(linea);
            leerarchivo.Close();


        }
        public void agregarproducto(string id, string descripcion, int unidades, double precio)
        {
            escribirarchivo = File.AppendText(bloc);
            escribirarchivo.WriteLine(id + "*" + descripcion + "*" + unidades + "*" + precio);
            escribirarchivo.Close();
        }

        public void meuinventario()
        {
            string linea;
            int op = 0;
            while (op != 4)
            {
                Console.Clear();
                Console.WriteLine("1. producto nuevo\n2. rellenar existencia\n3. mostrar listado de productos\n4. regresar al menu principal");
                op = int.Parse(Console.ReadLine());

                if (op == 1)
                {
                    Console.WriteLine("\nnuevo producto");
                    Console.WriteLine("\ningrese id para el producto");
                    string id = Console.ReadLine();

                    Console.WriteLine("ingrese descripcion del producto");
                    string descripcion = Console.ReadLine();

                    Console.WriteLine("ingrese unidades");
                    int unidades = int.Parse(Console.ReadLine());

                    Console.WriteLine("ingrese precio del producto");
                    double precio = int.Parse(Console.ReadLine());

                    agregarproducto(id, descripcion, unidades, precio);

                    Console.WriteLine("se ha creado nuevo producto\n");

                }
                if (op == 2)
                {
                    string descrip = "";
                    int uni = 0;
                    double prec = 0;
                    string id2 = "";


                    Console.WriteLine("\nid - nombre - unidades - precio");

                    mostrarproductos();

                    Console.Write("\ningrese el id producto a rellenar: ");
                    string id = Console.ReadLine();

                    using (escribirarchivo = new StreamWriter(temporal))
                    {
                        using (leerarchivo = new StreamReader(bloc))
                        {
                            while ((linea = leerarchivo.ReadLine()) != null)
                            {
                                string[] producto = linea.Split('*');

                                if (producto[0] == id)
                                {
                                    id2 = producto[0];

                                    descrip = producto[1];
                                    uni = Convert.ToInt32(producto[2]);
                                    prec = Convert.ToDouble(producto[3]);
                                }
                                else
                                {
                                    escribirarchivo.WriteLine(linea);
                                }
                            }
                        }
                    }


                    Console.WriteLine("\ningrese cantidad a rellenar");

                    int unidadesnuevas = int.Parse(Console.ReadLine());
                    int totalunidades = unidadesnuevas + uni;

                    agregarproducto(id2, descrip, totalunidades, prec);

                    Console.WriteLine("\nunidades añadidas");

                    File.Delete(bloc);

                    File.Move(temporal, bloc);
                }
                if (op == 3)
                {
                    Console.WriteLine("\nid - nombre - unidades - precio");

                    mostrarproductos();

                    Console.ReadKey();
                }
            }
        }

        static void agregarfactura(string correlativo, string cliente, string nit, string fecha, double total)
        {
            escribirarchivo = File.AppendText(bloc2);
            escribirarchivo.WriteLine(correlativo + "*" + cliente + "*" + nit + "*" + fecha + "*" + total.ToString("N2"));
            escribirarchivo.Close();

        }

        public void menufactura()
        {
            string linea;
            int op = 0;

            while (op != 3)
            {
                Console.Clear();
                Console.WriteLine("1. generar nueva factura\n2. mostrar listado de facturas\n3. regresar al menu principal");
                op = int.Parse(Console.ReadLine());

                if (op == 1)
                {
                    Console.WriteLine("ingrese correlativo de factura ");
                    string correlativo = Console.ReadLine();

                    Console.WriteLine("ingrese noombre a facturar ");
                    string clinete = Console.ReadLine();

                    Console.WriteLine("ingrese nit del cliente");
                    string nit = Console.ReadLine();

                    Console.WriteLine("ingrese fecha de facturacion ");
                    string fecha = Console.ReadLine();



                    string r = "s";
                    double total = 0;
                    while (r == "s" || r == "S")
                    {
                        Console.Clear();
                        Console.WriteLine("\nid - nombre - unidades - precio");
                        mostrarproductos();
                        Console.WriteLine("\ningrese id de producto a facturar");
                        string id = Console.ReadLine();

                        Console.WriteLine("ingrese unidades a retirar:");
                        int unidades = int.Parse(Console.ReadLine());

                        int contador = 0;
                        string id2 = "";
                        string producto = "";
                        int uni = 0;
                        double precio = 0;
                        using (escribirarchivo = new StreamWriter(temporal))
                        {
                            using (leerarchivo = new StreamReader(bloc))
                            {
                                while ((linea = leerarchivo.ReadLine()) != null)
                                {
                                    string[] descripcion = linea.Split('*');
                                    if (descripcion[0] == id)
                                    {
                                        contador = contador + 1;
                                        id2 = descripcion[0];
                                        producto = descripcion[1];
                                        uni = Convert.ToInt32(descripcion[2]);
                                        precio = Convert.ToDouble(descripcion[3]);
                                    }
                                    else
                                    {
                                        escribirarchivo.WriteLine(linea);
                                    }
                                }
                            }
                        }
                        if (contador == 1)
                        {
                            File.Delete(bloc);
                            File.Move(temporal, bloc);
                            int restaunidades = uni - unidades;
                            agregarproducto(id2, producto, restaunidades, precio);
                            double subtotal = precio * unidades;
                            total = total + subtotal;
                            agregardescipcion(correlativo, producto, unidades, precio, subtotal);
                        }
                        Console.WriteLine("agregar otro producto? s/n");
                        r = Console.ReadLine();
                        if (r != "s" || r != "S")
                        {
                            agregarfactura(correlativo, clinete, nit, fecha, total);
                        }
                    }
                    Console.WriteLine("se ha agregado la factura");
                }
                if (op == 2)
                {
                    mostrarfacturas();
                }
            }

        }
        static void agregardescipcion(string correlativo, string producto, int cantidad, double precio, double total)
        {
            escribirarchivo = File.AppendText(bloc3);
            escribirarchivo.WriteLine(correlativo + "*" + producto + "*" + cantidad + "*" + precio + "*" + total);
            escribirarchivo.Close();
        }
        static void mostrarfacturas()
        {
            string linea1;
            string linea2;
            using (leerarchivo = new StreamReader(bloc2))
            {
                while ((linea1 = leerarchivo.ReadLine()) != null)
                {
                    string[] factura = linea1.Split('*');

                    Console.WriteLine("correlativo: " + factura[0]);
                    Console.WriteLine("facturado a: " + factura[1]);
                    Console.WriteLine("nit: " + factura[2]);
                    Console.WriteLine("fecha: " + factura[3]);

                    using (leer2 = new StreamReader(bloc3))
                    {
                        Console.WriteLine("producto               cantidad               precio               subtotal");
                        while ((linea2 = leer2.ReadLine()) != null)
                        {
                            string[] descripcion = linea2.Split('*');
                            if (factura[0] == descripcion[0])
                            {
                                Console.WriteLine(descripcion[1] + "               " + descripcion[2] + "               " + descripcion[3] + "               " + descripcion[4]);
                            }
                        }
                    }
                    Console.WriteLine("                                                        Total facturado: " + factura[4] + "\n");
                }
            }
            Console.ReadKey();
        }
    }
}
