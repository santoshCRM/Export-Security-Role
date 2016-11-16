namespace MsCrmTools.Soaplogger
{
    partial class SoapLogger
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label6 = new System.Windows.Forms.Label();
            this.txt_recordid = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_logicalname = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lbl_copystroutput = new System.Windows.Forms.Label();
            this.txt_stringoutput = new System.Windows.Forms.TextBox();
            this.lbl_copyoutput = new System.Windows.Forms.Label();
            this.txt_output = new System.Windows.Forms.TextBox();
            this.txt_request = new System.Windows.Forms.TextBox();
            this.Execute = new System.Windows.Forms.Button();
            this.grd_parameter = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_convert = new System.Windows.Forms.Button();
            this.lbl_parameter = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grd_parameter)).BeginInit();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(35, 140);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Record Guid";
            // 
            // txt_recordid
            // 
            this.txt_recordid.Location = new System.Drawing.Point(160, 134);
            this.txt_recordid.Multiline = true;
            this.txt_recordid.Name = "txt_recordid";
            this.txt_recordid.Size = new System.Drawing.Size(152, 27);
            this.txt_recordid.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(35, 93);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Target Logical Name";
            // 
            // txt_logicalname
            // 
            this.txt_logicalname.Location = new System.Drawing.Point(160, 90);
            this.txt_logicalname.Multiline = true;
            this.txt_logicalname.Name = "txt_logicalname";
            this.txt_logicalname.Size = new System.Drawing.Size(152, 27);
            this.txt_logicalname.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(35, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Request Name";
            // 
            // lbl_copystroutput
            // 
            this.lbl_copystroutput.AutoSize = true;
            this.lbl_copystroutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_copystroutput.Location = new System.Drawing.Point(623, 13);
            this.lbl_copystroutput.Name = "lbl_copystroutput";
            this.lbl_copystroutput.Size = new System.Drawing.Size(116, 22);
            this.lbl_copystroutput.TabIndex = 8;
            this.lbl_copystroutput.Text = "String Output";
            this.lbl_copystroutput.Click += new System.EventHandler(this.lbl_copystroutput_Click);
            // 
            // txt_stringoutput
            // 
            this.txt_stringoutput.Location = new System.Drawing.Point(626, 46);
            this.txt_stringoutput.Multiline = true;
            this.txt_stringoutput.Name = "txt_stringoutput";
            this.txt_stringoutput.Size = new System.Drawing.Size(302, 325);
            this.txt_stringoutput.TabIndex = 9;
            // 
            // lbl_copyoutput
            // 
            this.lbl_copyoutput.AutoSize = true;
            this.lbl_copyoutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_copyoutput.Location = new System.Drawing.Point(315, 13);
            this.lbl_copyoutput.Name = "lbl_copyoutput";
            this.lbl_copyoutput.Size = new System.Drawing.Size(111, 22);
            this.lbl_copyoutput.TabIndex = 6;
            this.lbl_copyoutput.Text = "Copy Output";
            this.lbl_copyoutput.Click += new System.EventHandler(this.lbl_copyoutput_Click);
            // 
            // txt_output
            // 
            this.txt_output.Location = new System.Drawing.Point(318, 46);
            this.txt_output.Multiline = true;
            this.txt_output.Name = "txt_output";
            this.txt_output.Size = new System.Drawing.Size(302, 325);
            this.txt_output.TabIndex = 10;
            // 
            // txt_request
            // 
            this.txt_request.Location = new System.Drawing.Point(160, 46);
            this.txt_request.Multiline = true;
            this.txt_request.Name = "txt_request";
            this.txt_request.Size = new System.Drawing.Size(152, 27);
            this.txt_request.TabIndex = 1;
            // 
            // Execute
            // 
            this.Execute.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Execute.Location = new System.Drawing.Point(230, 377);
            this.Execute.Name = "Execute";
            this.Execute.Size = new System.Drawing.Size(124, 39);
            this.Execute.TabIndex = 5;
            this.Execute.Text = "Execute";
            this.Execute.UseVisualStyleBackColor = false;
            this.Execute.Click += new System.EventHandler(this.Execute_Click);
            // 
            // grd_parameter
            // 
            this.grd_parameter.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grd_parameter.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.grd_parameter.Location = new System.Drawing.Point(38, 188);
            this.grd_parameter.Name = "grd_parameter";
            this.grd_parameter.Size = new System.Drawing.Size(274, 183);
            this.grd_parameter.TabIndex = 4;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Name";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Value";
            this.Column2.Name = "Column2";
            // 
            // btn_convert
            // 
            this.btn_convert.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btn_convert.Location = new System.Drawing.Point(551, 377);
            this.btn_convert.Name = "btn_convert";
            this.btn_convert.Size = new System.Drawing.Size(124, 39);
            this.btn_convert.TabIndex = 7;
            this.btn_convert.Text = "Convert";
            this.btn_convert.UseVisualStyleBackColor = false;
            this.btn_convert.Click += new System.EventHandler(this.btn_convert_Click);
            // 
            // lbl_parameter
            // 
            this.lbl_parameter.AutoSize = true;
            this.lbl_parameter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_parameter.Location = new System.Drawing.Point(38, 169);
            this.lbl_parameter.Name = "lbl_parameter";
            this.lbl_parameter.Size = new System.Drawing.Size(96, 13);
            this.lbl_parameter.TabIndex = 15;
            this.lbl_parameter.Text = "Add Parameters";
            // 
            // SoapLogger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbl_parameter);
            this.Controls.Add(this.btn_convert);
            this.Controls.Add(this.grd_parameter);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txt_recordid);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txt_logicalname);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lbl_copystroutput);
            this.Controls.Add(this.txt_stringoutput);
            this.Controls.Add(this.lbl_copyoutput);
            this.Controls.Add(this.txt_output);
            this.Controls.Add(this.txt_request);
            this.Controls.Add(this.Execute);
            this.Name = "SoapLogger";
            this.Size = new System.Drawing.Size(957, 421);
            ((System.ComponentModel.ISupportInitialize)(this.grd_parameter)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Execute;
        private System.Windows.Forms.TextBox txt_request;
        private System.Windows.Forms.TextBox txt_output;
        private System.Windows.Forms.Label lbl_copyoutput;
        private System.Windows.Forms.TextBox txt_stringoutput;
        private System.Windows.Forms.Label lbl_copystroutput;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_logicalname;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_recordid;
        private System.Windows.Forms.DataGridView grd_parameter;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.Button btn_convert;
        private System.Windows.Forms.Label lbl_parameter;
    }
}
