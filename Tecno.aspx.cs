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
            lblMensaje.ForeColor = System.Drawing.Color.Red;

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                lblMensaje.Text = "El nombre es obligatorio.";
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(cadena))
                using (SqlCommand cmd = new SqlCommand("sp_Tecnologia_Crear", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text.Trim());
                    cmd.Parameters.AddWithValue("@Descripcion", string.IsNullOrWhiteSpace(txtDescripcion.Text) ? (object)DBNull.Value : txtDescripcion.Text.Trim());
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                lblMensaje.ForeColor = System.Drawing.Color.Green;
                lblMensaje.Text = "Tecnología guardada correctamente.";
                Limpiar();
                CargarTecnologias();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al guardar: " + ex.Message;
            }
        }

        protected void gvTecnologias_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvTecnologias.EditIndex = e.NewEditIndex;
            CargarTecnologias();
        }

        protected void gvTecnologias_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvTecnologias.EditIndex = -1;
            CargarTecnologias();
            lblMensaje.Text = "";
        }

        protected void gvTecnologias_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            lblMensaje.ForeColor = System.Drawing.Color.Red;

            int id = Convert.ToInt32(gvTecnologias.DataKeys[e.RowIndex].Value);

            TextBox txtNombreEdit = (TextBox)gvTecnologias.Rows[e.RowIndex].FindControl("txtNombreEdit");
            TextBox txtDescripcionEdit = (TextBox)gvTecnologias.Rows[e.RowIndex].FindControl("txtDescripcionEdit");

            if (txtNombreEdit == null || txtDescripcionEdit == null)
            {
                lblMensaje.Text = "Error al obtener los controles de edición.";
                return;
            }

            string nombre = txtNombreEdit.Text.Trim();
            string descripcion = txtDescripcionEdit.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                lblMensaje.Text = "El nombre es obligatorio.";
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(cadena))
                using (SqlCommand cmd = new SqlCommand("sp_Tecnologia_Actualizar", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TecnologiaID", id);
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", string.IsNullOrWhiteSpace(descripcion) ? (object)DBNull.Value : descripcion);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                lblMensaje.ForeColor = System.Drawing.Color.Green;
                lblMensaje.Text = "Tecnología actualizada correctamente.";
                gvTecnologias.EditIndex = -1;
                CargarTecnologias();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al actualizar: " + ex.Message;
            }
        }

        protected void gvTecnologias_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            lblMensaje.ForeColor = System.Drawing.Color.Red;

            int id = Convert.ToInt32(gvTecnologias.DataKeys[e.RowIndex].Value);
            try
            {
                using (SqlConnection con = new SqlConnection(cadena))
                using (SqlCommand cmd = new SqlCommand("sp_Tecnologia_Eliminar", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TecnologiaID", id);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                lblMensaje.ForeColor = System.Drawing.Color.Green;
                lblMensaje.Text = "Tecnología eliminada correctamente.";
                CargarTecnologias();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al eliminar: " + ex.Message;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Limpiar();
            lblMensaje.Text = "";
        }

        void Limpiar()
        {
            txtNombre.Text = "";
            txtDescripcion.Text = "";
        }
    }
}
