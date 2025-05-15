using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.Modelo;

namespace DAC.Repositorio
{
    public interface IUsuarioRepositorio
    {
        List<Usuario> ObtenerUsuarios();

    }
}
