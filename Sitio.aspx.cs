using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace SitioWeb
{
    public partial class Sitio : System.Web.UI.Page
    {
        string cadena = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarTecnologias();
                CargarSitios();
            }
        }

        void CargarTecnologias()
        {
            using (SqlConnection con = new SqlConnection(cadena))
            using (SqlCommand cmd = new SqlCommand("sp_Tecnologia_Leer", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                ddlTecnologia.DataSource = dt;
                ddlTecnologia.DataTextField = "Nombre";
                ddlTecnologia.DataValueField = "TecnologiaID";
                ddlTecnologia.DataBind();
            }
        }

        void CargarSitios()
        {
            using (SqlConnection con = new SqlConnection(cadena))
            using (SqlCommand cmd = new SqlCommand("sp_SitioWeb_Leer", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvSitios.DataSource = dt;
                gvSitios.DataBind();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(cadena))
                using (SqlCommand cmd = new SqlCommand("sp_SitioWeb_Crear", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                    cmd.Parameters.AddWithValue("@URL", txtURL.Text);
                    cmd.Parameters.AddWithValue("@TecnologiaID", ddlTecnologia.SelectedValue);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                lblMensaje.Text = "Sitio guardado correctamente.";
                Limpiar();
                CargarSitios();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
            }
        }

        protected void gvSitios_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            gvSitios.EditIndex = e.NewEditIndex;
            CargarSitios();
        }

        protected void gvSitios_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            gvSitios.EditIndex = -1;
            CargarSitios();
        }

        protected void gvSitios_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(gvSitios.DataKeys[e.RowIndex].Value);
            string nombre = ((TextBox)gvSitios.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
            string url = ((TextBox)gvSitios.Rows[e.RowIndex].Cells[2].Controls[0]).Text;
            string tecnologiaID = ((TextBox)gvSitios.Rows[e.RowIndex].Cells[3].Controls[0]).Text;

            using (SqlConnection con = new SqlConnection(cadena))
            using (SqlCommand cmd = new SqlCommand("sp_SitioWeb_Actualizar", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SitioWebID", id);
                cmd.Parameters.AddWithValue("@Nombre", nombre);
                cmd.Parameters.AddWithValue("@URL", url);
                cmd.Parameters.AddWithValue("@TecnologiaID", tecnologiaID);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            gvSitios.EditIndex = -1;
            CargarSitios();
        }

        protected void gvSitios_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(gvSitios.DataKeys[e.RowIndex].Value);
            using (SqlConnection con = new SqlConnection(cadena))
            using (SqlCommand cmd = new SqlCommand("sp_SitioWeb_Eliminar", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SitioWebID", id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            CargarSitios();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        void Limpiar()
        {
            txtNombre.Text = "";
            txtURL.Text = "";
            if (ddlTecnologia.Items.Count > 0)
                ddlTecnologia.SelectedIndex = 0;
        }
    }
}
