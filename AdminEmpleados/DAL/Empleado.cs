using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminEmpleados.BLL;
using System.Data;
using System.Data.SqlClient;

namespace AdminEmpleados.DAL
{
    internal class Empleado
    {
        Conexion conn;

        public Empleado() 
        {
            conn = new Conexion();
        }

        public bool InsertEmpl(EmpleadoBLL Empl)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO [dbEmpDep1].[dbo].[Empleados] (nombres, apellido1, apellido2, correo, foto) VALUES (@nombres, @apellido1, @apellido2, @correo, @foto)");
            cmd.Parameters.Add("@nombres", SqlDbType.VarChar).Value = Empl.NombreEmpleado;
            cmd.Parameters.Add("@apellido1", SqlDbType.VarChar).Value = Empl.PrimerApellido;
            cmd.Parameters.Add("@apellido2", SqlDbType.VarChar).Value = Empl.SegundoApellido;
            cmd.Parameters.Add("@correo", SqlDbType.VarChar).Value = Empl.Correo;
            cmd.Parameters.Add("@foto", SqlDbType.Image).Value = Empl.FotoEmpleado;
            return conn.execNonQuery(cmd);
        }

        public DataSet getAllEmployees()
        {
            SqlCommand query = new SqlCommand("SELECT [pkEmpId] as ID, [nombres] as [Nombre(s)], [apellido1] as [Primer Apellido],[apellido2] as [Segundo Apellido], [correo] as Email FROM [dbEmpDep1].[dbo].[Empleados]");

            return conn.execQuery(query);
        }
    }
}
