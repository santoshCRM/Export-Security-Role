namespace RoleDocumenter
{
    partial class RoleDocumenterCtl
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStripMenu = new System.Windows.Forms.ToolStrip();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.tssSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRetrieveRoles = new System.Windows.Forms.ToolStripButton();
            this.btnExport = new System.Windows.Forms.ToolStripButton();
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.drp_roles = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.splitHeaderGrids = new System.Windows.Forms.SplitContainer();
            this.label7 = new System.Windows.Forms.Label();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.splitSearchGrid = new System.Windows.Forms.SplitContainer();
            this.splitSearch = new System.Windows.Forms.SplitContainer();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.splitGrids = new System.Windows.Forms.SplitContainer();
            this.grpTable = new System.Windows.Forms.GroupBox();
            this.grdview_role = new System.Windows.Forms.DataGridView();
            this.grpMisc = new System.Windows.Forms.GroupBox();
            this.grdview_misRole = new System.Windows.Forms.DataGridView();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.toolStripMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitHeaderGrids)).BeginInit();
            this.splitHeaderGrids.Panel1.SuspendLayout();
            this.splitHeaderGrids.Panel2.SuspendLayout();
            this.splitHeaderGrids.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitSearchGrid)).BeginInit();
            this.splitSearchGrid.Panel1.SuspendLayout();
            this.splitSearchGrid.Panel2.SuspendLayout();
            this.splitSearchGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitSearch)).BeginInit();
            this.splitSearch.Panel1.SuspendLayout();
            this.splitSearch.Panel2.SuspendLayout();
            this.splitSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitGrids)).BeginInit();
            this.splitGrids.Panel1.SuspendLayout();
            this.splitGrids.Panel2.SuspendLayout();
            this.splitGrids.SuspendLayout();
            this.grpTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdview_role)).BeginInit();
            this.grpMisc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdview_misRole)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripMenu
            // 
            this.toolStripMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbClose,
            this.tssSeparator1,
            this.btnRetrieveRoles,
            this.btnExport});
            this.toolStripMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripMenu.Name = "toolStripMenu";
            this.toolStripMenu.Size = new System.Drawing.Size(559, 31);
            this.toolStripMenu.TabIndex = 4;
            this.toolStripMenu.Text = "toolStrip1";
            // 
            // tsbClose
            // 
            this.tsbClose.Image = global::RoleDocumenter.Properties.Resources.tsbClose_Image;
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new System.Drawing.Size(64, 28);
            this.tsbClose.Text = "Close";
            this.tsbClose.Click += new System.EventHandler(this.tsbClose_Click);
            // 
            // tssSeparator1
            // 
            this.tssSeparator1.Name = "tssSeparator1";
            this.tssSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // btnRetrieveRoles
            // 
            this.btnRetrieveRoles.Image = global::RoleDocumenter.Properties.Resources.iconfinder_Revert_131718;
            this.btnRetrieveRoles.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRetrieveRoles.Name = "btnRetrieveRoles";
            this.btnRetrieveRoles.Size = new System.Drawing.Size(108, 28);
            this.btnRetrieveRoles.Text = "Retrieve Roles";
            this.btnRetrieveRoles.Click += new System.EventHandler(this.btnRetrieveRoles_Click);
            // 
            // btnExport
            // 
            this.btnExport.Image = global::RoleDocumenter.Properties.Resources.export;
            this.btnExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(69, 28);
            this.btnExport.Text = "Export";
            this.btnExport.ToolTipText = "Export to Excel";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // splitMain
            // 
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitMain.IsSplitterFixed = true;
            this.splitMain.Location = new System.Drawing.Point(0, 31);
            this.splitMain.Name = "splitMain";
            this.splitMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.drp_roles);
            this.splitMain.Panel1.Controls.Add(this.label1);
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.splitHeaderGrids);
            this.splitMain.Size = new System.Drawing.Size(559, 628);
            this.splitMain.SplitterDistance = 25;
            this.splitMain.TabIndex = 5;
            // 
            // drp_roles
            // 
            this.drp_roles.FormattingEnabled = true;
            this.drp_roles.Location = new System.Drawing.Point(53, 3);
            this.drp_roles.Name = "drp_roles";
            this.drp_roles.Size = new System.Drawing.Size(277, 21);
            this.drp_roles.TabIndex = 3;
            this.drp_roles.SelectedIndexChanged += new System.EventHandler(this.drp_roles_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Roles";
            // 
            // splitHeaderGrids
            // 
            this.splitHeaderGrids.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitHeaderGrids.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitHeaderGrids.Location = new System.Drawing.Point(0, 0);
            this.splitHeaderGrids.Name = "splitHeaderGrids";
            this.splitHeaderGrids.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitHeaderGrids.Panel1
            // 
            this.splitHeaderGrids.Panel1.Controls.Add(this.label7);
            this.splitHeaderGrids.Panel1.Controls.Add(this.pictureBox6);
            this.splitHeaderGrids.Panel1.Controls.Add(this.pictureBox5);
            this.splitHeaderGrids.Panel1.Controls.Add(this.label5);
            this.splitHeaderGrids.Panel1.Controls.Add(this.label4);
            this.splitHeaderGrids.Panel1.Controls.Add(this.pictureBox3);
            this.splitHeaderGrids.Panel1.Controls.Add(this.label3);
            this.splitHeaderGrids.Panel1.Controls.Add(this.pictureBox2);
            this.splitHeaderGrids.Panel1.Controls.Add(this.label2);
            this.splitHeaderGrids.Panel1.Controls.Add(this.pictureBox1);
            // 
            // splitHeaderGrids.Panel2
            // 
            this.splitHeaderGrids.Panel2.Controls.Add(this.splitSearchGrid);
            this.splitHeaderGrids.Size = new System.Drawing.Size(559, 599);
            this.splitHeaderGrids.SplitterDistance = 25;
            this.splitHeaderGrids.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(179, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 13);
            this.label7.TabIndex = 21;
            this.label7.Text = "Business Unit";
            // 
            // pictureBox6
            // 
            this.pictureBox6.Image = global::RoleDocumenter.Properties.Resources.BU;
            this.pictureBox6.Location = new System.Drawing.Point(153, 4);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(18, 17);
            this.pictureBox6.TabIndex = 20;
            this.pictureBox6.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = global::RoleDocumenter.Properties.Resources.organization;
            this.pictureBox5.Location = new System.Drawing.Point(433, 3);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(18, 17);
            this.pictureBox5.TabIndex = 19;
            this.pictureBox5.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(461, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Organization";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(293, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Parent: Child Business Unit";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::RoleDocumenter.Properties.Resources.P_BU;
            this.pictureBox3.Location = new System.Drawing.Point(266, 4);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(17, 17);
            this.pictureBox3.TabIndex = 16;
            this.pictureBox3.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(115, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "User";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::RoleDocumenter.Properties.Resources.user;
            this.pictureBox2.Location = new System.Drawing.Point(90, 4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(17, 17);
            this.pictureBox2.TabIndex = 14;
            this.pictureBox2.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(57, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "None";
            // 
            // pictureBox1
            // 
            this.pictureBox1.ErrorImage = global::RoleDocumenter.Properties.Resources.none;
            this.pictureBox1.Image = global::RoleDocumenter.Properties.Resources.none;
            this.pictureBox1.Location = new System.Drawing.Point(32, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(17, 17);
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // splitSearchGrid
            // 
            this.splitSearchGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitSearchGrid.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitSearchGrid.IsSplitterFixed = true;
            this.splitSearchGrid.Location = new System.Drawing.Point(0, 0);
            this.splitSearchGrid.Name = "splitSearchGrid";
            this.splitSearchGrid.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitSearchGrid.Panel1
            // 
            this.splitSearchGrid.Panel1.Controls.Add(this.splitSearch);
            // 
            // splitSearchGrid.Panel2
            // 
            this.splitSearchGrid.Panel2.Controls.Add(this.splitGrids);
            this.splitSearchGrid.Size = new System.Drawing.Size(559, 570);
            this.splitSearchGrid.SplitterDistance = 25;
            this.splitSearchGrid.TabIndex = 1;
            // 
            // splitSearch
            // 
            this.splitSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitSearch.Location = new System.Drawing.Point(0, 0);
            this.splitSearch.Name = "splitSearch";
            // 
            // splitSearch.Panel1
            // 
            this.splitSearch.Panel1.Controls.Add(this.lblSearch);
            // 
            // splitSearch.Panel2
            // 
            this.splitSearch.Panel2.Controls.Add(this.txtSearch);
            this.splitSearch.Size = new System.Drawing.Size(559, 25);
            this.splitSearch.SplitterDistance = 45;
            this.splitSearch.TabIndex = 2;
            // 
            // lblSearch
            // 
            this.lblSearch.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(3, 7);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(41, 13);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "Search";
            // 
            // txtSearch
            // 
            this.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSearch.Location = new System.Drawing.Point(0, 0);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(510, 20);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // splitGrids
            // 
            this.splitGrids.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitGrids.Location = new System.Drawing.Point(0, 0);
            this.splitGrids.Name = "splitGrids";
            // 
            // splitGrids.Panel1
            // 
            this.splitGrids.Panel1.Controls.Add(this.grpTable);
            // 
            // splitGrids.Panel2
            // 
            this.splitGrids.Panel2.Controls.Add(this.grpMisc);
            this.splitGrids.Size = new System.Drawing.Size(559, 541);
            this.splitGrids.SplitterDistance = 342;
            this.splitGrids.TabIndex = 0;
            // 
            // grpTable
            // 
            this.grpTable.Controls.Add(this.grdview_role);
            this.grpTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpTable.Location = new System.Drawing.Point(0, 0);
            this.grpTable.Name = "grpTable";
            this.grpTable.Size = new System.Drawing.Size(342, 541);
            this.grpTable.TabIndex = 4;
            this.grpTable.TabStop = false;
            this.grpTable.Text = "Table Privileges";
            // 
            // grdview_role
            // 
            this.grdview_role.AllowUserToAddRows = false;
            this.grdview_role.AllowUserToDeleteRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.grdview_role.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.grdview_role.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdview_role.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdview_role.Location = new System.Drawing.Point(3, 16);
            this.grdview_role.Name = "grdview_role";
            this.grdview_role.ReadOnly = true;
            this.grdview_role.RowHeadersVisible = false;
            this.grdview_role.RowHeadersWidth = 62;
            this.grdview_role.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdview_role.ShowEditingIcon = false;
            this.grdview_role.Size = new System.Drawing.Size(336, 522);
            this.grdview_role.TabIndex = 3;
            this.grdview_role.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grdview_role_ColumnHeaderMouseClick);
            // 
            // grpMisc
            // 
            this.grpMisc.Controls.Add(this.grdview_misRole);
            this.grpMisc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpMisc.Location = new System.Drawing.Point(0, 0);
            this.grpMisc.Name = "grpMisc";
            this.grpMisc.Size = new System.Drawing.Size(213, 541);
            this.grpMisc.TabIndex = 5;
            this.grpMisc.TabStop = false;
            this.grpMisc.Text = "Miscellaneous Privileges";
            // 
            // grdview_misRole
            // 
            this.grdview_misRole.AllowUserToAddRows = false;
            this.grdview_misRole.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.grdview_misRole.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.grdview_misRole.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdview_misRole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdview_misRole.Location = new System.Drawing.Point(3, 16);
            this.grdview_misRole.Name = "grdview_misRole";
            this.grdview_misRole.ReadOnly = true;
            this.grdview_misRole.RowHeadersVisible = false;
            this.grdview_misRole.RowHeadersWidth = 62;
            this.grdview_misRole.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdview_misRole.Size = new System.Drawing.Size(207, 522);
            this.grdview_misRole.TabIndex = 4;
            // 
            // RoleDocumenterCtl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitMain);
            this.Controls.Add(this.toolStripMenu);
            this.Name = "RoleDocumenterCtl";
            this.Size = new System.Drawing.Size(559, 659);
            this.Load += new System.EventHandler(this.RoleDocumenterCtl_Load);
            this.toolStripMenu.ResumeLayout(false);
            this.toolStripMenu.PerformLayout();
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel1.PerformLayout();
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.splitHeaderGrids.Panel1.ResumeLayout(false);
            this.splitHeaderGrids.Panel1.PerformLayout();
            this.splitHeaderGrids.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitHeaderGrids)).EndInit();
            this.splitHeaderGrids.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.splitSearchGrid.Panel1.ResumeLayout(false);
            this.splitSearchGrid.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitSearchGrid)).EndInit();
            this.splitSearchGrid.ResumeLayout(false);
            this.splitSearch.Panel1.ResumeLayout(false);
            this.splitSearch.Panel1.PerformLayout();
            this.splitSearch.Panel2.ResumeLayout(false);
            this.splitSearch.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitSearch)).EndInit();
            this.splitSearch.ResumeLayout(false);
            this.splitGrids.Panel1.ResumeLayout(false);
            this.splitGrids.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitGrids)).EndInit();
            this.splitGrids.ResumeLayout(false);
            this.grpTable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdview_role)).EndInit();
            this.grpMisc.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdview_misRole)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStripMenu;
        private System.Windows.Forms.ToolStripButton tsbClose;
        private System.Windows.Forms.ToolStripSeparator tssSeparator1;
        private System.Windows.Forms.ToolStripButton btnRetrieveRoles;
        private System.Windows.Forms.ToolStripButton btnExport;
        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.ComboBox drp_roles;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitGrids;
        private System.Windows.Forms.DataGridView grdview_role;
        private System.Windows.Forms.DataGridView grdview_misRole;
        private System.Windows.Forms.SplitContainer splitHeaderGrids;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox grpTable;
        private System.Windows.Forms.GroupBox grpMisc;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.SplitContainer splitSearchGrid;
        private System.Windows.Forms.SplitContainer splitSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
    }
}
