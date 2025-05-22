using System;
using System.Collections.Generic;
using System.Web.UI;
using BE.Modelo;
using DAC.Repositorio;
using BL.Servicio;
using System.Web.UI.WebControls;
using System.Linq;
using System.Text;

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

       protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario User = new Usuario
                {
                    Nombre = txtNombre.Text.Trim(),
                    Direccion = txtDireccion.Text.Trim(),
                    Telefono = txtTelefono.Text.Trim(),
                };
                Usuario UserCreated = CreateUser(User);
                CargarUsuarios();
                lblMensaje.Text = $"Usuario creado con el ID: {UserCreated.Id}";
            }catch(Exception ex)
            {
                lblMensaje.Text = $"Error al crear el Usuario" + ex.Message;
            }
           
        }

        protected void EditingUser(object sender, GridViewEditEventArgs e)
        {
            gvUsuarios.EditIndex = e.NewEditIndex;
            CargarUsuarios();
        }
        protected void UpdatingUser(object sender,GridViewUpdateEventArgs e)
        {

            int id = Convert.ToInt32(gvUsuarios.DataKeys[e.RowIndex].Value);
            GridViewRow row = gvUsuarios.Rows[e.RowIndex];
            string nombre = ((TextBox)row.FindControl("txtNombreEdit")).Text;
            string direcccion = ((TextBox)row.FindControl("txtDireccionEdit")).Text;
            string Telefono = ((TextBox)row.FindControl("txtTelefonoEdit")).Text;

            Usuario userActualized = new Usuario
            {
                Id = id,
                Nombre = nombre,
                Direccion = direcccion,
                Telefono = Telefono
            };
            _usuarioService.ActualizarUsuario(userActualized);
            gvUsuarios.EditIndex = -1;
            CargarUsuarios();
        }

        protected void CancellingEditionUser(object sender, GridViewCancelEditEventArgs e)
        {
            gvUsuarios.EditIndex = -1;
            CargarUsuarios(); 
        }

        protected void DeleteUser(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(gvUsuarios.DataKeys[e.RowIndex].Value);
            _usuarioService.BorrarUsuario(id);
            CargarUsuarios();
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            List<Usuario> userList = _usuarioService.ObtenerUsuarios();
            StringBuilder sb = new StringBuilder();
            foreach(var user in userList)
            {
                sb.AppendLine($"{user.Id}\t{user.Nombre}\t{user.Direccion}\t{user.Telefono}");
            }
            Response.Clear();
            Response.ContentType = "text/plain";
            Response.AddHeader("Content-Disposition", "attachment; filename=usuario.txt");
            Response.Write(sb.ToString());
            Response.End();
        }

        private Usuario CreateUser(Usuario user)
        {
            return _usuarioService.CrearUsuario(user);
        }
    }
}