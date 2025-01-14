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
using System.IO;
using System.Drawing.Imaging;
using AdminEmpleados.BLL;

namespace AdminEmpleados.PL
{
    public partial class frmEmpleados : Form
    {
        byte[] imageByte;
        Empleado emp;
        public frmEmpleados()
        {
            InitializeComponent();
            emp = new Empleado();

        }

        private void frmEmpleados_Load(object sender, EventArgs e)
        {
            Departamento depto = new Departamento();

            cmbDepto.DataSource = depto.getUniqueDepartments().Tables[0];
            cmbDepto.DisplayMember = "Departamento";
            cmbDepto.ValueMember = "ID";

        }

        private void btnExaminar_Click(object sender, EventArgs e)
        {
            OpenFileDialog selectPic = new OpenFileDialog();
            selectPic.Title = "Employee Profile Picture";

            if (selectPic.ShowDialog() == DialogResult.OK)
            {
                pbEmpFoto.Image = Image.FromStream(selectPic.OpenFile());

                //to send the image as binary to the DB
                MemoryStream memory = new MemoryStream();
                pbEmpFoto.Image.Save(memory, ImageFormat.Png);

                imageByte = memory.ToArray();
            }
        }

        private EmpleadoBLL RecoverInfo()
        {
            EmpleadoBLL Empleado = new EmpleadoBLL();
            int ID = 0; int.TryParse(txtID.Text, out ID);
            int DeptoID = 0;int.TryParse(cmbDepto.ValueMember, out ID);
            Empleado.ID = ID;
            Empleado.NombreEmpleado = txtNombres.Text;
            Empleado.PrimerApellido = txtPrimerApellido.Text;
            Empleado.SegundoApellido = txtSegundoApellido.Text;
            Empleado.Correo = txtCorreo.Text;
            Empleado.Departamento = DeptoID;
            Empleado.FotoEmpleado = imageByte;

            return Empleado;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            emp.InsertEmpl(RecoverInfo());
        }
    }
}
