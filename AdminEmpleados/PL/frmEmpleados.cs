using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AdminEmpleados.DAL;

namespace AdminEmpleados.PL
{
    public partial class frmEmpleados : Form
    {
        public frmEmpleados()
        {
            InitializeComponent();
        }

        private void frmEmpleados_Load(object sender, EventArgs e)
        {
            Departamento depto = new Departamento();

            cmbDepto.DataSource = depto.getUniqueDepartments().Tables[0];
            cmbDepto.DisplayMember = "Departamento";
            cmbDepto.ValueMember = "ID";

        }
    }
}
