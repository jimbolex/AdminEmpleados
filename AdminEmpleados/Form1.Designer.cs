namespace AdminEmpleados
{
    partial class frmEmpAdminMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnDeptos = new System.Windows.Forms.Button();
            this.btnEmpleados = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnDeptos
            // 
            this.btnDeptos.Location = new System.Drawing.Point(61, 45);
            this.btnDeptos.Name = "btnDeptos";
            this.btnDeptos.Size = new System.Drawing.Size(152, 153);
            this.btnDeptos.TabIndex = 0;
            this.btnDeptos.Text = "Departamentos";
            this.btnDeptos.UseVisualStyleBackColor = true;
            this.btnDeptos.Click += new System.EventHandler(this.btnDeptos_Click);
            // 
            // btnEmpleados
            // 
            this.btnEmpleados.Location = new System.Drawing.Point(269, 45);
            this.btnEmpleados.Name = "btnEmpleados";
            this.btnEmpleados.Size = new System.Drawing.Size(152, 153);
            this.btnEmpleados.TabIndex = 1;
            this.btnEmpleados.Text = "Empleados";
            this.btnEmpleados.UseVisualStyleBackColor = true;
            this.btnEmpleados.Click += new System.EventHandler(this.btnEmpleados_Click);
            // 
            // frmEmpAdminMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 244);
            this.Controls.Add(this.btnEmpleados);
            this.Controls.Add(this.btnDeptos);
            this.Name = "frmEmpAdminMain";
            this.Text = "Administrador de Empleados";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnDeptos;
        private System.Windows.Forms.Button btnEmpleados;
    }
}

