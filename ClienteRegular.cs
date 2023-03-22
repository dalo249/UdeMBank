using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdeMBank
{
    class ClienteRegular : Cliente
    {
         
        public ClienteRegular(int id_cliente, string contrasena_cliente, double saldo_actual, string numero_tarjeta, double impuesto = 1.5) : base(id_cliente, contrasena_cliente, saldo_actual, numero_tarjeta, impuesto)
        {
            
        }
    }
}
