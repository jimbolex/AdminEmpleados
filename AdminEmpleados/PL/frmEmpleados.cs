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
            getAllEmployees();

        }

        private void frmEmpleados_Load(object sender, EventArgs e)
        {
            Departamento depto = new Departamento();

            cmbDepto.DataSource = depto.getUniqueDepartments().Tables[0];
            cmbDepto.DisplayMember = "Departamento";
            cmbDepto.ValueMember = "ID";
            cmbDepto.SelectedItem = null;
            cmbDepto.SelectedText = "----------------------------------------------------------- Seleccione un Departamento -----------------------------------------------------------";

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

        private void getAllEmployees()
        {
            dgvEmpleados.DataSource = emp.getAllEmployees().Tables[0];
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
            int DeptoID = 0; int.TryParse(cmbDepto.SelectedValue.ToString(), out DeptoID);
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
                getAllEmployees();
               
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

        private void selectRow(object sender, DataGridViewCellMouseEventArgs e)
        {
            int i = e.RowIndex;
            int empID;
            dgvEmpleados.ClearSelection();                      

            if (i >= 0)
            {
                empID = int.Parse(dgvEmpleados.Rows[i].Cells[0].Value.ToString());
                imageByte = emp.getEmployeesPic(empID).Tables[0].Rows[0].Field<byte[]>(0);
                txtID.Text =empID.ToString();
                txtNombres.Text = dgvEmpleados.Rows[i].Cells[1].Value.ToString();
                txtPrimerApellido.Text = dgvEmpleados.Rows[i].Cells[2].Value.ToString();
                txtSegundoApellido.Text = dgvEmpleados.Rows[i].Cells[3].Value.ToString();
                txtCorreo.Text = dgvEmpleados.Rows[i].Cells[4].Value.ToString();
                pbEmpFoto.Image = EmpImage(imageByte);
                cmbDepto.ResetText();
                try
                {
                    cmbDepto.SelectedText = emp.getEmployeesDept(empID).Tables[0].Rows[0].Field<string>(0);
                    
                }
                catch (Exception)
                {
                    cmbDepto.SelectedText = "----------------------------------------------------------- Seleccione un Departamento -----------------------------------------------------------";
                }
                finally
                {
                    clearForm(false);
                }
                
            }
        }

        private Image EmpImage(byte[] ImageValue)
        {
            using (MemoryStream memory = new MemoryStream(ImageValue))
            {
                Image img = Image.FromStream(memory);
                return img;
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to delete this record?", "Delete Record?", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                if (emp.DeleteEmp(RecoverInfo()))
                {
                    MessageBox.Show($"Record '{RecoverInfo().NombreEmpleado} {RecoverInfo().PrimerApellido}' deleted successfully");
                    getAllEmployees();
                    clearForm();
                }
                else
                {
                    MessageBox.Show($"Something went wrong. Contact Support.");
                }
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (emp.UpdateEmp(RecoverInfo()))
            {
                MessageBox.Show($"Employee ID: '{RecoverInfo().ID}' updated successfully");
                getAllEmployees();
                clearForm();
            }
            else
            {
                MessageBox.Show($"Something went wrong. Contact Support.");
            }
        }
    }
}
