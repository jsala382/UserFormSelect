<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="UserForms.Inicio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FirstContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style ="text-align: center;  margin-top: 50px;">
        <asp:FileUpload ID="tuArchivoExcel" runat="server" />
        <asp:Button ID = "btnEnviarXls" runat="server" Text ="Enviar XLS" OnClick ="btnEnviarClickXls"/>
        <asp:Label ID="lblResultado" runat="server" ForeColor ="Green"></asp:Label>
    </div>
</asp:Content>
