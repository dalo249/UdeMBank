using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace UdeMBank
{
    class GestorBaseDatos
    {
        string direccionbd = "Data source=DESKTOP-RL1O9IS; Initial Catalog=GestionLoginUdeMBank; Integrated Security=True";
        public SqlConnection conectarbd = new SqlConnection();

        public GestorBaseDatos()
        {
            conectarbd.ConnectionString = direccionbd;
        }

        public void abrir_conexion()
        {
            try
            {
                conectarbd.Open();
                Console.WriteLine("Se abrio conexion con la base de datos de login de usuarios");
            }
            catch (Exception excepcion)
            {
                Console.WriteLine("No fue posible abrir la base de datos de login de usuarios \n Error: " + excepcion.Message);
            }

        }

        public void cerrar_conexion()
        {
            conectarbd.Close();
            Console.WriteLine("Se cerro conexion con base de datos de login de usuarios");
        }

        public void agregar_cliente(Cliente cliente)
        {
            int tipo_usuario = 1;
            string consulta_agregar = "INSERT INTO USUARIO(Id,contrasena,tipo_usuario) VALUES(@id ,@contrasena,@tipo_usuario)";

            SqlCommand comando = new SqlCommand(consulta_agregar, conectarbd);
            comando.Parameters.AddWithValue("@id", cliente.get_id());
            comando.Parameters.AddWithValue("@contrasena", cliente.get_contrasena());
            comando.Parameters.AddWithValue("@tipo_usuario", tipo_usuario);

            try
            {
                comando.ExecuteNonQuery();
            }
            catch 
            {
                Console.WriteLine("Error : No es posible registrarse, ya existe un usuario registrado con el id " + cliente.get_id());
            }

        }
        

        public void agregar_administrador(Administrador administrador)
        {
            int tipo_usuario = 2;
            string consulta_agregar = "INSERT INTO USUARIO(Id,contrasena,tipo_usuario) VALUES(@id ,@contrasena,@tipo_usuario)";

            SqlCommand comando = new SqlCommand(consulta_agregar, conectarbd);
            comando.Parameters.AddWithValue("@id" , administrador.get_id());
            comando.Parameters.AddWithValue("@contrasena", administrador.get_contrasena());
            comando.Parameters.AddWithValue("@tipo_usuario", tipo_usuario);
            
            try
            {
                comando.ExecuteNonQuery();
            }
            catch 
            {
                Console.WriteLine("Error: No es posible registrarse, ya existe un administrador registrado con el id " + administrador.get_id());
            }

        }

        public (int, string) ingresar_usuario(Usuario usuario, int tipo_usuario)
        {
            string consulta_buscar = "SELECT * FROM USUARIO WHERE Id = @id AND contrasena = @contrasena AND tipo_usuario = @tipo_usuario";
            SqlCommand comando = new SqlCommand(consulta_buscar, conectarbd);
            comando.Parameters.AddWithValue("@id", usuario.get_id());
            comando.Parameters.AddWithValue("@contrasena", usuario.get_contrasena());
            comando.Parameters.AddWithValue("@tipo_usuario", tipo_usuario);

            SqlDataReader lector = comando.ExecuteReader();
            if (lector.Read())
            {
                int id = lector.GetInt32(0);
                string contrasena = lector.GetString(1);
                lector.Close();
                
                return(id, contrasena);
            }
            return (00, "none");


            
            
        }

     

     
       

    }
}
     
    
