using System;
using System.Collections.Generic;
using System.Web.UI;
using BE.Modelo;
using DAC.Repositorio;
using BL.Servicio;

namespace UserForms
{
    public partial class _Default : Page
    {
        private readonly IUsuarioService _usuarioService;
        public _Default()
        {
            _usuarioService = new ImplementUserService(new UsuarioImplement());
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarUsuarios();
            }
        }

        private void CargarUsuarios()
        {
            List<Usuario> listUser = _usuarioService.ObtenerUsuarios();
            gvUsuarios.DataSource = listUser;
            gvUsuarios.DataBind();
        }
    }
}