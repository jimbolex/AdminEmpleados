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

        private SqlConnection StablishConn()
        {
            this.conn = new SqlConnection(this.StringConn);
            return this.conn;
        }
        

        /* Method that will process actions without data return (INSERTS, UPDATES Y DELETES) */
        public bool execNoDataRetCMD(SqlCommand sqlCMD)
        {
            try
            {
                SqlCommand cmd = sqlCMD;
                cmd.Connection = this.StablishConn();
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

        /* Methods that will return data from the database (SELECTS) */

        public DataSet execQuery(SqlCommand sqlQueryCmd)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();

            try
            {
                SqlCommand cmd = sqlQueryCmd;
                cmd.Connection = this.StablishConn();
                adapter.SelectCommand = cmd;
                this.conn.Open();
                adapter.Fill(ds);
                this.conn.Close();

                return ds;
            }
            catch (Exception)
            {

                return ds;
            }
        }
    }
}
