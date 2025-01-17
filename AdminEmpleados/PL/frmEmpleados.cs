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
            clearForm();

        }

        private void frmEmpleados_Load(object sender, EventArgs e)
        {
            Departamento depto = new Departamento();

            cmbDepto.DataSource = depto.getUniqueDepartments().Tables[0];
            cmbDepto.DisplayMember = "Departamento";
            cmbDepto.ValueMember = "ID";

        }
        private void clearForm(bool clear = true)
        {
            if (clear)
            {
                txtID.Clear();
                txtNombres.Clear();
                txtPrimerApellido.Clear();
                txtSegundoApellido.Clear();
                txtCorreo.Clear();
            }


            btnAgregar.Enabled = clear ? true : false;
            btnModificar.Enabled = clear ? false : true;
            btnEliminar.Enabled = clear ? false : true;
            btnCancelar.Enabled = true;
        }

        private void btnExaminar_Click(object sender, EventArgs e)
        {
            OpenFileDialog selectPic = new OpenFileDialog();
            selectPic.Title = "Employee Profile Picture";

            if (selectPic.ShowDialog() == DialogResult.OK)
            {
                pbEmpFoto.Image = Image.FromStream(selectPic.OpenFile());

                //prepare the image to be sent as binary to the DB
                MemoryStream memory = new MemoryStream();
                pbEmpFoto.Image.Save(memory, ImageFormat.Png);

                imageByte = memory.ToArray();
            }
        }

        private EmpleadoBLL RecoverInfo()
        {
            EmpleadoBLL Empleado = new EmpleadoBLL();
            int ID = 0; int.TryParse(txtID.Text, out ID);
            int DeptoID = 0; int.TryParse(cmbDepto.ValueMember, out ID);
            Empleado.ID = ID;
            Empleado.NombreEmpleado = txtNombres.Text;
            Empleado.PrimerApellido = txtPrimerApellido.Text;
            Empleado.SegundoApellido = String.IsNullOrEmpty(txtSegundoApellido.Text) ? " " : txtSegundoApellido.Text;
            Empleado.Correo = txtCorreo.Text;
            Empleado.Departamento = DeptoID;
            Empleado.FotoEmpleado = imageByte;

            return Empleado;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (emp.InsertEmpl(RecoverInfo()))
            {
                MessageBox.Show($"Employee: {RecoverInfo().NombreEmpleado} {RecoverInfo().PrimerApellido} added successfully.");
                clearForm();
               
            }
            else
            {
                MessageBox.Show($"Something went wrong. Contact Support.");
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            clearForm();
        }
    }
}
