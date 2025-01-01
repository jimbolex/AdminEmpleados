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
        Departamento dept;
        public frmDepartamentos()
        {
            InitializeComponent();
            dept = new Departamento();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (dept.InsertDept(RecoverInfo()))
            {
                MessageBox.Show($"Record '{RecoverInfo().Departamento}' added successfully");
                txtDepto.Clear();
            }
            else
            {
                MessageBox.Show($"Something went wrong. Contact Support.");
            }
            
        }

        private DepartamentoBLL RecoverInfo()
        {
            DepartamentoBLL Departamento = new DepartamentoBLL();
            int ID = 0; int.TryParse(txtDeptoID.Text, out ID);
            Departamento.ID = ID;
            Departamento.Departamento = txtDepto.Text;

            return Departamento;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
