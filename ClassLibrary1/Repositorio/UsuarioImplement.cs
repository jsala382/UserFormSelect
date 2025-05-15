using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using BE.Modelo;

namespace DAC.Repositorio
{
    public class UsuarioImplement : IUsuarioRepositorio
    {

        private readonly string connectionString = ConfigurationManager
                                                   .ConnectionStrings["ConexionUser"]
                                                   .ToString();
        public List<Usuario> ObtenerUsuarios()
        {
            List<Usuario> listUser = new List<Usuario>();
            using(SqlConnection connectionUser =
                    new SqlConnection(connectionString))
            {
                SqlCommand commandUser = new SqlCommand
                                         ("sp_SelectUser", connectionUser);
                connectionUser.Open();
                using(SqlDataReader readerUser = commandUser.ExecuteReader())
                {
                    while (readerUser.Read())
                    {
                        Usuario user = new Usuario
                        {
                            Id = Convert.ToInt32(readerUser["Id"].ToString()),
                            Nombre = readerUser["Nombre"].ToString(),
                            Direccion = readerUser["Direccion"].ToString(),
                            Telefono = readerUser["Telefono"].ToString()
                        };
                        listUser.Add(user);
                    }
                }
            }
            return listUser;
        }
    }
}
