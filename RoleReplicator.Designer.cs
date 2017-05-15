namespace RoleReplicatorControl
{
    partial class RoleReplicator
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
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lstbox_queues = new System.Windows.Forms.ListBox();
            this.chk_queue = new System.Windows.Forms.CheckBox();
            this.chk_team = new System.Windows.Forms.CheckBox();
            this.chk_role = new System.Windows.Forms.CheckBox();
            this.lstview_sRole = new System.Windows.Forms.ListBox();
            this.grdview_user = new System.Windows.Forms.DataGridView();
            this.btn_copyRole = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btn_retUsers = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btn_close = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lstbox_Teams = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txt_filter = new System.Windows.Forms.TextBox();
            this.lb_Filter = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.drp_users = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdview_user)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lstbox_queues);
            this.groupBox4.Location = new System.Drawing.Point(17, 445);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(320, 174);
            this.groupBox4.TabIndex = 24;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Queues";
            // 
            // lstbox_queues
            // 
            this.lstbox_queues.FormattingEnabled = true;
            this.lstbox_queues.Location = new System.Drawing.Point(6, 23);
            this.lstbox_queues.Name = "lstbox_queues";
            this.lstbox_queues.Size = new System.Drawing.Size(308, 147);
            this.lstbox_queues.TabIndex = 0;
            // 
            // chk_queue
            // 
            this.chk_queue.AutoSize = true;
            this.chk_queue.Checked = true;
            this.chk_queue.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_queue.Location = new System.Drawing.Point(536, 64);
            this.chk_queue.Name = "chk_queue";
            this.chk_queue.Size = new System.Drawing.Size(58, 17);
            this.chk_queue.TabIndex = 23;
            this.chk_queue.Text = "Queue";
            this.chk_queue.UseVisualStyleBackColor = true;
            this.chk_queue.Visible = false;
            // 
            // chk_team
            // 
            this.chk_team.AutoSize = true;
            this.chk_team.Checked = true;
            this.chk_team.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_team.Location = new System.Drawing.Point(478, 64);
            this.chk_team.Name = "chk_team";
            this.chk_team.Size = new System.Drawing.Size(53, 17);
            this.chk_team.TabIndex = 22;
            this.chk_team.Text = "Team";
            this.chk_team.UseVisualStyleBackColor = true;
            // 
            // chk_role
            // 
            this.chk_role.AutoSize = true;
            this.chk_role.Checked = true;
            this.chk_role.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_role.Location = new System.Drawing.Point(427, 64);
            this.chk_role.Name = "chk_role";
            this.chk_role.Size = new System.Drawing.Size(48, 17);
            this.chk_role.TabIndex = 16;
            this.chk_role.Text = "Role";
            this.chk_role.UseVisualStyleBackColor = true;
            // 
            // lstview_sRole
            // 
            this.lstview_sRole.FormattingEnabled = true;
            this.lstview_sRole.Location = new System.Drawing.Point(6, 19);
            this.lstview_sRole.Name = "lstview_sRole";
            this.lstview_sRole.Size = new System.Drawing.Size(308, 121);
            this.lstview_sRole.TabIndex = 1;
            // 
            // grdview_user
            // 
            this.grdview_user.AllowUserToAddRows = false;
            this.grdview_user.AllowUserToDeleteRows = false;
            this.grdview_user.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdview_user.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdview_user.Location = new System.Drawing.Point(16, 46);
            this.grdview_user.Name = "grdview_user";
            this.grdview_user.ReadOnly = true;
            this.grdview_user.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdview_user.Size = new System.Drawing.Size(635, 462);
            this.grdview_user.TabIndex = 2;
            this.grdview_user.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grdview_user_CellMouseClick);
            // 
            // btn_copyRole
            // 
            this.btn_copyRole.Image = global::RoleReplicatorControl.Properties.Resources.tsbRun_Image;
            this.btn_copyRole.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_copyRole.Name = "btn_copyRole";
            this.btn_copyRole.Size = new System.Drawing.Size(81, 22);
            this.btn_copyRole.Text = "Copy Role";
            this.btn_copyRole.Click += new System.EventHandler(this.btn_copyRole_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btn_retUsers
            // 
            this.btn_retUsers.Image = global::RoleReplicatorControl.Properties.Resources.tsbConnect_Image;
            this.btn_retUsers.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_retUsers.Name = "btn_retUsers";
            this.btn_retUsers.Size = new System.Drawing.Size(100, 22);
            this.btn_retUsers.Text = "Retrieve Users";
            this.btn_retUsers.Click += new System.EventHandler(this.btn_retUsers_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btn_close
            // 
            this.btn_close.Image = global::RoleReplicatorControl.Properties.Resources.tsbClose_Image;
            this.btn_close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(56, 22);
            this.btn_close.Text = "Close";
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.btn_close,
            this.toolStripSeparator2,
            this.btn_retUsers,
            this.toolStripSeparator3,
            this.btn_copyRole});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1068, 25);
            this.toolStrip1.TabIndex = 21;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lstbox_Teams);
            this.groupBox3.Location = new System.Drawing.Point(17, 234);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(320, 205);
            this.groupBox3.TabIndex = 20;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Teams";
            // 
            // lstbox_Teams
            // 
            this.lstbox_Teams.FormattingEnabled = true;
            this.lstbox_Teams.Location = new System.Drawing.Point(6, 19);
            this.lstbox_Teams.Name = "lstbox_Teams";
            this.lstbox_Teams.Size = new System.Drawing.Size(308, 173);
            this.lstbox_Teams.TabIndex = 8;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lstview_sRole);
            this.groupBox2.Location = new System.Drawing.Point(17, 88);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(320, 144);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Security Role";
            // 
            // txt_filter
            // 
            this.txt_filter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_filter.Location = new System.Drawing.Point(62, 20);
            this.txt_filter.Name = "txt_filter";
            this.txt_filter.Size = new System.Drawing.Size(583, 20);
            this.txt_filter.TabIndex = 1;
            this.txt_filter.TextChanged += new System.EventHandler(this.txt_filter_TextChanged);
            // 
            // lb_Filter
            // 
            this.lb_Filter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_Filter.AutoSize = true;
            this.lb_Filter.Location = new System.Drawing.Point(7, 20);
            this.lb_Filter.Name = "lb_Filter";
            this.lb_Filter.Size = new System.Drawing.Size(29, 13);
            this.lb_Filter.TabIndex = 0;
            this.lb_Filter.Text = "Filter";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.Controls.Add(this.grdview_user);
            this.groupBox1.Controls.Add(this.txt_filter);
            this.groupBox1.Controls.Add(this.lb_Filter);
            this.groupBox1.Location = new System.Drawing.Point(7, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(651, 526);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Available Users";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(664, 531);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Users";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(374, 62);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(672, 557);
            this.tabControl1.TabIndex = 18;
            // 
            // drp_users
            // 
            this.drp_users.FormattingEnabled = true;
            this.drp_users.Location = new System.Drawing.Point(60, 62);
            this.drp_users.Name = "drp_users";
            this.drp_users.Size = new System.Drawing.Size(277, 21);
            this.drp_users.TabIndex = 17;
            this.drp_users.SelectedIndexChanged += new System.EventHandler(this.drp_users_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "User";
            // 
            // RoleReplicator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.chk_queue);
            this.Controls.Add(this.chk_team);
            this.Controls.Add(this.chk_role);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.drp_users);
            this.Controls.Add(this.label1);
            this.Name = "RoleReplicator";
            this.Size = new System.Drawing.Size(1068, 644);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdview_user)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ListBox lstbox_queues;
        private System.Windows.Forms.CheckBox chk_queue;
        private System.Windows.Forms.CheckBox chk_team;
        private System.Windows.Forms.CheckBox chk_role;
        private System.Windows.Forms.ListBox lstview_sRole;
        private System.Windows.Forms.DataGridView grdview_user;
        private System.Windows.Forms.ToolStripButton btn_copyRole;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btn_retUsers;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btn_close;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListBox lstbox_Teams;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txt_filter;
        private System.Windows.Forms.Label lb_Filter;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.ComboBox drp_users;
        private System.Windows.Forms.Label label1;

    }
}
