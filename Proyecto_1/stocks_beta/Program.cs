using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace beta_stock
{
    class Stock
    {
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

        // ======================== VALIDADORES ========================
        static bool StringValido(string cadena)
        {
            return !string.IsNullOrWhiteSpace(cadena) &&
                   cadena.All(c => char.IsLetter(c) || char.IsWhiteSpace(c));
        }

        static string IngresarNombre()
        {
            Console.WriteLine("Ingrese el nombre del producto:");
            string nombre = Console.ReadLine();
            while (!StringValido(nombre))
            {
                Console.WriteLine("Error! Solo se aceptan letras. Intente nuevamente:");
                nombre = Console.ReadLine();
            }
            return nombre.Trim();
        }

        static int IngresarCodigo()
        {
            int varX;
            Console.WriteLine("Ingrese el código (hasta 6 dígitos):");
            while (!int.TryParse(Console.ReadLine(), out varX) || varX.ToString().Length > 6)
            {
                Console.WriteLine("Error! Formato inválido, intente nuevamente:");
            }
            return varX;
        }

        static int IngresoCantidad(string ubicacion)
        {
            int cantidad;
            Console.WriteLine($"Ingrese la cantidad en {ubicacion}:");
            while (!int.TryParse(Console.ReadLine(), out cantidad) || cantidad < 0)
            {
                Console.WriteLine("Error! Ingrese un número válido:");
            }
            return cantidad;
        }

        static float IngresoPrecio()
        {
            float precio;
            Console.WriteLine("Ingrese el precio del producto:");
            while (!float.TryParse(Console.ReadLine(), out precio) || precio < 0)
            {
                Console.WriteLine("Error! Ingrese un número válido:");
            }
            return precio;
        }

        // ======================== FUNCIONES BASE ========================

        static bool TryParseItem(string linea, out Item item)
        {
            item = new Item();
            if (string.IsNullOrWhiteSpace(linea)) return false;

            var partes = linea.Split(',');
            if (partes.Length < 5) return false;

            try
            {
                string GetValor(string p) => p.Split(':')[1].Trim();

                item.Nombre = GetValor(partes[0]);
                item.Codigo = int.Parse(GetValor(partes[1]));
                item.CantidadTienda = int.Parse(GetValor(partes[2]));
                item.CantidadBodega = int.Parse(GetValor(partes[3]));
                item.Precio = float.Parse(GetValor(partes[4]));
                return true;
            }
            catch { return false; }
        }

        static void UpsertItem(string ruta, Item nuevo)
        {
            var lineas = File.Exists(ruta)
                ? File.ReadAllLines(ruta, Encoding.UTF8).ToList()
                : new List<string>();

            int idx = -1;
            for (int i = 0; i < lineas.Count; i++)
            {
                if (lineas[i].Contains($"Codigo: {nuevo.Codigo}"))
                {
                    idx = i;
                    break;
                }
            }

            if (idx >= 0)
            {
                if (TryParseItem(lineas[idx], out var existente))
                {
                    existente.CantidadTienda += nuevo.CantidadTienda;
                    existente.CantidadBodega += nuevo.CantidadBodega;
                    existente.Precio = nuevo.Precio;
                    lineas[idx] = existente.ToString();
                    Console.WriteLine("✔ Producto existente actualizado correctamente.\n");
                }
            }
            else
            {
                lineas.Add(nuevo.ToString());
                Console.WriteLine("✔ Producto agregado exitosamente.\n");
            }

            File.WriteAllLines(ruta, lineas, Encoding.UTF8);
        }

        static void MostrarStock(string ruta)
        {
            if (!File.Exists(ruta))
            {
                Console.WriteLine("⚠ No existe ningún stock registrado aún.");
                return;
            }

            Console.Clear();
            Console.WriteLine("\n══════════════════════════════════════");
            Console.WriteLine("           STOCK COMPLETO");
            Console.WriteLine("══════════════════════════════════════\n");

            foreach (var linea in File.ReadLines(ruta, Encoding.UTF8))
            {
                var partes = linea.Split(',');
                foreach (var parte in partes)
                    Console.WriteLine("- " + parte.Trim());
                Console.WriteLine("──────────────────────────────────────");
            }

            Console.WriteLine("\nPresione una tecla para continuar...");
            Console.ReadKey();
        }

        static void BuscarPorCodigo(string ruta)
        {
            if (!File.Exists(ruta))
            {
                Console.WriteLine("⚠ No existe el archivo de stock.");
                return;
            }

            int codigo = IngresarCodigo();
            bool encontrado = false;

            foreach (var linea in File.ReadLines(ruta, Encoding.UTF8))
            {
                if (linea.Contains($"Codigo: {codigo}", StringComparison.InvariantCultureIgnoreCase))
                {
                    Console.WriteLine("\n══════════════════════════════════════");
                    Console.WriteLine("        RESULTADO DE BÚSQUEDA");
                    Console.WriteLine("══════════════════════════════════════\n");
                    var partes = linea.Split(',');
                    foreach (var parte in partes)
                        Console.WriteLine("- " + parte.Trim());
                    Console.WriteLine("──────────────────────────────────────");
                    encontrado = true;
                    break;
                }
            }

            if (!encontrado)
                Console.WriteLine("❌ No se encontró ningún producto con ese código.");

            Console.WriteLine("\nPresione una tecla para continuar...");
            Console.ReadKey();
        }

        static void BuscarPorNombre(string ruta)
        {
            if (!File.Exists(ruta))
            {
                Console.WriteLine("⚠ No existe el archivo de stock.");
                return;
            }

            string nombre = IngresarNombre();
            bool encontrado = false;

            foreach (var linea in File.ReadLines(ruta, Encoding.UTF8))
            {
                if (linea.Contains($"Nombre: {nombre}", StringComparison.InvariantCultureIgnoreCase))
                {
                    Console.WriteLine("\n══════════════════════════════════════");
                    Console.WriteLine("        RESULTADO DE BÚSQUEDA");
                    Console.WriteLine("══════════════════════════════════════\n");
                    var partes = linea.Split(',');
                    foreach (var parte in partes)
                        Console.WriteLine("- " + parte.Trim());
                    Console.WriteLine("──────────────────────────────────────");
                    encontrado = true;
                }
            }

            if (!encontrado)
                Console.WriteLine("❌ No se encontró ningún producto con ese nombre.");

            Console.WriteLine("\nPresione una tecla para continuar...");
            Console.ReadKey();
        }

        // ======================== CREAR STOCK ========================

        static void CrearStock(string ruta)
        {
            string reingreso = "si";
            do
            {
                Console.Clear();
                Console.WriteLine("\n══════════════════════════════════════");
                Console.WriteLine("          AGREGAR / ACTUALIZAR ITEM");
                Console.WriteLine("══════════════════════════════════════\n");

                string nombreProducto = IngresarNombre();
                int codigo = IngresarCodigo();
                int cantTienda = IngresoCantidad("tienda");
                int cantBodega = IngresoCantidad("bodega");
                float precioReal = IngresoPrecio();

                Item nuevo = new Item
                {
                    Nombre = nombreProducto,
                    Codigo = codigo,
                    CantidadTienda = cantTienda,
                    CantidadBodega = cantBodega,
                    Precio = precioReal
                };

                UpsertItem(ruta, nuevo);

                Console.WriteLine("¿Desea agregar otro producto? (si/no):");
                reingreso = (Console.ReadLine() ?? "").Trim().ToLowerInvariant();

            } while (reingreso == "si");
        }

        // ======================== MENÚ PRINCIPAL ========================

        static void MostrarMenu()
        {
            Console.Clear();
            Console.WriteLine("══════════════════════════════════════");
            Console.WriteLine("        SISTEMA DE STOCK v1.0");
            Console.WriteLine("══════════════════════════════════════");
            Console.WriteLine("1. Agregar o actualizar producto");
            Console.WriteLine("2. Buscar producto por código");
            Console.WriteLine("3. Buscar producto por nombre");
            Console.WriteLine("4. Mostrar todo el stock");
            Console.WriteLine("5. Salir");
            Console.WriteLine("──────────────────────────────────────");
            Console.Write("Seleccione una opción: ");
        }

        static void Main(string[] args)
        {
            string ruta = "/home/basturk/Escritorio/Don Bosco/Algoritms/Algoritmos_uno/Proyecto_1/stocks_beta/prueba.txt";

            while (true)
            {
                MostrarMenu();
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        CrearStock(ruta);
                        break;
                    case "2":
                        BuscarPorCodigo(ruta);
                        break;
                    case "3":
                        BuscarPorNombre(ruta);
                        break;
                    case "4":
                        MostrarStock(ruta);
                        break;
                    case "5":
                        Console.WriteLine("\nSaliendo del sistema... ¡Hasta luego!");
                        return;
                    default:
                        Console.WriteLine("Opción inválida. Presione una tecla para intentar de nuevo...");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
