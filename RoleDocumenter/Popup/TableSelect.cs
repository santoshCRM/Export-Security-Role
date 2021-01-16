using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoleDocumenter
{
    public partial class FrmMultiTable : Form
    {
        public List<EntityMetadata> SelectedEntities = new List<EntityMetadata>();
        public FrmMultiTable(IOrganizationService service)
        {
            InitializeComponent();
            lstTables.Service = service;
        }

        private void FrmMultiTable_Load(object sender, EventArgs e)
        {
            lstTables.SortList(0);
            //lstTables.LoadData();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {

            //DialogResult = DialogResult.OK;

            SelectedEntities = lstTables.CheckedEntities;
            Close();
        }
    }
}
