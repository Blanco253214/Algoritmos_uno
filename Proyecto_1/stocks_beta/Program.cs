using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace beta_stock
{

    class Item
    {
        public string Nombre { get; set; }
        //nombre del producto.
        public int Cantidad_disponible { get; set; }
        //cantidad de disponibilidad inmediata (en tienda).
        public int Cantidad_Bodega { get; set; }
        //cantidad disponible en la bodega central.
        public Item(string nombre, int cant_disponible, int cant_bodega)
        {
            Nombre = nombre;
            Cantidad_disponible = cant_disponible;
            Cantidad_Bodega = cant_bodega;
        }

        //get y set significa que se puede leer (get) y escribit: (set) 
    }

    class Stock
    {
        static void Main(string[] args)
        {
            //List<string> stock = new List<string>();
            string nombre = agregarProducto();
            int numero = agregarCantidadTienda();
        }

        static void mostrarMenu()
        {
            Console.WriteLine("MENU PARA EL STOCK!");
            Console.WriteLine("Elija la opcion que desea ingresar");
            Console.WriteLine("1 para ingresar un item al stock");
            Console.WriteLine("2 para buscar un item en el stock");
            Console.WriteLine("3 para mostrar el stock completo");
        }

        static string agregarProducto()
        {
            int cantidad;
            string entrada;

            Console.WriteLine("Ingrese el nombre del producto");
            entrada = Console.ReadLine();
            if (int.TryParse(entrada, out cantidad))
            {
                Console.WriteLine("Error, dato ingresado no valido.");
                Console.WriteLine("Funcionalidad en desarrollo.");
            }
            else
            {
                Console.WriteLine("El item se ingreso correctamente!");
            }
            return entrada;
        }

        static int agregarCantidadTienda()
        {
            int cantidad_tienda;
            Console.WriteLine("Ingrese la cantidad disponible en la tienda");
            if (int.TryParse(Console.ReadLine(), out cantidad_tienda))
            {
                Console.WriteLine($"La cantidad de {cantidad_tienda} se agrego correctamente!");
            }
            else
            {
                Console.WriteLine("Error! formato ingresado no valido. Intente nuevamente");
                Console.WriteLine("funcionalidad en desarrollo");
            }
            return cantidad_tienda;
        }
    }
}