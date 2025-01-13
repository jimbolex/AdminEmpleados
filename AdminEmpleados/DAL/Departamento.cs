using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminEmpleados.BLL;

namespace AdminEmpleados.DAL
{
    internal class Departamento
    {
        Conexion conn;
        public Departamento()
        {
            conn = new Conexion();
        }


        public bool InsertDept(DepartamentoBLL oDepartment)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO [dbEmpDep1].[dbo].[Departamentos] (departamento) VALUES (@depto)");
            cmd.Parameters.Add("@depto", SqlDbType.VarChar).Value = oDepartment.Departamento;
            return conn.execNoDataRetCMD(cmd);


        }

        public DataSet getAllDepartments()
        {
            SqlCommand query = new SqlCommand("SELECT pkDepID as [ID], departamento as [Departamento] FROM [dbEmpDep1].[dbo].[Departamentos]");

            return conn.execQuery(query);
        }

        public DataSet getUniqueDepartments()
        {
            SqlCommand query = new SqlCommand("SELECT distinct pkDepID as [ID], departamento as [Departamento] FROM [dbEmpDep1].[dbo].[Departamentos]");

            return conn.execQuery(query);
        }

        public bool DeleteDept(DepartamentoBLL oDepartment)
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM [dbEmpDep1].[dbo].[Departamentos] WHERE pkDepID = @deptoID");
            cmd.Parameters.Add("@deptoID", SqlDbType.Int).Value = oDepartment.ID;
            return !String.IsNullOrEmpty(oDepartment.ID.ToString())
                ? conn.execNoDataRetCMD(cmd)
                : false;
        }

        public bool UpdateDept(DepartamentoBLL oDepartment)
        {
            SqlCommand cmd = new SqlCommand("UPDATE [dbEmpDep1].[dbo].[Departamentos] SET departamento = @depto WHERE pkDepID = @deptoID");
            cmd.Parameters.Add("@deptoID", SqlDbType.Int).Value = oDepartment.ID;
            cmd.Parameters.Add("@depto", SqlDbType.VarChar).Value = oDepartment.Departamento;
            return !String.IsNullOrEmpty(oDepartment.ID.ToString())
                ? conn.execNoDataRetCMD(cmd)
                : false;
        }
    }
}
