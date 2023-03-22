using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace UdeMBank
{
    class BancoUdeM
    {
        public List<Cliente> clientes = new List<Cliente>();
        public List<ATM> atms = new List<ATM>();
        private Administrador administrador;
        private Usuario usuario_ingresado;
        public double balance_general;


        public BancoUdeM(double balance_general)
        {
            this.balance_general = balance_general;
     

        }

        public void inicializar_usuario_ingresado(Usuario usuario_ingresado)
        {
            this.usuario_ingresado = usuario_ingresado;
        }

        public void inicializar_administrador(Administrador administrador)
        {
            this.administrador = administrador;
        }
        public void registrar_administrador(int id, string contrasena)
        {
            Administrador administrador = new Administrador(id, contrasena);
            inicializar_administrador(administrador);
            agregar_administrador_bd(administrador);
        }

        

        public Cliente buscar_cliente_por_id(int id)
        {
            if (clientes != null)
            {
                foreach (Cliente cliente in clientes)
                {
                    if (cliente.get_id() == id)
                    {

                        return cliente;

                    }
                    //el cliente con el id no se encuentra registrado

                }
            }
            return null;
                           

        }

        public ATM buscar_atm_por_codigo(int codigo)
        {
            if (atms != null)
            {
                foreach (ATM atm in atms)
                {
                    if (atm.get_codigo() == codigo)
                    {

                        return atm;

                    }
                    //el cliente con el id no se encuentra registrado

                }
            }
            return null;


        }


        public void registrar_cliente_nuevo(int id, string contrasena, double saldo_actual, int tipo_cliente)
        {
            if (buscar_cliente_por_id(id) == null)
            {
                string numero_tarjeta = generar_numero_tarjeta();
                if (tipo_cliente == 1)
                {
                    ClientePlatino cliente = this.administrador.registrar_cliente_platino(id, contrasena, saldo_actual, numero_tarjeta);
                    agregar_cliente_al_sistema(cliente);
         
                }
                else if (tipo_cliente == 2)
                {
                    ClienteRegular cliente = this.administrador.registrar_cliente_regular(id, contrasena, saldo_actual, numero_tarjeta);
                    agregar_cliente_al_sistema(cliente);

                }

            }

        }

        //Este metodo repite la creacion del numero tarjeta en caso de ser igual a otra
        public string generar_numero_tarjeta()
        {
            string numero_tarjeta = administrador.generar_numero_tarjeta();
            
            foreach (Cliente cliente in clientes)
            {
                while ((cliente.get_numero_tarjeta() == numero_tarjeta))
                {
                   administrador.generar_numero_tarjeta();
                }
                 return numero_tarjeta;
                        
            }
            return null;
             

        }

        public void agregar_cliente_al_sistema(Cliente cliente)
        {
            clientes.Add(cliente);
            agregar_cliente_bd(cliente);

        }

        //los metodos de agregar usuarios a bd crean por cada objeto un registro en la base de datos
        public void agregar_cliente_bd(Cliente cliente)
        {
            GestorBaseDatos gestor_bd = new GestorBaseDatos();
            gestor_bd.abrir_conexion();
            gestor_bd.agregar_cliente(cliente);
            gestor_bd.cerrar_conexion();

        }

        public void agregar_administrador_bd(Administrador administrador)
        {
            GestorBaseDatos gestor_bd = new GestorBaseDatos();
            gestor_bd.abrir_conexion();
            gestor_bd.agregar_administrador(administrador);
            gestor_bd.cerrar_conexion();
        }


        /*este metodo permite iniciar sesion verificando que un usuario ingrese con su id y contraseña correspondiente
        y su perfil correspondiente : Administrador/Usuario.*/
        public void iniciar_sesion(int id, string contrasena, int tipo_usuario)
        {
            Usuario usuario = new Usuario(id, contrasena);
            GestorBaseDatos gestor_bd = new GestorBaseDatos();
            (int, string) tupla = gestor_bd.ingresar_usuario(usuario, tipo_usuario);
            int id_usuario_ingresado = tupla.Item1;
            string contrasena_usuario_ingresado = tupla.Item2;
            usuario_ingresado = new Usuario(id_usuario_ingresado, contrasena_usuario_ingresado);
            inicializar_usuario_ingresado(usuario_ingresado);
            
            

        }

        public void crear_atm(double balance_general, int codigo, string nombre)
        {
            if (buscar_atm_por_codigo(codigo) == null)
            {
                double monto_atm = chequear_balance_general(balance_general);
                double saldo_ingresado = recargar_atm(monto_atm);
                ATM atm = administrador.crear_atm(codigo, nombre, saldo_ingresado);
                atms.Add(atm);
            }
            

        }

        public void descontar_balance_general(double monto_transaccion)
        {
            balance_general -= monto_transaccion;
        }    

        public void acumular_deposito_balance_general(double monto_transaccion)
        {
            balance_general += monto_transaccion;
        }


        public double chequear_balance_general(double balance_general)
        {
            if (balance_general <= 400)
            {
                double monto_atm = 50;
                Console.WriteLine("Recargo aprobado limitado; monto: 50");
                return monto_atm;
            }
            else
            {
                double monto_atm = 100;
                Console.WriteLine("Recargo apropbado exitoso, monto total: 100 ");
                return monto_atm;
            }
             
            
        }

        public void debe_recargar_atm(ATM atm, double balance_general)
        {
            if (atm.chequear_balance_actual())
            {
                double monto_atm = chequear_balance_general(balance_general);
                recargar_atm(monto_atm);
                atm.recibir_recarga(monto_atm);
                Console.WriteLine("El banco recargo el balance del ATM correctamente");

            }
            else
            {
                Console.WriteLine("No es necesario recargar el balance de ATM");

            }


        }

        public double recargar_atm(double monto_atm)
        {
            balance_general = balance_general - monto_atm;
            return balance_general;
        }

        public void retirar_desde_sucursal_virtual( double pmonto_transaccion)
        {
            Cliente cliente = buscar_cliente_por_id(usuario_ingresado.get_id());
            double monto_transaccion = cliente.aplicar_impuesto(pmonto_transaccion);

            if (cliente.tiene_saldo_disponible(monto_transaccion))
            {
                cliente.descontar_saldo_actual(monto_transaccion);
                descontar_balance_general(monto_transaccion);
                
            }
            Console.WriteLine("Retiro desde sucursal virtual exitoso");


        }

        public void depositar_desde_sucursal_virtual(double pmonto_transaccion)
        {
            Cliente cliente = buscar_cliente_por_id(usuario_ingresado.get_id());
            double monto_transaccion = cliente.aplicar_impuesto(pmonto_transaccion);

            if (cliente.tiene_saldo_disponible(monto_transaccion))
            {
                acumular_deposito_balance_general(monto_transaccion);
                cliente.descontar_saldo_actual(monto_transaccion);
                

            }
            Console.WriteLine("Retiro desde sucursal virtual exitoso");
        }

        public void retirar_desde_atm( double pmonto_transaccion)
        {   
            Cliente cliente = buscar_cliente_por_id(usuario_ingresado.get_id());
            double monto_transaccion = cliente.aplicar_impuesto(pmonto_transaccion);

            if (cliente.tiene_saldo_disponible(monto_transaccion))
            {   
                List<ATM> atms = buscar_atms_disponibles(monto_transaccion);
                mostrar_atms_disponibles(atms);
                Console.WriteLine( "Ingrese el codigo del cajero del que desea retirar su dinero");
                int codigo = int.Parse(Console.ReadLine());
                ATM atm = buscar_atm_por_codigo(codigo);
                cliente.descontar_saldo_actual(monto_transaccion);
                atm.descontar_monto_retirado(monto_transaccion);
                debe_recargar_atm(atm, balance_general);
                Console.WriteLine("Retiro finalizado exitosamente");


            }

        }

        public void depositar_desde_atm( List<ATM> atms, double pmonto_transaccion)
        {
            Cliente cliente = buscar_cliente_por_id(usuario_ingresado.get_id());
            double monto_transaccion = cliente.aplicar_impuesto(pmonto_transaccion);

            if (cliente.tiene_saldo_disponible(monto_transaccion))
            {
                mostrar_atms_disponibles(atms);
                Console.WriteLine("Ingrese el codigo del cajero del desde el que desea realizar el deposito");
                int codigo = int.Parse(Console.ReadLine());
                ATM atm = buscar_atm_por_codigo(codigo);
                atm.acumular_deposito(monto_transaccion);
                acumular_deposito_balance_general(monto_transaccion);
                cliente.descontar_saldo_actual(monto_transaccion);
                


            }
        }

        public void transferir_otro_cliente(double monto_transaccion)
        {

        }



        public List<ATM> buscar_atms_disponibles(double monto_transaccion)
        {
            List<ATM> atms_disponibles = new List<ATM>();

            foreach(ATM atm in atms)
            {
                if (atm.get_balance_actual() > monto_transaccion)
                {
                    atms_disponibles.Add(atm);
                }
                
            }
            return atms_disponibles;

        }

        public void mostrar_atms_disponibles(List<ATM> atms)
        {
            Console.WriteLine("-----------------------------------------");
            foreach (ATM atm in atms)
            {
                atm.mostrar_atm();
                Console.WriteLine("-----------------------------------------");

            }
        }







    }
}
