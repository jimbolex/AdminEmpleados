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
            
            Conexion conexion = new Conexion();
            if (conexion.InsertDept(RecuperarInformacion())) MessageBox.Show($"Record '{RecuperarInformacion()}' added successfully"); else MessageBox.Show($"Something went wrong. Contact Support.");

        }

        private string RecuperarInformacion()
        {
            DepartamentoBLL Departamento = new DepartamentoBLL();
            //int ID = 0; int.TryParse(txtDeptoID.Text, out ID); <= delete later
            //Departamento.ID = ID;
            Departamento.Departamento = txtDepto.Text;

            return Departamento.Departamento;


        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
