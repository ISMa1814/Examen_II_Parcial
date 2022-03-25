﻿using Datos.Entidades;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Accesos
{
    public class UsuarioDA
    {
        readonly string cadena = "Server=localhost; Port=3306; Database=examen2; Uid=root; Pwd=0708;";

        MySqlConnection conn;
        MySqlCommand cmd;

        //Este metodo se usa para COMPROBAR SI EL USUARIO ESTA ACTIVO
        public Usuario Login(string codigo, string clave)
        {
            Usuario user = null;

            try
            {
                string sql = "SELECT * FROM usuario WHERE Codigo = @Codigo AND Clave = @Clave;";
                conn = new MySqlConnection(cadena);
                conn.Open();

                cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Codigo", codigo);
                cmd.Parameters.AddWithValue("@Clave", clave);

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    user = new Usuario();
                    user.Codigo = reader[0].ToString();
                    user.Nombre = reader[1].ToString();
                    user.Clave = reader[2].ToString();
                    user.EstaActivo = Convert.ToBoolean(reader[3]);
                }
                reader.Close();
                conn.Close();
            }
            catch (Exception ex)
            {

            }
            return user;
        }
    }
}
