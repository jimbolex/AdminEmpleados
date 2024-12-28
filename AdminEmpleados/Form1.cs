using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AdminEmpleados.PL;

namespace AdminEmpleados
{
    public partial class frmEmpAdminMain : Form
    {
        public frmEmpAdminMain()
        {
            InitializeComponent();
        }

        private void btnDeptos_Click(object sender, EventArgs e)
        {
            frmDepartamentos Deptos = new frmDepartamentos();
            Deptos.Show();
        }

        private void btnEmpleados_Click(object sender, EventArgs e)
        {
            frmEmpleados Empleados = new frmEmpleados();
            Empleados.Show();
        }
    }
}
