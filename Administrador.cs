using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdeMBank
{
    class Administrador : Usuario
    {
        
        
        public Administrador(int id_administrador, string contrasena_administrador) : base(id_administrador, contrasena_administrador)
        {
            
        }

        public string generar_numero_tarjeta()
        {
            Random random = new Random();
            int numero_tarjeta = random.Next(100000000, 999999999);
            return numero_tarjeta.ToString();    

        }

        public ClientePlatino registrar_cliente_platino(int id, string contrasena, double saldo_actual, string numero_tarjeta)
        {
            ClientePlatino cliente_platino = new ClientePlatino(id, contrasena, saldo_actual, numero_tarjeta);
            return cliente_platino;
        }

        public ClienteRegular registrar_cliente_regular(int id, string contrasena, double saldo_actual, string numero_tarjeta)
        {
            ClienteRegular cliente_regular = new ClienteRegular(id, contrasena, saldo_actual, numero_tarjeta);
            return cliente_regular;
        }

        public ATM crear_atm(int codigo, string nombre, double saldo_ingresado)
        {
            ATM atm = new ATM(codigo, nombre, saldo_ingresado);
            return atm;
        }


    }
}
