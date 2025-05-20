
using System.Collections.Generic;
using System.ComponentModel;
using BE.Modelo;

namespace BL.Servicio
{
    public interface IUsuarioService
    {
        List<Usuario> ObtenerUsuarios();
        Usuario CrearUsuario(Usuario usuario);

        Usuario ActualizarUsuario(Usuario usuario);
        void BorrarUsuario(int id);
    }
}
