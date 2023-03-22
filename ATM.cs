using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace UdeMBank
{
    class ATM
    {
        private int codigo;
        //private string nombre;
        private double balance_actual;
        private double limite_inferior_balance = 30;


        public ATM(int codigo, string nombre, double balance_actual) 
        {
            this.codigo = codigo;
            //this.nombre = nombre;
            this.balance_actual = balance_actual;
        
        }

        public int get_codigo() => this.codigo;

        public void set_codigo(int codigo) { this.codigo = codigo; }

        public double get_balance_actual() => this.balance_actual;

        public void set_balance_actual(double balance_actual) {this.balance_actual = balance_actual;}

        public void mostrar_atm()
        {
            Console.WriteLine(" ATM con codigo: " + codigo + " balance_actual "+ balance_actual );
        }



        public void actualizar_balance_actual(double monto_atm)
        {
            balance_actual = get_balance_actual();
            balance_actual += monto_atm; 
        }

        public bool chequear_balance_actual()
        {
            if (get_balance_actual() <= limite_inferior_balance) 
            {
                Console.WriteLine("Solicitar recargo al banco: balance actual del ATM muy bajo");
                return true;
                
            }
            else
            { 
                return false;
            }
        }

        public void recibir_recarga(double monto_atm)
        {
            balance_actual += monto_atm;
        }

        public void descontar_monto_retirado(double monto_transaccion)
        {
            balance_actual -= monto_transaccion; 
        }

        public void acumular_deposito(double monto_transaccion)
        {
            balance_actual += monto_transaccion;
        }


     



    }
}
