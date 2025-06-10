<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Tecno.aspx.cs" Inherits="SitioWeb.Tecno" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        .form-container {
            max-width: 500px;
            background: #f9f9f9;
            padding: 20px 30px;
            border-radius: 8px;
            box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }

        h2 {
            color: #333;
            margin-bottom: 20px;
            font-weight: 700;
        }

        label {
            display: inline-block;
            width: 120px;
            font-weight: 600;
            color: #444;
            vertical-align: middle;
            margin-right: 10px;
        }

        input[type="text"], textarea {
            width: 300px;
            padding: 8px 12px;
            border: 1px solid #ccc;
            border-radius: 4px;
            font-size: 15px;
            transition: border-color 0.3s ease;
        }

        input[type="text"]:focus, textarea:focus {
            border-color: #007acc;
            outline: none;
        }

        textarea {
            resize: vertical;
            height: 80px;
        }

        .form-row {
            margin-bottom: 15px;
        }

        .btn {
            padding: 8px 18px;
            font-size: 15px;
            border-radius: 4px;
            cursor: pointer;
            border: none;
            transition: background-color 0.3s ease;
            margin-right: 10px;
            font-weight: 600;
        }

        .btn-primary {
            background-color: #007acc;
            color: white;
        }

        .btn-primary:hover {
            background-color: #005a99;
        }

        .btn-secondary {
            background-color: #aaa;
            color: white;
        }

        .btn-secondary:hover {
            background-color: #888;
        }

        #<%= lblMensaje.ClientID %> {
            font-weight: 600;
            margin-bottom: 15px;
        }

        table.gridview {
            width: 100%;
            border-collapse: collapse;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }

        table.gridview th, table.gridview td {
            padding: 12px 15px;
            border: 1px solid #ddd;
            text-align: left;
            font-size: 14px;
        }

        table.gridview th {
            background-color: #007acc;
            color: white;
            font-weight: 700;
        }

        table.gridview tr:nth-child(even) {
            background-color: #f3f7fb;
        }

        table.gridview tr:hover {
            background-color: #e1f0ff;
        }
    </style>

    <div class="form-container">
        <h2>Registro de Tecnologías</h2>

        <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"></asp:Label>

        <div class="form-row">
            <label for="<%= txtNombre.ClientID %>">Nombre:</label>
            <asp:TextBox ID="txtNombre" runat="server" />
        </div>

        <div class="form-row">
            <label for="<%= txtDescripcion.ClientID %>">Descripción:</label>
            <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" Rows="3" />
        </div>

        <div class="form-row" style="margin-top: 20px;">
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" CssClass="btn btn-primary" />
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" Visible="false" CssClass="btn btn-secondary" />
        </div>
    </div>

    <br />

    <asp:GridView ID="gvTecnologias" runat="server" AutoGenerateColumns="False" DataKeyNames="TecnologiaID"
        CssClass="gridview"
        OnRowEditing="gvTecnologias_RowEditing"
        OnRowDeleting="gvTecnologias_RowDeleting"
        OnRowCancelingEdit="gvTecnologias_RowCancelingEdit"
        OnRowUpdating="gvTecnologias_RowUpdating">

        <Columns>
            <asp:BoundField DataField="TecnologiaID" HeaderText="ID" ReadOnly="true" />

            <asp:TemplateField HeaderText="Nombre">
                <ItemTemplate>
                    <%# Eval("Nombre") %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtNombreEdit" runat="server" Text='<%# Bind("Nombre") %>' />
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Descripción">
                <ItemTemplate>
                    <%# Eval("Descripcion") %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtDescripcionEdit" runat="server" Text='<%# Bind("Descripcion") %>' TextMode="MultiLine" Rows="3" />
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:CommandField ShowEditButton="True" ShowDeleteButton="True"
                EditText="Editar" UpdateText="Actualizar" CancelText="Cancelar" DeleteText="Eliminar" />
        </Columns>

    </asp:GridView>

</asp:Content>


