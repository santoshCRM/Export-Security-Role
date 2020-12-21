using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;

namespace RoleDocumenter
{
    public partial class RoleDocumenterCtl : PluginControlBase, IGitHubPlugin
    {
        public string RepositoryName => "Export-Security-Role";
        private AppInsights ai;
        private const string aiEndpoint = "https://dc.services.visualstudio.com/v2/track";

        private const string aiKey = "f521c450-81df-45cb-aedc-700df1b55541";
        public string UserName => "santoshCRM";

        public override void ClosingPlugin(PluginCloseInfo info)
        {
            SettingsManager.Instance.Save(GetType(), roleSettings);

            base.ClosingPlugin(info);
        }
        public Settings roleSettings;
        public RoleDocumenterCtl()
        {
            InitializeComponent();
            ai = new AppInsights(aiEndpoint, aiKey, Assembly.GetExecutingAssembly());
            ai.WriteEvent("Control Loaded");
        }

        private void RoleDocumenterCtl_Load(object sender, EventArgs e)
        {
            if (!SettingsManager.Instance.TryLoad(GetType(), out roleSettings))
            {
                roleSettings = new Settings();

                LogWarning("Settings not found => a new settings file has been created!");
            }
            else
            {
                LogInfo("Settings found and loaded");

                chkLocalised.Checked = roleSettings.DisplayNames;
            }
            ExecuteMethod(PopulateRoles);
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
          //  SettingsManager.Instance.Save(GetType(), roleSettings);
            CloseTool();
        }


