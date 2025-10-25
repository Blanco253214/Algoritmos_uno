using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.CompilerServices;

namespace beta_stock
{
    class Stock
    {
        //Creacion del struct item.
        struct Item
        {
            public string Nombre { get; set; }
            public int Codigo { get; set; }
            public int CantidadTienda { get; set; }
            public int CantidadBodega { get; set; }
            public float Precio { get; set; }

            public override string ToString()
            {
                return $"Nombre: {Nombre}, Codigo: {Codigo}, Cantidad en tienda: {CantidadTienda}, Cantidad en bodega: {CantidadBodega}, Precio: {Precio}";
            }
        }
        //Validadores.
        static bool StringValido(string cadena)
        {
            return !string.IsNullOrEmpty(cadena) && cadena.All(char.IsLetter);
        }
        static string IngresarNombre()
        {
            //string respuesta = "si";
            Console.WriteLine("ingrese el nombre del producto: ");
            string Nombre = Console.ReadLine();
            while (!StringValido(Nombre))
            {
                Console.WriteLine("Error! ha ingresado un formato invalido.");
                Console.WriteLine("Solo se aceptan caracteres alfabeticos.");
                Console.WriteLine("Ingrese el nombre del producto: ");
                Nombre = Console.ReadLine();
            }
            return Nombre;
        }
        static int IngresarCodigo()
        {
            int varX;
            Console.WriteLine("Se admiten hasta 6 numeros.");
            Console.WriteLine("Ingrese el codigo del item: ");
            while (!int.TryParse(Console.ReadLine(), out varX) || varX.ToString().Length > 6)
            {
                Console.WriteLine("Error! formato invalido, intente nuevamente.");
                Console.WriteLine("Ingrese el codigo del item: ");
            }
            return varX;
        }
        static int IngresoCantidad()
        {
            int cantidad;
            Console.WriteLine("Ingrese la cantidad deseada: ");
            while (int.TryParse(Console.ReadLine(), out cantidad))
            {
                Console.WriteLine("Error, ingreso invalido. Intente nuevamente");
                Console.WriteLine("Ingrese la cantidad deseada: ");
            }
            return cantidad;
        }
        static float IngresoPrecio()
        {
            float precioItem;
            Console.WriteLine("Ingrese el precio del producto: ");
            while (float.TryParse(Console.ReadLine(), out precioItem))
            {
                Console.WriteLine("Error, ingreso invalido. Intente nuevamente");
                Console.WriteLine("Ingrese el precio del item: ");
            }
            return precioItem;
        }
        //Funciones.
        static void CrearStock(string ruta)
        {
            try
            {
                using (var wt = new StreamWriter(ruta, append: true, Encoding.UTF8 ))
                {
                    string reingreso = "si";
                    do
                    {
                        //Creacion del struct
                        string nombreProducto = IngresarNombre();
                        int codigo = IngresarCodigo();
                        int cantTienda = IngresoCantidad();
                        int cantBodega = IngresoCantidad();
                        float precioReal = IngresoPrecio();
                        Item nuevo = new Item
                        {
                            Nombre = nombreProducto,
                            Codigo = codigo,
                            CantidadTienda = cantBodega,
                            CantidadBodega = cantBodega,
                            Precio = precioReal

                        };
                        //se escribe en el archivo.
                        wt.WriteLine(nuevo);
                        Console.WriteLine("Desea seguir ingresando items? ");
                        reingreso = (Console.ReadLine() ?? "").Trim().ToLowerInvariant();
                    } while (reingreso == "si");
                    Console.WriteLine("Items ingresados correctamente.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al guardar los empleados: " + e.Message);
            }
        }
        static void Main(string[] args)
        {
            
        }
    }
}