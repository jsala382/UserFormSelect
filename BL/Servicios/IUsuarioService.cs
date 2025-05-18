
using System.Collections.Generic;
using BE.Modelo;

namespace BL.Servicio
{
    public interface IUsuarioService
    {
        List<Usuario> ObtenerUsuarios();
        Usuario CrearUsuario(Usuario usuario);

        Usuario ActualizarUsuario(Usuario usuario);
    }
}
