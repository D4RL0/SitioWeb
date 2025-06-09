<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Sitio.aspx.cs" Inherits="SitioWeb.Sitio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Registro de Sitios Web</h2>
    <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"></asp:Label>
    <table>
        <tr>
            <td>Nombre:</td>
            <td><asp:TextBox ID="txtNombre" runat="server" /></td>
        </tr>
        <tr>
            <td>URL:</td>
            <td><asp:TextBox ID="txtURL" runat="server" /></td>
        </tr>
        <tr>
            <td>Tecnología:</td>
            <td>
                <asp:DropDownList ID="ddlTecnologia" runat="server"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" />
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" Visible="false" />
            </td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="gvSitios" runat="server" AutoGenerateColumns="False" DataKeyNames="SitioWebID"
        OnRowEditing="gvSitios_RowEditing"
        OnRowDeleting="gvSitios_RowDeleting"
        OnRowCancelingEdit="gvSitios_RowCancelingEdit"
        OnRowUpdating="gvSitios_RowUpdating">
        <Columns>
            <asp:BoundField DataField="SitioWebID" HeaderText="ID" ReadOnly="true" />
            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
            <asp:BoundField DataField="URL" HeaderText="URL" />
            <asp:BoundField DataField="TecnologiaID" HeaderText="Tecnología" />
            <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
        </Columns>
    </asp:GridView>
</asp:Content>