        /// <summary>
        /// This event occurs when the plugin is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyPluginControl_OnCloseTool(object sender, EventArgs e)
        {
            // Before leaving, save the settings
            SettingsManager.Instance.Save(GetType(), roleSettings);
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
            if (grdview_role.Columns[e.ColumnIndex].Name != "DisplayName") return;

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
            if (PrivilegeSets.Count == 0)
            {
                MessageBox.Show("Please select a role first", "Select Role", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
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
                wkSheet.Cells[rowCount, 1].Value = chkLocalised.Checked ? privilegeSet.DisplayName : privilegeSet.Name;

                wkSheet.Cells[rowCount, 2].Value = privilegeSet.Create?.Tag ?? string.Empty;
                wkSheet.Cells[rowCount, 3].Value = privilegeSet.Read?.Tag ?? string.Empty;
                wkSheet.Cells[rowCount, 4].Value = privilegeSet.Write?.Tag ?? string.Empty;
                wkSheet.Cells[rowCount, 5].Value = privilegeSet.Delete?.Tag ?? string.Empty;
                wkSheet.Cells[rowCount, 6].Value = privilegeSet.Append?.Tag ?? string.Empty;
                wkSheet.Cells[rowCount, 7].Value = privilegeSet.AppendTo?.Tag ?? string.Empty;
                wkSheet.Cells[rowCount, 8].Value = privilegeSet.Assign?.Tag ?? string.Empty;
                wkSheet.Cells[rowCount, 9].Value = privilegeSet.Share?.Tag ?? string.Empty;

            }
            var table = wkSheet.Tables.Add(wkSheet.Cells[1, 1, rowCount, 9], "Tables");

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
            table = wkSheet.Tables.Add(wkSheet.Cells[1, 1, rowCount, 2], "MisPriv");

            table.TableStyle = TableStyles.Medium3;
            table.ShowHeader = true;
            table.ShowRowStripes = true;
            wkSheet.Cells.AutoFitColumns();

            var fileName = saveExcel(((SecurityRole)drp_roles.SelectedItem).Name);
            if (fileName == null) return;
            //if (saveExcel(((SecurityRole)drp_roles.SelectedItem).Name).ToString()) == null)
            try
            {
                package.SaveAs(new FileInfo(fileName));
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.GetBaseException().Message, exc.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            ai.WriteEvent("Privileges Exported", PrivilegeSets.Count + MisPrivilegeSets.Count);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (PrivilegeSets == null || PrivilegeSets.Count == 0) return;
            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                grdview_role.DataSource = PrivilegeSets.Where(x => x.Name.ToLower().Contains(txtSearch.Text.ToLower()) || x.DisplayName.ToLower().Contains(txtSearch.Text.ToLower())).ToList();
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

        private void BtnExport_D365_Click(object sender, EventArgs e)
        {
            if (PrivilegeSets.Count == 0)
            {
                MessageBox.Show("Please select a role first", "Select Role", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var package = new ExcelPackage();

            // Create the configured tabs
            foreach (SecurityTab tab in D365Tabs)
            {
                var wkSheet = package.Workbook.Worksheets.Add(tab.TabName);

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
                foreach (PrivilegeSet privilegeSet in PrivilegeSets.Where(ps => tab.tables.Contains(ps.Name)))
                {
                    rowCount++;
                    wkSheet.Cells[rowCount, 1].Value = chkLocalised.Checked ? privilegeSet.DisplayName : privilegeSet.Name;
                    wkSheet.Cells[rowCount, 2].Value = privilegeSet.Create?.Tag ?? string.Empty;
                    wkSheet.Cells[rowCount, 3].Value = privilegeSet.Read?.Tag ?? string.Empty;
                    wkSheet.Cells[rowCount, 4].Value = privilegeSet.Write?.Tag ?? string.Empty;
                    wkSheet.Cells[rowCount, 5].Value = privilegeSet.Delete?.Tag ?? string.Empty;
                    wkSheet.Cells[rowCount, 6].Value = privilegeSet.Append?.Tag ?? string.Empty;
                    wkSheet.Cells[rowCount, 7].Value = privilegeSet.AppendTo?.Tag ?? string.Empty;
                    wkSheet.Cells[rowCount, 8].Value = privilegeSet.Assign?.Tag ?? string.Empty;
                    wkSheet.Cells[rowCount, 9].Value = privilegeSet.Share?.Tag ?? string.Empty;

                }

                foreach (MisprivilegeSet mps in MisPrivilegeSets.Where(mp => tab.miscPrivs.Contains(mp.Name)))
                {
                    rowCount++;
                    wkSheet.Cells[rowCount, 1].Value = chkLocalised.Checked ? mps.DisplayName : mps.Name;
                    wkSheet.Cells[rowCount, 2].Value = mps.Role?.Tag ?? string.Empty;
                }
                var tabTable = wkSheet.Tables.Add(wkSheet.Cells[1, 1, rowCount, 9], tab.TabName.Replace(" ", string.Empty) + "Tables");

                tabTable.TableStyle = TableStyles.Medium3;
                tabTable.ShowHeader = true;
                tabTable.ShowRowStripes = true;
                wkSheet.Cells.AutoFitColumns();

            }

            // Creaete BPF Tab
            var wkBPF = package.Workbook.Worksheets.Add("Business Process Flows");

            wkBPF.Cells[1, 1].Value = "Name";
            wkBPF.Cells[1, 2].Value = "Create";

            wkBPF.Cells[1, 3].Value = "Read";

            wkBPF.Cells[1, 4].Value = "Write";
            wkBPF.Cells[1, 5].Value = "Delete";
            wkBPF.Cells[1, 6].Value = "Append";
            wkBPF.Cells[1, 7].Value = "Append To";
            int rowCountBPF = 1;


            foreach (PrivilegeSet privSet in PrivilegeSets.Where(ps => BPFs.Contains(ps.Name)))
            {
                rowCountBPF++;
                wkBPF.Cells[rowCountBPF, 1].Value = chkLocalised.Checked ? privSet.DisplayName : privSet.Name;
                wkBPF.Cells[rowCountBPF, 2].Value = privSet.Create?.Tag ?? string.Empty;
                wkBPF.Cells[rowCountBPF, 3].Value = privSet.Read?.Tag ?? string.Empty;
                wkBPF.Cells[rowCountBPF, 4].Value = privSet.Write?.Tag ?? string.Empty;
                wkBPF.Cells[rowCountBPF, 5].Value = privSet.Delete?.Tag ?? string.Empty;
                wkBPF.Cells[rowCountBPF, 6].Value = privSet.Append?.Tag ?? string.Empty;
                wkBPF.Cells[rowCountBPF, 7].Value = privSet.AppendTo?.Tag ?? string.Empty;
            }

            var bpfTable = wkBPF.Tables.Add(wkBPF.Cells[1, 1, rowCountBPF, 7], "BPFTables");

            bpfTable.TableStyle = TableStyles.Medium3;
            bpfTable.ShowHeader = true;
            bpfTable.ShowRowStripes = true;
            wkBPF.Cells.AutoFitColumns();

            // Create CustomEntities

            var wkShtCustom = package.Workbook.Worksheets.Add("Custom Entities");

            wkShtCustom.Cells[1, 1].Value = "Name";
            wkShtCustom.Cells[1, 2].Value = "Create";

            wkShtCustom.Cells[1, 3].Value = "Read";

            wkShtCustom.Cells[1, 4].Value = "Write";
            wkShtCustom.Cells[1, 5].Value = "Delete";
            wkShtCustom.Cells[1, 6].Value = "Append";
            wkShtCustom.Cells[1, 7].Value = "Append To";
            wkShtCustom.Cells[1, 8].Value = "Assign";
            wkShtCustom.Cells[1, 9].Value = "Share";
            PrivilegeSets.Sort(new PrivelegeComparer(SortOrder.Ascending));
            int rowCountCustom = 1;
            foreach (PrivilegeSet privilegeSet in PrivilegeSets.Where(ps => !NonCustom.Contains(ps.Name)))
            {
                rowCountCustom++;
                wkShtCustom.Cells[rowCountCustom, 1].Value = chkLocalised.Checked ? privilegeSet.DisplayName : privilegeSet.Name;
                wkShtCustom.Cells[rowCountCustom, 2].Value = privilegeSet.Create?.Tag ?? string.Empty;
                wkShtCustom.Cells[rowCountCustom, 3].Value = privilegeSet.Read?.Tag ?? string.Empty;
                wkShtCustom.Cells[rowCountCustom, 4].Value = privilegeSet.Write?.Tag ?? string.Empty;
                wkShtCustom.Cells[rowCountCustom, 5].Value = privilegeSet.Delete?.Tag ?? string.Empty;
                wkShtCustom.Cells[rowCountCustom, 6].Value = privilegeSet.Append?.Tag ?? string.Empty;
                wkShtCustom.Cells[rowCountCustom, 7].Value = privilegeSet.AppendTo?.Tag ?? string.Empty;
                wkShtCustom.Cells[rowCountCustom, 8].Value = privilegeSet.Assign?.Tag ?? string.Empty;
                wkShtCustom.Cells[rowCountCustom, 9].Value = privilegeSet.Share?.Tag ?? string.Empty;

            }

            var custTable = wkShtCustom.Tables.Add(wkShtCustom.Cells[1, 1, rowCountCustom, 9], "CustomTables");

            custTable.TableStyle = TableStyles.Medium3;
            custTable.ShowHeader = true;
            custTable.ShowRowStripes = true;
            wkShtCustom.Cells.AutoFitColumns();

            var fileName = saveExcel(((SecurityRole)drp_roles.SelectedItem).Name);
            if (fileName == null) return;
            try
            {
                package.SaveAs(new FileInfo(fileName));
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.GetBaseException().Message, exc.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            ai.WriteEvent("Privileges Exported", PrivilegeSets.Count + MisPrivilegeSets.Count);
        }

        private void ChkLocalised_CheckedChanged(object sender, EventArgs e)
        {
            roleSettings.DisplayNames = chkLocalised.Checked;
            if (drp_roles.SelectedIndex > 0)
            {
                grdview_role.DataSource = null;
                SecurityRole entity = (SecurityRole)drp_roles.SelectedItem;
                ExecuteMethod(GetRole, entity);
            }

        }

        private void MyPluginControl_OnaCloseTool(object sender, EventArgs e)
        {

        }
    }
}