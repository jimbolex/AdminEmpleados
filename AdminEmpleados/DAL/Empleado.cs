using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminEmpleados.BLL;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

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

            if (conn.execNonQuery(cmd))
            {
                int empID = this.getMaxEmployee().Tables[0].Rows[0].Field<int>(0); //obtain an specific field from a dataset.
                return InsertEmplDept(empID, Empl.Departamento);
            }
            else
            {
                return false;
            }
        }

        //version with a many-to-many relationship
        private bool InsertEmplDept(int emplID, int depID)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO [dbEmpDep1].[dbo].[DepartamentoEmpleado] VALUES (@empID, @depID)");
            cmd.Parameters.Add("@empID", SqlDbType.Int).Value = emplID;
            cmd.Parameters.Add("@depID", SqlDbType.Int).Value = depID;
            return conn.execNonQuery(cmd);
        }

        private DataSet getMaxEmployee()
        {
            SqlCommand query = new SqlCommand("SELECT MAX(pkEmpId) FROM [dbEmpDep1].[dbo].[Empleados]");

            return conn.execQuery(query);
        }

        public DataSet getAllEmployees()
        {
            SqlCommand query = new SqlCommand("SELECT [pkEmpId] as ID, [nombres] as [Nombre(s)], [apellido1] as [Primer Apellido],[apellido2] as [Segundo Apellido], [correo] as Email FROM [dbEmpDep1].[dbo].[Empleados]");

            return conn.execQuery(query);
        }

        public DataSet getEmployeesPic(int emplID)
        {
            SqlCommand query = new SqlCommand("SELECT foto FROM [dbEmpDep1].[dbo].[Empleados] WHERE pkEmpId = @empID");
            query.Parameters.Add("@empID", SqlDbType.Int).Value=emplID;

            return conn.execQuery(query);
        }

        public DataSet getEmployeesDept(int emplID)
        {
            /*SqlCommand query = new SqlCommand("SELECT A.departamento FROM [dbEmpDep1].[dbo].[Departamentos] A with (nolock) " +
                "JOIN [dbEmpDep1].[dbo].[DepartamentoEmpleado] B with (nolock) " +
                "ON A.pkDepID = B.fkDeptId " +
                "WHERE B.fkEmpId = @empID");*/
            SqlCommand query = new SqlCommand("BEGIN TRANSACTION; " +
                "SELECT A.departamento FROM [dbEmpDep1].[dbo].[Departamentos] A with (nolock) " +
                "JOIN [dbEmpDep1].[dbo].[DepartamentoEmpleado] B with (nolock) " +
                "ON A.pkDepID = B.fkDeptId " +
                "WHERE B.fkEmpId = @empID " +
                "IF @@ROWCOUNT = 0 " +
                "BEGIN " +
                "SELECT '----------------------------------------------------------- Seleccione un Departamento -----------------------------------------------------------'" +
                "END " +
                "COMMIT TRANSACTION;");

            query.Parameters.Add("@empID", SqlDbType.Int).Value = emplID;

            return conn.execQuery(query);
        }

        public bool DeleteEmp(EmpleadoBLL Empl)
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM [dbEmpDep1].[dbo].[Empleados] WHERE pkEmpId = @EmpID");
            cmd.Parameters.Add("@EmpID", SqlDbType.Int).Value = Empl.ID;
            return !String.IsNullOrEmpty(Empl.ID.ToString())
                ? conn.execNonQuery(cmd)
                : false;
        }

        public bool UpdateEmp(EmpleadoBLL Empl)
        {
            SqlCommand cmd = new SqlCommand("UPDATE [dbEmpDep1].[dbo].[Empleados] " +
                "SET nombres = @nombres, apellido1 = @apellido1, apellido2 = @apellido2, correo = @correo " +
                "WHERE pkEmpId = @EmpID");
            cmd.Parameters.Add("@EmpID", SqlDbType.Int).Value = Empl.ID;
            cmd.Parameters.Add("@nombres", SqlDbType.VarChar).Value = Empl.NombreEmpleado;
            cmd.Parameters.Add("@apellido1", SqlDbType.VarChar).Value = Empl.PrimerApellido;
            cmd.Parameters.Add("@apellido2", SqlDbType.VarChar).Value = Empl.SegundoApellido;
            cmd.Parameters.Add("@correo", SqlDbType.VarChar).Value = Empl.Correo;

            if (!String.IsNullOrEmpty(Empl.ID.ToString()) && UpdateEmpDept(Empl.ID, Empl.Departamento))
            {
                return conn.execNonQuery(cmd);
            }
            else
            {
                return false;
            }
            
               
        }

        public bool UpdateEmpDept(int empId, int depId)
        {
            SqlCommand cmd = new SqlCommand("BEGIN TRANSACTION; " +
                "UPDATE [dbEmpDep1].[dbo].[DepartamentoEmpleado] WITH (UPDLOCK, SERIALIZABLE) " +
                "SET fkDeptId = @DepID " +
                "WHERE fkEmpId = @EmpID; " +
                "IF @@ROWCOUNT = 0 " +
                "BEGIN " +
                "INSERT into [dbEmpDep1].[dbo].[DepartamentoEmpleado] " +
                "VALUES (@EmpID, @DepID) " +
                "END " +
                "COMMIT TRANSACTION;");
            cmd.Parameters.Add("@EmpID", SqlDbType.Int).Value = empId;
            cmd.Parameters.Add("@DepID", SqlDbType.Int).Value = depId;

            return conn.execNonQuery(cmd);

        }
    }
}
