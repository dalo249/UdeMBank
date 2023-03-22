using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdeMBank
{
    class ClientePlatino : Cliente
    {
        public ClientePlatino(int id_cliente, string contrasena_cliente, double saldo_actual, string numero_tarjeta, double impuesto = 0.8) : base(id_cliente, contrasena_cliente, saldo_actual, numero_tarjeta, impuesto)
        {

        }
    }
}
