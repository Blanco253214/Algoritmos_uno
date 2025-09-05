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
            //int numero = agregarCantidadTienda();
        }

        static void mostrarMenu()
        {
            Console.WriteLine("MENU PARA EL STOCK!");
            Console.WriteLine("Elija la opcion que desea ingresar");
            Console.WriteLine("1 para ingresar un item al stock");
            Console.WriteLine("2 para buscar un item en el stock");
            Console.WriteLine("3 para mostrar el stock completo");
        }

        static bool stringValido(string entrada)
        //funcion para validar el nombre ingresado.
        {
            bool valido = true;
            if (string.IsNullOrWhiteSpace(entrada))
            {
                valido = false;
            }
            foreach (char c in entrada)
            {
                if (!char.IsLetter(c))
                {
                    valido = false;
                    break;
                }
            }
            return valido;
        }

        static string agregarProducto()
        {
            string entrada;

            Console.WriteLine("Ingrese el nombre del producto: ");
            entrada = Console.ReadLine();
            if (stringValido(entrada))
            {
                Console.WriteLine("El nombure del producto de ha ingresado correctamente!");
            }
            else
            {
                Console.WriteLine("Error, intente con un nombre valido.");
                //hay que hacer una funcion recursiva para que el usuario reingrese
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

        static int agregarCantidadBodega()
        {
            int cantidad_bodega;
            Console.WriteLine("Ingrese la cantidad disponible en la bodega");
            if (int.TryParse(Console.ReadLine(), out cantidad_bodega))
            {
                Console.WriteLine($"La cantidad de {cantidad_bodega} se agrego correctamente!");
            }
            else
            {
                Console.WriteLine("Error! formato ingresado no valido. Intente nuevamente");
                Console.WriteLine("funcionalidad en desarrollo");
            }
            return cantidad_bodega;
        }

        static void mostrarStock(Item item)
        {
            Console.WriteLine($"Nombre: {item.Nombre}");
            Console.WriteLine($"Cantidad disponible en tienda: {item.Cantidad_disponible}");
            Console.WriteLine($"Cantidad disponible en bodega: {item.Cantidad_Bodega}");
        }
    }
}