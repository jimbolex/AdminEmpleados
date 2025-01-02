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
            dgvDeptos.DataSource = dept.getAllDepartments().Tables[0];
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (dept.InsertDept(RecoverInfo()))
            {
                MessageBox.Show($"Record '{RecoverInfo().Departamento}' added successfully");
                txtDepto.Clear();
                txtDeptoID.Clear();
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

        private void selectRow(object sender, DataGridViewCellMouseEventArgs e)
        {
            int i = e.RowIndex;
            txtDeptoID.Text = dgvDeptos.Rows[i].Cells[0].Value.ToString();
            txtDepto.Text = dgvDeptos.Rows[i].Cells[1].Value.ToString();
        }
    }
}
