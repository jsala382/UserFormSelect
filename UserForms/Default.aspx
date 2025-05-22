<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="UserForms._Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FirstContent" runat="server">
    <h2>Crear Usuario</h2>
    <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"></asp:Label>
    <br />
    <asp:Label ID="lblNombre" runat="server"></asp:Label>
    <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txtNombre"
        ErrorMessage="Campo nombre es obligatorio" ValidationGroup="CrearUsuario"></asp:RequiredFieldValidator>
    <br />
    Nombre:
    <br />
    <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox><br />
    <br />
    Direcciom:
    <br />
    <asp:TextBox ID="txtDireccion" runat="server"></asp:TextBox><br />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDireccion"
        ErrorMessage="Campo direccion es obligatorio" ValidationGroup="CrearUsuario"></asp:RequiredFieldValidator>
    <br />
    Telefono:
    <br />
    <asp:TextBox ID="txtTelefono" runat="server"></asp:TextBox><br />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTelefono"
        ErrorMessage="Campo telefono es obligatorio" ValidationGroup="CrearUsuario"></asp:RequiredFieldValidator>
    <br />
    <br />
    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnSave_Click"
        ValidationGroup="CrearUsuario" />
    <br />
    <br />
    <asp:Button ID="btnDescargar" runat="server" Text="Descargar" OnClick="btnDownload_Click"/>
    <br />
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Lista de Usuario</h2>
    <asp:GridView ID="gvUsuarios" runat="server" AutoGenerateColumns="false"
        DataKeyNames="Id"
        OnRowEditing="EditingUser"
        OnRowCancelingEdit="CancellingEditionUser"
        OnRowUpdating="UpdatingUser"
        OnRowDeleting="DeleteUser">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="id" ReadOnly="true" />
            <asp:TemplateField HeaderText="nombre" SortExpression="Nombre">
                <ItemTemplate>
                    <%# Eval("Nombre") %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtNombreEdit" runat="server" Text='<%# Bind("Nombre") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            

            <asp:TemplateField HeaderText="direccion" SortExpression="Direccion">
                <ItemTemplate>
                    <%# Eval ("Direccion") %>
                </ItemTemplate>

                <EditItemTemplate>
                    <asp:TextBox ID="txtDireccionEdit" runat="server" Text='<%# Bind("Direccion") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="telefono" SortExpression="Telefono">
                <ItemTemplate>
                     <%# Eval("Telefono") %>
                </ItemTemplate>
               
                <EditItemTemplate>
                    <asp:TextBox ID="txtTelefonoEdit" runat="server" Text='<%# Bind("Telefono") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>

                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Edit" Text="Editar" CausesValidation="false"></asp:LinkButton>
                        <asp:LinkButton ID="lnkDelete"
                            CommandName="Delete" Text="Eliminar"
                            runat="server"
                            OnClientClick="return confirm ('Esta seguro que desea elimnar este registro?');"
                            CausesValidation="false">
                        </asp:LinkButton>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:LinkButton ID="lnkUpdate" runat="server" CommandName="Update" Text="Actualizar" CausesValidation="false"></asp:LinkButton>
                        <asp:LinkButton ID="lnkCancel" runat="server" CommandName="Cancel" Text="Cancelar" CausesValidation="false"></asp:LinkButton>
                    </EditItemTemplate>
                </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
