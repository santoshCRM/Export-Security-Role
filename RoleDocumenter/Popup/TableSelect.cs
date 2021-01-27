using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace RoleDocumenter
{
    public partial class FrmMultiTable : Form
    {
        private class EntityView
        {
            public bool isSelected { get; set; }
            public string DisplayName { get; set; }
            public string LogicalName { get; set; }
        }

        class EVComparer : IComparer<EntityView>
        {
            SortOrder sortOrder = SortOrder.None;

            public EVComparer(SortOrder sortingOrder) { sortOrder = sortingOrder; }

            public int Compare(EntityView x, EntityView y)
            {
                int returnValue = 1;

                if (sortOrder == SortOrder.Ascending)
                    returnValue = x.DisplayName.CompareTo(y.DisplayName);
                else
                    returnValue = y.DisplayName.CompareTo(x.DisplayName);

                return returnValue;
            }
        }



        private List<EntityView> Entities = new List<EntityView>();
        public List<EntityMetadata> EntityMetadatas = new List<EntityMetadata>();
        public List<EntityMetadata> SelectedEntities = new List<EntityMetadata>();

        public FrmMultiTable(List<EntityMetadata> entities)
        {
            InitializeComponent();
            EntityMetadatas = entities;
            Entities.AddRange(EntityMetadatas.Select(entity => new EntityView
            {
                DisplayName = entity.DisplayName?.UserLocalizedLabel?.Label ?? entity.LogicalName,
                LogicalName = entity.LogicalName
            }));
            Entities.Sort(new EVComparer(SortOrder.Ascending));
            gvMultiTables.DataSource = Entities;
            InitGrid();
            //Entities = entities;

            //lstTables.Service = service;
        }

        private void InitGrid()
        {
            gvMultiTables.Columns["isSelected"].HeaderText = "Select";
            gvMultiTables.Columns["DisplayName"].MinimumWidth = 100;
            gvMultiTables.Columns["DisplayName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            gvMultiTables.Columns["DisplayName"].HeaderText = "Name";

            gvMultiTables.Columns["LogicalName"].MinimumWidth = 50;
            gvMultiTables.Columns["LogicalName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            gvMultiTables.Columns["LogicalName"].HeaderText = "Logical";

            

        }

        private void FrmMultiTable_Load(object sender, EventArgs e)
        {
            gvMultiTables.DataSource = Entities;
            //  lstTables.SortList(0);
            //lstTables.LoadData();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {

            //DialogResult = DialogResult.OK;

            //  SelectedEntities = Entities;
            var returnEntities = new List<EntityMetadata>();
            SelectedEntities = EntityMetadatas.Where(em => gvMultiTables.Rows
                                .Cast<DataGridViewRow>()
                                .Where(x => x.Cells[0].Value.ToString() == "True")
                                .Any(dv => ((EntityView)dv.DataBoundItem).LogicalName == em.LogicalName)).ToList();

            Close();
        }

        private void TxtTableSearch_TextChanged(object sender, EventArgs e)
        {
            if (Entities == null || Entities.Count == 0)
                return;

            if (!string.IsNullOrEmpty(txtTableSearch.Text))
                gvMultiTables.DataSource = Entities.Where(x => x.DisplayName.ToLower().Contains(txtTableSearch.Text.ToLower())
                || x.LogicalName.ToLower().Contains(txtTableSearch.Text.ToLower())).ToList();
            InitGrid();
        }

        private void gvMultiTables_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (gvMultiTables.Columns[e.ColumnIndex].Name != "DisplayName")
                return;

            //get the current column details
            SortOrder sortOrder = getSortOrder(e.ColumnIndex, gvMultiTables);

            Entities.Sort(new EVComparer(sortOrder));
            gvMultiTables.DataSource = null;
            gvMultiTables.DataSource = Entities;

            gvMultiTables.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = sortOrder;
            InitGrid();
        }

        private SortOrder getSortOrder(int columnIndex, DataGridView gv)
        {
            if (gv.Columns[columnIndex].HeaderCell.SortGlyphDirection == SortOrder.None ||
                gv.Columns[columnIndex].HeaderCell.SortGlyphDirection == SortOrder.Descending)
            {
                gv.Columns[columnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                return SortOrder.Ascending;
            }
            else
            {
                gv.Columns[columnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                return SortOrder.Descending;
            }
        }
    }
}
