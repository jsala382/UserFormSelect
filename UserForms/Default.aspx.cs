using System;
using System.Collections.Generic;
using System.Web.UI;
using BE.Modelo;
using DAC.Repositorio;
using BL.Servicio;
using System.Web.UI.WebControls;
using System.Linq;
using System.Text;
using OfficeOpenXml;

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

        protected void btnDownloadXls(object sender, EventArgs e)
        {
            List<Usuario> userList = _usuarioService.ObtenerUsuarios();
            using (var package = new OfficeOpenXml.ExcelPackage())
            {
                var workSheet = package.Workbook.Worksheets.Add("Usuarios");
                workSheet.Cells[1, 1].Value = "ID";
                workSheet.Cells[1, 2].Value = "Nombre";
                workSheet.Cells[1, 3].Value = "Direccion";
                workSheet.Cells[1, 4].Value = "Telefono";

                int row = 2;
                foreach (var users in userList){
                    workSheet.Cells[row, 1].Value = users.Id;
                    workSheet.Cells[row, 2].Value = users.Nombre;
                    workSheet.Cells[row, 3].Value = users.Direccion;
                    workSheet.Cells[row, 4].Value = users.Telefono;
                    row++;
                }
                workSheet.Cells[workSheet.Dimension.Address].AutoFitColumns();
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition","attachement; filename= Usuarios.xls");
                Response.BinaryWrite(package.GetAsByteArray());
                Response.End();
            }
        }

        private Usuario CreateUser(Usuario user)
        {
            return _usuarioService.CrearUsuario(user);
        }
    }
}