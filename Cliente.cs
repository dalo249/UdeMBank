using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdeMBank
{
    class Cliente : Usuario
    {
        
        private double saldo_actual;
        private string numero_tarjeta;
        private double impuesto;
        public Cliente(int id_cliente, string contrasena_cliente, double saldo_actual, string numero_tarjeta, double impuesto) : base(id_cliente, contrasena_cliente)
        {
            this.saldo_actual = saldo_actual;
            this.impuesto = impuesto;
            this.numero_tarjeta = numero_tarjeta;
        }

        public double get_saldo_actual() => this.saldo_actual;
        public void set_saldo_actual(double saldo_actual) { this.saldo_actual = saldo_actual; }

        public string get_numero_tarjeta() => this.numero_tarjeta;
        public void set_numero_tarjeta(string numero_tarjeta) { this.numero_tarjeta = numero_tarjeta; }

        public double aplicar_impuesto(double monto_transaccion)
        {
            return monto_transaccion * (monto_transaccion * impuesto);
            
        }

        public void retirar_dinero(int cantidad)
        {
            if((this.saldo_actual - aplicar_impuesto(cantidad) ) >= 0)
            {
                this.saldo_actual = this.saldo_actual - aplicar_impuesto(cantidad);

            }
            else
            {
                Console.WriteLine("saldo insuficiente");
            }

        }

        public bool tiene_saldo_disponible(double monto_transaccion)
        {
            if (this.saldo_actual >= monto_transaccion)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void descontar_saldo_actual(double monto_transaccion)
        {
            saldo_actual -= monto_transaccion;
        }

        
    }
}
