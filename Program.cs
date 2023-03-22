using System;

namespace UdeMBank
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("hola");
            BancoUdeM banco = new BancoUdeM(50000000);
            Console.WriteLine("banco creado");
            Administrador administrador = new Administrador(123, "000");
            banco.agregar_administrador_bd(administrador);

            banco.registrar_cliente_nuevo(1122411, "PRUEBA3", 32000, 1);
            
            Console.WriteLine("listo");
        }
    }
}