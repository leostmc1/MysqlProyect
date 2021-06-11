using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;

namespace MysqlProyect
{
    public partial class Default : System.Web.UI.Page
    {
        //Cadena de coneccion de web config 
        private static string cadena = ConfigurationManager.ConnectionStrings["Cadena"].ConnectionString;
        private static MySqlConnection conexion = new MySqlConnection(cadena);

        private void Listar()
        {
            string consulta = "select * from tautor";
            MySqlCommand comando = new MySqlCommand(consulta, conexion);   // lleva la consulta a la base de datos 
            MySqlDataAdapter adapter = new MySqlDataAdapter(comando);      // trae la consulta 
            DataTable tabla = new DataTable();
            adapter.Fill(tabla);
            gv1.DataSource = tabla;
            gv1.DataBind();
            

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Listar();
        }

        protected void btnagregar_Click(object sender, EventArgs e)
        {
            try
            {
                string codAutor = cod.Text.Trim();
                string apellidos =ape.Text.Trim();
                string nombres =nom.Text.Trim();
                string nacionalidad =naci.Text.Trim();

                string consulta = "Insert into tautor values(@codautor,@apellidos,@nombres,@nacionalidad)";
                MySqlCommand comando = new MySqlCommand(consulta, conexion);

                // envio de parametros
                comando.Parameters.AddWithValue("@codautor", codAutor);
                comando.Parameters.AddWithValue("@apellidos", apellidos);
                comando.Parameters.AddWithValue("@nombres", nombres);
                comando.Parameters.AddWithValue("@nacionalidad", nacionalidad);
       

                // ejecutar insert

                conexion.Open();
                byte i = Convert.ToByte(comando.ExecuteNonQuery());
                conexion.Close();
                if (i == 1)
                    Listar();
                else
                    Response.Write("No se agrego");
            }
            catch(Exception ex)
            {
                Response.Write("ERROR: " + ex.Message);
            }
          

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                string consulta = "delete from tautor where codautor='" + cod.Text.Trim() + "'";
                MySqlCommand comando = new MySqlCommand(consulta, conexion);
                conexion.Open();
                byte i = Convert.ToByte(comando.ExecuteNonQuery());
                conexion.Close();
                if (i == 1) Listar();
                else Response.Write("Error al Eliminar");

            }
            catch(Exception ex)
            {
                conexion.Close();
                Response.Write("Error: " + ex.Message);

            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    string codAutor = cod.Text.Trim();
                    string apellidos = ape.Text.Trim();
                    string nombres = nom.Text.Trim();
                    string nacionalidad = naci.Text.Trim();
                    string consulta = "update tautor set Apellidos = @apellidos, Nombres = @nombres, Nacionalidad = @nacionalidad where CodAutor = @codautor";
                    MySqlCommand comando = new MySqlCommand(consulta, conexion);
                    comando.Parameters.AddWithValue("@codautor", codAutor);
                    comando.Parameters.AddWithValue("@apellidos", apellidos);
                    comando.Parameters.AddWithValue("@nombres", nombres);
                    comando.Parameters.AddWithValue("@nacionalidad", nacionalidad);
                    conexion.Open();
                    byte i = Convert.ToByte(comando.ExecuteNonQuery());
                    conexion.Close();
                    if (i == 1)
                    {
                        Listar();
                    }
                    else
                    {
                        Response.Write("Error al Actualizar");
                    }
                }
                catch (Exception ex)
                {
                    conexion.Close();
                    Response.Write("Error: " + ex.Message);

                }
            }



        }
    }
}