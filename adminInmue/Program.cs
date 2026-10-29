using System;
using System.Collections.Generic;
using System.Linq;

List<Propiedad> propiedades = new List<Propiedad>();
bool running = true;

// Menu Principal de la aplicación
while (running)
{
    Console.Clear();
    Console.WriteLine("\n Bienvenido, Usuario...seleccione una opción: ");
    Console.WriteLine("Mostrar todas las propiedades (1)");
    Console.WriteLine("Agregar una propiedad (2)");
    Console.WriteLine("Mostrar estadísticas (3)");
    Console.WriteLine("Salir (4)");

    string? opcion = Console.ReadLine()?.Trim();
    //Valiodacion de la entrada del usuario.
    switch (opcion)
    {
        case "1":
            Console.WriteLine("Option 1 Seleccionada.");
            mostrarPropiedades(propiedades);
            break;

        case "2":
            Console.WriteLine("Option 2 Seleccionada.");
            agregarPropiedad(propiedades);
            break;
        
        case "3":
            Console.WriteLine("Option 3 Seleccionada.");
            mostrarEstadisticas(propiedades);
            break;
        case "4":
            running = false;
            Console.WriteLine("\n Saliendo del programa, hasta pronto! ...");
            break;
        default:
            Console.WriteLine("\n Opción inválida. Por favor, seleccione una opción válida.");
            break;
    }

    if (running)
{
    Pause();
}
}

//Se crea una función para pausar la ejecución del programa 
// y esperar a que el usuario presione una tecla antes de continuar.
static void Pause()
{
    Console.WriteLine();
    Console.WriteLine("\n Presione cualquier tecla para continuar...");
    Console.ReadKey();
}
// Función para mostrar todas las propiedades
static void mostrarPropiedades(List<Propiedad> propiedades)
{
    //Se valida que si existan propiedades registradas, de lo contrario se muestra
    // un mensaje de que no hay propiedades registradas y se retorna de la función.
    if (propiedades.Count == 0)
    {
        Console.Clear();
        Console.WriteLine("\n No hay propiedades registradas.");
        return;
    }
    Console.Clear();
    Console.WriteLine("Propiedades registradas: ");
    //Se recorre la lista de propiedades y se imprime junto con sus detalles.
    foreach (var Propiedad in propiedades)
    {
        Console.WriteLine(
            $"ID: {Propiedad.Id} " + 
            $"Direccion: {Propiedad.Direccion} " + 
            $"Precio: {Propiedad.Precio} " + 
            $"Activo: {Propiedad.IsActivo} "
        );
    }
}

// Función para agregar una nueva propiedad
static void agregarPropiedad(List<Propiedad> propiedades)
{
    Console.WriteLine("Ingrese la dirección de la propiedad: ");
    string? direccion = Console.ReadLine();
    //Se valida que el campo no sea nulo. 
    if(string.IsNullOrWhiteSpace(direccion))
    {
        Console.WriteLine("La dirección no puede estar vacía.");
        return;
    }

    Console.WriteLine("Ingrese el precio de la propiedad: ");
    string? precioInput = Console.ReadLine();
    //Se valida que el campo no sea nulo.
    bool precioValido = decimal.TryParse(precioInput, out decimal precio);
    if (!precioValido || precio <= 0)
    {
        Console.WriteLine("El precio debe ser un número positivo.");
        return;
    }
    //Se obtiene el siguiente ID disponible para la nueva propiedad.
    int nextId = propiedades.Count == 0
    ? 1
    : propiedades.Max(p => p.Id) + 1;
    // Se crea una nueva instancia de la clase Propiedad 
    // con los datos ingresados por el usuario.
    Propiedad nuevaPropiedad = new()
    {
        Id = nextId,
        Direccion = direccion.Trim(),
        Precio = precio,
        IsActivo = true,
        CreateDate = DateTime.Now
    };

    propiedades.Add(nuevaPropiedad);
    Console.Clear();
    Console.WriteLine("Propiedad agregada exitosamente. ID: {nextId}");

}

// Función para mostrar estadísticas de propiedades
static void mostrarEstadisticas(List<Propiedad> propiedades)
{
    int totalPropiedades = propiedades.Count;
    int propiedadesActivas = propiedades.Count(p => p.IsActivo);
    decimal precioTotal = propiedades
    .Where(p => p.IsActivo)
    .Sum(p => p.Precio);

    Console.Clear();
    Console.WriteLine("\n--- Estadísticas de Propiedades ---\n");
    Console.WriteLine($"Total de propiedades registradas: {totalPropiedades}");
    Console.WriteLine($"Total de propiedades activas: {propiedadesActivas}");
    Console.WriteLine($"Precio Mensual: ${precioTotal:N2}");
}

// Aquí puedes definir la clase Propiedad

public class Propiedad
{
    public int Id { get; set; }
    public string Direccion { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public bool IsActivo { get; set; }
    public DateTime CreateDate { get; set; }
}
