using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdeMBank
{
    class Usuario
    {
        private int id;
        private string contrasena;

        public Usuario(int id, string contrasena)
        {
            this.id = id;
            this.contrasena = contrasena;
            
        }

        public int get_id() => this.id;
        public void set_id(int id) { this.id = id; }
        public string get_contrasena() => this.contrasena;
        public void set_contrasena(string contrasena) { this.contrasena = contrasena; }

        public void iniciar_sesion(Usuario usuario,int tipo_usuario, GestorBaseDatos gestor_bd)
        {
            iniciar_sesion(usuario, tipo_usuario, gestor_bd);
            //gestor_is.iniciar_sesion(usuario, tipo_usuario, gestor_bd);
            
        }

        //Aca podremos crear el metodo para validar usuario sea como administrador o cliente

    }
}
