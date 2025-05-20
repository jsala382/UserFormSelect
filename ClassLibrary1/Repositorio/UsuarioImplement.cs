using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
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
                commandUser.CommandType = CommandType.StoredProcedure;
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
        
        
        public Usuario CrearUsuario(Usuario user)
        {
           using(SqlConnection connectioUserCreate = 
                               new SqlConnection(connectionString))
            {
                SqlCommand commandUserCreate = new SqlCommand
                                                   ("sp_InsertUser"
                                                   , connectioUserCreate);
                commandUserCreate.CommandType = CommandType.StoredProcedure;

                commandUserCreate.Parameters.AddWithValue("@Nombre", user.Nombre);
                commandUserCreate.Parameters.AddWithValue("@Direccion", user.Direccion);
                commandUserCreate.Parameters.AddWithValue("@Telefono", user.Telefono);

                SqlParameter idParam = new SqlParameter("@Id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                commandUserCreate.Parameters.Add(idParam);
                connectioUserCreate.Open();
                commandUserCreate.ExecuteNonQuery();
                user.Id = (int)idParam.Value;
                return user;
            }
        }

        public Usuario ActualizarUsuario(Usuario user)
        {
            using (SqlConnection connectionUserUpdate = new SqlConnection(connectionString))
            {
                SqlCommand commandUpdateUser = new SqlCommand("sp_UpdateUser",connectionUserUpdate);
                commandUpdateUser.CommandType = CommandType.StoredProcedure;
                commandUpdateUser.Parameters.AddWithValue("@Id", user.Id);
                commandUpdateUser.Parameters.AddWithValue("@Nombre", user.Nombre);
                commandUpdateUser.Parameters.AddWithValue("@Direccion", user.Direccion);
                commandUpdateUser.Parameters.AddWithValue("@Telefono", user.Telefono);
                connectionUserUpdate.Open();
                commandUpdateUser.ExecuteNonQuery();
                return user;
            }
        }

        public void BorrarUsuario(int id)
        {
            using (SqlConnection connectionUserDelete = new
                SqlConnection(connectionString))
            {
                SqlCommand commandDeleteUser = new SqlCommand("sp_DeleteUser", connectionUserDelete);
                commandDeleteUser.CommandType = CommandType.StoredProcedure;
                commandDeleteUser.Parameters.AddWithValue("@Id", id);
                connectionUserDelete.Open();
                commandDeleteUser.ExecuteNonQuery();


            }
        }
    }
}
