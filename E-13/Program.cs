using System;

Console.WriteLine("SISTEMA DE CONTROL DE MOTORES");
Console.WriteLine();

// Selección para determinar el tipo de motor
Console.WriteLine("Seleccione el tipo de motor a registrar:");
Console.WriteLine("1. Motor CD (Corriente Directa)");
Console.WriteLine("2. Motor AC (Corriente Alterna)");
Console.WriteLine("3. Servomotor");
Console.Write("Opción: ");
int opcion = Convert.ToInt32(Console.ReadLine());
Console.WriteLine();

// Variable de la clase base (Polimorfismo: puede almacenar cualquier subclase de Motor)
Motor? motorSeleccionado = null;

switch (opcion)
{
    case 1:
        motorSeleccionado = new MotorCD();
        break;
    case 2:
        motorSeleccionado = new MotorAC();
        break;
    case 3:
        motorSeleccionado = new ServoMotor();
        break;
    default:
        Console.WriteLine("Opción no válida.");
        return; // Termina el programa si la opción no existe
}

// UN SOLO BLOQUE para capturar y mostrar los datos de cualquier motor
motorSeleccionado.CapturarDatos();
motorSeleccionado.MostrarDatos();


// ================= CLASE BASE =================
class Motor
{
    // Propiedades comunes
    public string Identificador { get; set; } = "";
    public double Temperatura { get; set; }
    public double Corriente { get; set; }
    public double Velocidad { get; set; }
    public string Tipo { get; set; } = "Genérico";

    public string ObtenerTipoMotor()
    {
        return Tipo;
    }

    public string ObtenerEstado()
    {
        return Velocidad > 0 ? "Motor en marcha" : "Motor detenido";
    }

    public string RevisarTemperatura()
    {
        return Temperatura > 70 ? "ALERTA: Temperatura elevada" : "Temperatura normal";
    }

    // Método virtual: captura lo común y permite a las subclases agregar su propia captura
    public virtual void CapturarDatos()
    {
        Console.Write("Ingrese el identificador del motor: ");
        Identificador = Console.ReadLine() ?? "Sin identificador";

        Console.Write("Ingrese la temperatura del motor (°C): ");
        Temperatura = Convert.ToDouble(Console.ReadLine());

        Console.Write("Ingrese la corriente del motor (A): ");
        Corriente = Convert.ToDouble(Console.ReadLine());

        Console.Write("Ingrese la velocidad del motor en rpm: ");
        Velocidad = Convert.ToDouble(Console.ReadLine());
    }

    // Método virtual: muestra lo común y permite a las subclases mostrar sus datos propios
    public virtual void MostrarDatos()
    {
        Console.WriteLine();
        Console.WriteLine("DATOS DEL MOTOR");
        Console.WriteLine($"Tipo de motor: {ObtenerTipoMotor()}");
        Console.WriteLine($"Motor: {Identificador}");
        Console.WriteLine($"Temperatura: {Temperatura} °C");
        Console.WriteLine($"Corriente: {Corriente} A");
        Console.WriteLine($"Velocidad: {Velocidad} rpm");
        Console.WriteLine($"Estado: {ObtenerEstado()}");
        Console.WriteLine($"Revisión de temperatura: {RevisarTemperatura()}");
    }
}


// ================= CLASES DERIVADAS =================
class MotorCD : Motor
{
    public double Voltaje { get; set; }

    public MotorCD()
    {
        Tipo = "Corriente Directa";
    }

    public override void CapturarDatos()
    {
        base.CapturarDatos(); // Llama al bloque común de la clase Motor
        Console.Write("Ingrese el voltaje del motor (V): ");
        Voltaje = Convert.ToDouble(Console.ReadLine());
    }

    public override void MostrarDatos()
    {
        base.MostrarDatos(); // Muestra los datos comunes de la clase Motor
        Console.WriteLine($"Voltaje: {Voltaje} V");
    }
}

class MotorAC : Motor
{
    public double Fasedeoperacion { get; set; }

    public MotorAC()
    {
        Tipo = "Corriente Alterna";
    }

    public double ObtenerFrecuencia()
    {
        int numeroPolos = 4;
        return (Velocidad * numeroPolos) / 120.0;
    }

    public override void CapturarDatos()
    {
        base.CapturarDatos(); // Reutiliza la captura común
        Console.Write("Ingrese la fase de operación (1 o 3): ");
        Fasedeoperacion = Convert.ToDouble(Console.ReadLine());
    }

    public override void MostrarDatos()
    {
        base.MostrarDatos(); // Reutiliza la impresión común
        Console.WriteLine($"Fase de operación: {Fasedeoperacion} fases");
        Console.WriteLine($"Frecuencia calculada: {ObtenerFrecuencia()} Hz");
    }
}

class ServoMotor : Motor
{
    public double PosicionAngular { get; set; }

    public ServoMotor()
    {
        Tipo = "Servo Motor";
    }

    public string DeterminarPosicion()
    {
        if (PosicionAngular < 0 || PosicionAngular > 180)
            return "Fuera de rango operativo (debe estar entre 0° y 180°)";
        else if (PosicionAngular == 0)
            return "Posición inicial (0°)";
        else if (PosicionAngular == 90)
            return "Posición central / neutra (90°)";
        else if (PosicionAngular == 180)
            return "Posición de tope máximo (180°)";
        else
            return $"Posicionado a {PosicionAngular}°";
    }

    public override void CapturarDatos()
    {
        base.CapturarDatos(); // Reutiliza la captura común
        Console.Write("Ingrese la posición angular en grados (0 - 180): ");
        PosicionAngular = Convert.ToDouble(Console.ReadLine());
    }

    public override void MostrarDatos()
    {
        base.MostrarDatos(); // Reutiliza la impresión común
        Console.WriteLine($"Posición Angular: {PosicionAngular}°");
        Console.WriteLine($"Evaluación de posición: {DeterminarPosicion()}");
    }
}