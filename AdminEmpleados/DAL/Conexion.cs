using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace AdminEmpleados.DAL
{
    internal class Conexion
    {
        public bool ConexionTest()
        {
			try
			{
                SqlConnection conn = new SqlConnection("Data Source=LAPTOP-S6DV4KF5;Initial Catalog=dbEmpDep1;Integrated Security=True;Encrypt=False");
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "SELECT * FROM Empleados";
                cmd.Connection = conn;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                return true;
            }
			catch (Exception)
			{

				return false;
			}        
        }
    }
}
