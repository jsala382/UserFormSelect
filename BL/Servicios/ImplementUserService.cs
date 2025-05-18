using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using  BE.Modelo;
using DAC.Repositorio;

namespace BL.Servicio
{
    public class ImplementUserService : IUsuarioService
    {
        private readonly IUsuarioRepositorio _userRepository;
        public ImplementUserService(IUsuarioRepositorio userRepository)
        {
            this._userRepository = userRepository;
        }

        public List<Usuario> ObtenerUsuarios()
        {
            return _userRepository.ObtenerUsuarios();
        }

        public Usuario CrearUsuario(Usuario user)
        {
            return _userRepository.CrearUsuario(user);
        }

        public Usuario ActualizarUsuario(Usuario user)
        {
            return _userRepository.ActualizarUsuario(user);
        }
    }
}