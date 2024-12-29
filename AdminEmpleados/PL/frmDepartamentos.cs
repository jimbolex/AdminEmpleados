using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AdminEmpleados.BLL;
using AdminEmpleados.DAL;

namespace AdminEmpleados.PL
{
    public partial class frmDepartamentos : Form
    {
        public frmDepartamentos()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            //RecuperarInformacion();
            Conexion conexion = new Conexion();
            MessageBox.Show($"Connected?... {conexion.ConexionTest()}");

        }

        private void RecuperarInformacion()
        {
            DepartamentoBLL Departamento = new DepartamentoBLL();
            int ID = 0; int.TryParse(txtDeptoID.Text, out ID);
            Departamento.ID = ID;
            Departamento.Departamento = txtDepto.Text;

            MessageBox.Show($"Depto.ID: {Departamento.ID}; Nombre Depto: {Departamento.Departamento}"); // <== delete later


        }
    }
}
