using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration; //This needs also to be added in the References of the project in order to work

namespace AdminEmpleados.DAL
{
    internal class Conexion
    {
        private string StringConn = ConfigurationManager.ConnectionStrings["AdminEmpleados"].ConnectionString; //Obtaining the string connection from the App.config
        SqlConnection conn;

        public SqlConnection EstablecerConexion()
        {
            this.conn = new SqlConnection(this.StringConn);
            return this.conn;
        }
        public bool ConexionTest()
        {
			try
			{                
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "SELECT * FROM Empleados";
                cmd.Connection = this.EstablecerConexion();
                this.conn.Open();
                cmd.ExecuteNonQuery();
                this.conn.Close();

                return true;
            }
			catch (Exception)
			{

				return false;
			}        
        }
    }
}
