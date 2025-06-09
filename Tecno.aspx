<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Tecno.aspx.cs" Inherits="SitioWeb.Tecno" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Registro de Tecnologías</h2>
    <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"></asp:Label>
    <table>
        <tr>
            <td>Nombre:</td>
            <td><asp:TextBox ID="txtNombre" runat="server" /></td>
        </tr>
        <tr>
            <td>Descripción:</td>
            <td><asp:TextBox ID="txtDescripcion" runat="server" /></td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" />
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" Visible="false" />
            </td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="gvTecnologias" runat="server" AutoGenerateColumns="False" DataKeyNames="TecnologiaID"
        OnRowEditing="gvTecnologias_RowEditing"
        OnRowDeleting="gvTecnologias_RowDeleting"
        OnRowCancelingEdit="gvTecnologias_RowCancelingEdit"
        OnRowUpdating="gvTecnologias_RowUpdating">
        <Columns>
            <asp:BoundField DataField="TecnologiaID" HeaderText="ID" ReadOnly="true" />
            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
            <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
            <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
        </Columns>
    </asp:GridView>
</asp:Content>
