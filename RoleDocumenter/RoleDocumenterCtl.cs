using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using XrmToolBox.Extensibility;

namespace RoleDocumenter
{
    public partial class RoleDocumenterCtl : PluginControlBase
    {

        public RoleDocumenterCtl()
        {
            InitializeComponent();
        }

        private void RoleDocumenterCtl_Load(object sender, EventArgs e)
        {
            ExecuteMethod(PopulateRoles);
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            CloseTool();
        }


        private void GetRoles()
        {
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Getting All Roles",
                Work = (worker, args) =>
                {
                    args.Result = Service.RetrieveMultiple(new QueryExpression("role")
                    {
                        ColumnSet = new ColumnSet("name", "roleid")
                    });
                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    var result = args.Result as EntityCollection;
                    if (result != null)
                    {
                        MessageBox.Show($"Found {result.Entities.Count} accounts");
                    }
                }
            });
        }

        /// <summary>
        /// This event occurs when the plugin is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyPluginControl_OnCloseTool(object sender, EventArgs e)
        {
            // Before leaving, save the settings
            //SettingsManager.Instance.Save(GetType(), mySettings);
        }

        /// <summary>
        /// This event occurs when the connection has been updated in XrmToolBox
        /// </summary>
        public override void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter)
        {
            base.UpdateConnection(newService, detail, actionName, parameter);

        }

        private void btnRetrieveRoles_Click(object sender, EventArgs e)
        {
            ExecuteMethod(PopulateRoles);
        }

        private void drp_roles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drp_roles.SelectedIndex > 0)
            {

                //GetRole((SecurityRole) drp_roles.SelectedItem);
                grdview_role.DataSource = null;
                SecurityRole entity = (SecurityRole)drp_roles.SelectedItem;
                ExecuteMethod(GetRole, entity);



            }
        }

        private void grdview_role_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (grdview_role.Columns[e.ColumnIndex].Name != "Name") return;

            //get the current column details
            SortOrder sortOrder = getSortOrder(e.ColumnIndex);

            PrivilegeSets.Sort(new PrivelegeComparer(sortOrder));
            grdview_role.DataSource = null;
            grdview_role.DataSource = PrivilegeSets;
            grdview_role.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = sortOrder;
            InitTableGrid();
        }
        private SortOrder getSortOrder(int columnIndex)
        {
            if (grdview_role.Columns[columnIndex].HeaderCell.SortGlyphDirection == SortOrder.None ||
                grdview_role.Columns[columnIndex].HeaderCell.SortGlyphDirection == SortOrder.Descending)
            {
                grdview_role.Columns[columnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                return SortOrder.Ascending;
            }
            else
            {
                grdview_role.Columns[columnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                return SortOrder.Descending;
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            var package = new ExcelPackage();
            var wkSheet = package.Workbook.Worksheets.Add("Table Privileges");

            wkSheet.Cells[1, 1].Value = "Name";
            wkSheet.Cells[1, 2].Value = "Create";

            wkSheet.Cells[1, 3].Value = "Read";

            wkSheet.Cells[1, 4].Value = "Write";
            wkSheet.Cells[1, 5].Value = "Delete";
            wkSheet.Cells[1, 6].Value = "Append";
            wkSheet.Cells[1, 7].Value = "Append To";
            wkSheet.Cells[1, 8].Value = "Assign";
            wkSheet.Cells[1, 9].Value = "Share";
            PrivilegeSets.Sort(new PrivelegeComparer(SortOrder.Ascending));
            int rowCount = 1;
            foreach (PrivilegeSet privilegeSet in PrivilegeSets)
            {
                rowCount++;
                wkSheet.Cells[rowCount, 1].Value = privilegeSet.Name;
                wkSheet.Cells[rowCount, 2].Value = privilegeSet.Create?.Tag ?? string.Empty;
                wkSheet.Cells[rowCount, 3].Value = privilegeSet.Read?.Tag ??string.Empty;
                wkSheet.Cells[rowCount, 4].Value = privilegeSet.Write?.Tag ?? string.Empty;
                wkSheet.Cells[rowCount, 5].Value = privilegeSet.Delete?.Tag ??string.Empty;
                wkSheet.Cells[rowCount, 6].Value = privilegeSet.Append?.Tag ?? string.Empty;
                wkSheet.Cells[rowCount, 7].Value = privilegeSet.AppendTo?.Tag ?? string.Empty;
                wkSheet.Cells[rowCount, 8].Value = privilegeSet.Assign?.Tag ?? string.Empty;
                wkSheet.Cells[rowCount, 9].Value = privilegeSet.Share?.Tag?? string.Empty;

            }
            var table = wkSheet.Tables.Add(wkSheet.Cells[1,1,rowCount,9], "Tables");

            table.TableStyle = TableStyles.Medium3;
            table.ShowHeader = true;
            table.ShowRowStripes = true;
            wkSheet.Cells.AutoFitColumns();

            wkSheet = package.Workbook.Worksheets.Add("Misc Privileges");

            wkSheet.Cells[1, 1].Value = "Name";
            wkSheet.Cells[1, 2].Value = "Role";
            MisPrivilegeSets.Sort(new PrivelegeComparer(SortOrder.Ascending));
            rowCount = 1;
            foreach (MisprivilegeSet misPriv in MisPrivilegeSets)
            {
                rowCount++;
                wkSheet.Cells[rowCount, 1].Value = misPriv.Name;
                wkSheet.Cells[rowCount, 2].Value = misPriv.Role?.Tag ?? string.Empty;


            }
            table = wkSheet.Tables.Add(wkSheet.Cells[1,1,rowCount,2], "MisPriv");

            table.TableStyle = TableStyles.Medium3;
            table.ShowHeader = true;
            table.ShowRowStripes = true;
            wkSheet.Cells.AutoFitColumns();

            package.SaveAs(new FileInfo(saveExcel(((SecurityRole)drp_roles.SelectedItem).Name).ToString()));

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (PrivilegeSets == null || PrivilegeSets.Count == 0) return;
            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                grdview_role.DataSource = PrivilegeSets.Where(x => x.Name.ToLower().Contains(txtSearch.Text.ToLower())).ToList();
                grdview_misRole.DataSource = MisPrivilegeSets.Where(priv => priv.Name.ToLower().Contains(txtSearch.Text.ToLower())).ToList();
            }
            else
            {
                grdview_role.DataSource = PrivilegeSets;
                grdview_misRole.DataSource = MisPrivilegeSets;
            }
            InitMiscGrid();
            InitTableGrid();
        }
    }
}