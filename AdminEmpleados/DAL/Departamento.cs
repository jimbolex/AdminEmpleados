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
            return conn.execNoDataRetCMD($"INSERT INTO [dbEmpDep1].[dbo].[Departamentos] (departamento) VALUES ('{oDepartment.Departamento}')");
                
        }

        public DataSet getAllDepartments()
        {
            string query = "SELECT pkDepID as [ID], departamento as [Departamento] FROM [dbEmpDep1].[dbo].[Departamentos]";

            return conn.execQuery(query);
        }

        public bool DeleteDept(DepartamentoBLL oDepartment)
        {
            return !String.IsNullOrEmpty(oDepartment.ID.ToString())
                ? conn.execNoDataRetCMD($"DELETE FROM [dbEmpDep1].[dbo].[Departamentos] WHERE pkDepID = {oDepartment.ID}")
                : false;
        }

        public bool UpdateDept(DepartamentoBLL oDepartment)
        {
            return !String.IsNullOrEmpty(oDepartment.ID.ToString())
                ? conn.execNoDataRetCMD($"UPDATE [dbEmpDep1].[dbo].[Departamentos] SET departamento = '{oDepartment.Departamento}' WHERE pkDepID = {oDepartment.ID}")
                : false;
        }
    }
}
