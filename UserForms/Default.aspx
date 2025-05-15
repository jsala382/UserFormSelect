<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="UserForms._Default" %>



<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Lista de Usuario</h2>
        <asp:GridView ID="gvUsuarios" runat="server" AutoGenerateColumns="false">
              <Columns>
                <asp:BoundField DataField="Id" HeaderText="id" SortExpression="Id"/>
                <asp:BoundField DataField="Nombre" HeaderText ="nombre" SortExpression="Nombre" />
                <asp:BoundField DataField ="Direccion" HeaderText="direccion" SortExpression="Direccion" />
                <asp:BoundField DataField="Telefono" HeaderText ="telefono" SortExpression="Telefono" />
             </Columns>
        </asp:GridView>
</asp:Content>
