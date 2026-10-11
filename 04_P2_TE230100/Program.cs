namespace E04_P2_CargaBateria
{
    public class CargadorBateria
    {
        // CONSTANTES Y PROPIEDADES DE LA CLASE
        public const double VoltajeLimite = 12.6;
        public double VoltajeActual { get; private set; }
        public double Incremento { get; private set; }
        public int CicloActual { get; private set; }

        // MÉTODO CONSTRUCTOR
        public CargadorBateria(double voltajeInicial, double incremento)
        {
            VoltajeActual = voltajeInicial;
            Incremento = incremento;
            CicloActual = 0;
        }

        // MÉTODO PARA EJECUTAR UN CICLO DE CARGA
        public bool AplicarCicloCarga()
        {
            if (VoltajeActual >= VoltajeLimite)
            {
                return false;
            }

            CicloActual++;
            VoltajeActual += Incremento;

            // Ajuste para evitar superar los 12.6 V de límite
            if (VoltajeActual > VoltajeLimite)
            {
                VoltajeActual = VoltajeLimite;
            }

            return true;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=================================================");
            Console.WriteLine("     SISTEMA DE CARGA DE BATERÍA (12.6 V)        ");
            Console.WriteLine("=================================================\n");

            Console.Write("Ingrese el voltaje inicial de la batería (V): ");
            double vInicial;
            while (!double.TryParse(Console.ReadLine(), out vInicial) || vInicial < 0)
            {
                Console.Write("Entrada inválida. Ingrese un voltaje válido (V) >= 0: ");
            }

            if (vInicial >= CargadorBateria.VoltajeLimite)
            {
                Console.WriteLine($"\nLa batería ya tiene {vInicial} V. No requiere carga.");
                Console.ReadKey();
                return;
            }

            Console.Write("Ingrese el incremento de voltaje por ciclo (V): ");
            double incremento;
            while (!double.TryParse(Console.ReadLine(), out incremento) || incremento <= 0)
            {
                Console.Write("El incremento debe ser mayor a 0 V para evitar ciclos infinitos. Reintente: ");
            }

            CargadorBateria cargador = new CargadorBateria(vInicial, incremento);

            Console.WriteLine("\n-------------------------------------------------");
            Console.WriteLine("INICIANDO PROCESO DE CARGA:");
            Console.WriteLine("-------------------------------------------------");

            while (cargador.AplicarCicloCarga())
            {
                Console.WriteLine($"Ciclo #{cargador.CicloActual}: Voltaje = {cargador.VoltajeActual:F2} V");
            }

            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine($"PROCESO FINALIZADO: Batería cargada a {cargador.VoltajeActual:F1} V en {cargador.CicloActual} ciclos.");
            Console.WriteLine("-------------------------------------------------");

            Console.WriteLine("\nPresione cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}
