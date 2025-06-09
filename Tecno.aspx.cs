using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace SitioWeb
{
    public partial class Tecno : System.Web.UI.Page
    {
        string cadena = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarTecnologias();
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
                gvTecnologias.DataSource = dt;
                gvTecnologias.DataBind();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(cadena))
                using (SqlCommand cmd = new SqlCommand("sp_Tecnologia_Crear", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                    cmd.Parameters.AddWithValue("@Descripcion", txtDescripcion.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                lblMensaje.Text = "Tecnología guardada correctamente.";
                Limpiar();
                CargarTecnologias();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
            }
        }

        protected void gvTecnologias_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            gvTecnologias.EditIndex = e.NewEditIndex;
            CargarTecnologias();
        }

        protected void gvTecnologias_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            gvTecnologias.EditIndex = -1;
            CargarTecnologias();
        }

        protected void gvTecnologias_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(gvTecnologias.DataKeys[e.RowIndex].Value);
            string nombre = ((TextBox)gvTecnologias.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
            string descripcion = ((TextBox)gvTecnologias.Rows[e.RowIndex].Cells[2].Controls[0]).Text;

            using (SqlConnection con = new SqlConnection(cadena))
            using (SqlCommand cmd = new SqlCommand("sp_Tecnologia_Actualizar", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TecnologiaID", id);
                cmd.Parameters.AddWithValue("@Nombre", nombre);
                cmd.Parameters.AddWithValue("@Descripcion", descripcion);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            gvTecnologias.EditIndex = -1;
            CargarTecnologias();
        }

        protected void gvTecnologias_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(gvTecnologias.DataKeys[e.RowIndex].Value);
            using (SqlConnection con = new SqlConnection(cadena))
            using (SqlCommand cmd = new SqlCommand("sp_Tecnologia_Eliminar", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TecnologiaID", id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            CargarTecnologias();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        void Limpiar()
        {
            txtNombre.Text = "";
            txtDescripcion.Text = "";
        }
    }
}
