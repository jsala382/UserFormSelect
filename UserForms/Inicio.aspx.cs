using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserForms
{
    public partial class Inicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnEnviarClickXls(object sender, EventArgs e)
        {
            string extension = Path.GetExtension(tuArchivoExcel.FileName).ToLower();
            if (tuArchivoExcel.HasFile)
            {
                if (extension.Contains("xls"))
                {

                    try
                    {
                        using (var package = new OfficeOpenXml
                                                .ExcelPackage(tuArchivoExcel.
                                                 PostedFile.InputStream))
                        {
                            var worksheet = package.Workbook.Worksheets[0];
                            int rowCount = worksheet.Dimension.Rows;
                            string connectionString = ConfigurationManager
                                                     .ConnectionStrings["ConexionUser"]
                                                     .ConnectionString;
                            using (SqlConnection connection = new SqlConnection(connectionString))
                            {
                                connection.Open();
                                for (int row = 2; row <= rowCount; row++)
                                {
                                    string nombre = worksheet.Cells[row, 2].Text;
                                    string direccion = worksheet.Cells[row, 3].Text;
                                    string telefono = worksheet.Cells[row, 4].Text;

                                    if (string.IsNullOrWhiteSpace(nombre) &&
                                        string.IsNullOrWhiteSpace(direccion) &&
                                        string.IsNullOrWhiteSpace(telefono))
                                    {
                                        continue;
                                    }

                                    if (!string.IsNullOrWhiteSpace(nombre) &&
                                        !string.IsNullOrWhiteSpace(direccion) &&
                                        !string.IsNullOrWhiteSpace(telefono))
                                    {
                                        using (SqlCommand cmd = new SqlCommand("sp_InsertUser", connection))
                                        {
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            cmd.Parameters.AddWithValue("@Nombre", nombre);
                                            cmd.Parameters.AddWithValue("@Direccion", direccion);
                                            cmd.Parameters.AddWithValue("@Telefono", telefono);

                                            SqlParameter outputIdParam = new SqlParameter("@Id", SqlDbType.Int);
                                            outputIdParam.Direction = ParameterDirection.Output;
                                            cmd.Parameters.Add(outputIdParam);

                                            cmd.ExecuteNonQuery();
                                        }
                                    }
                                    else
                                    {
                                        lblResultado.Text = $" Fila {row} inválida. Revisa el archivo.";
                                        return;
                                    }
                                }
                                lblResultado.Text = " Datos insertados correctamente.";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        lblResultado.Text = "Error: " + ex.Message;
                    }
                }
                else if (extension.Contains("txt"))
                {
                    using (StreamReader reader = new StreamReader(tuArchivoExcel.PostedFile.InputStream))
                    {
                        string connectionUser = ConfigurationManager.ConnectionStrings["ConexionUser"].ConnectionString;
                        using (SqlConnection sqlConnection = new SqlConnection(connectionUser))
                        {
                            sqlConnection.Open();
                            int linea = 1;
                            while (!reader.EndOfStream)
                            {
                                string line = reader.ReadLine();
                                string[] campos = line.Split('\t');
                                if (campos.Length >= 3)
                                {
                                    string nombre = campos[0].Trim();
                                    string direccion = campos[1].Trim();
                                    string telefono = campos[2].Trim();
                                    if (!string.IsNullOrWhiteSpace(nombre) &&
                                        !string.IsNullOrWhiteSpace(direccion) &&
                                        !string.IsNullOrWhiteSpace(telefono))
                                    {
                                        using (SqlCommand cmd = new SqlCommand("sp_InsertUser", sqlConnection))
                                        {
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            cmd.Parameters.AddWithValue("@Nombre", nombre);
                                            cmd.Parameters.AddWithValue("@Direccion", direccion);
                                            cmd.Parameters.AddWithValue("@Telefono", telefono);

                                            SqlParameter outputIdParam = new SqlParameter("@Id", SqlDbType.Int);
                                            outputIdParam.Direction = ParameterDirection.Output;
                                            cmd.Parameters.Add(outputIdParam);

                                            cmd.ExecuteNonQuery();
                                        }
                                    }
                                    else
                                    {
                                        lblResultado.Text = $"La linea {linea} esta vacia";
                                        return;
                                    }
                                    linea++;
                                }
                                lblResultado.Text = "Datos insertados correctamente desde el TXT.";
                            }
                        }
                    }
                   
                }
            }
            else
            {
                lblResultado.Text = "Por favor, selecciona el archivo 'usuario' para subir.";
            }
        }

    }
}