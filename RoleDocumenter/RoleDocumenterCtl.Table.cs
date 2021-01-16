using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XrmToolBox.Extensibility;

namespace RoleDocumenter
{
    partial class RoleDocumenterCtl
    {
        internal static List<PrivilegeSet> TablePriviliges { get; set; }
        private void GetTableRoles(EntityMetadata table)
        {
            WorkAsync(
               new WorkAsyncInfo
               {
                   //   Message = "Retrieving Roles for " + table.DisplayName?.UserLocalizedLabel?.Label ?? table.LogicalName,
                   Work =
                       (w, e) =>
                       {
                           var privileges = GetRolesForTable(table, w);
                           e.Result = privileges;

                       },
                   ProgressChanged = e => SetWorkingMessage(e.UserState.ToString()),
                   PostWorkCallBack =
                       e =>
                       {
                           TablePriviliges = e.Result as List<PrivilegeSet>;
                           gvRoles.DataSource = TablePriviliges;
                           InitRoleGrid();

                       }
               });
        }

        private void InitRoleGrid()
        {
            gvRoles.Columns["DisplayName"].MinimumWidth = 200;
            gvRoles.Columns["DisplayName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            gvRoles.Columns["DisplayName"].DisplayIndex = 0;

            gvRoles.Columns["DisplayName"].HeaderText = "Name";

            gvRoles.Columns["Name"].Visible = false;
            gvRoles.Columns["Create"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

            gvRoles.Columns["Read"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            gvRoles.Columns["Write"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            gvRoles.Columns["Delete"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            gvRoles.Columns["Append"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            gvRoles.Columns["AppendTo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            gvRoles.Columns["Assign"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            gvRoles.Columns["Share"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
        }

        private List<PrivilegeSet> GetRolesForTable(EntityMetadata table, BackgroundWorker work)
        {

            string tableName = table.IsActivity ?? false
                ? "Activity"
                : (table.LogicalName == "annotation") ? "Note"
                : (table.LogicalName == "activitypointer") ? "Activity"
                : table.LogicalName.ToLower();

            work.ReportProgress(-1, "Getting Roles for " + tableName);
            int fetchCount = 5000;// Initialize the page number.
            int pageNumber = 1;// Initialize the number of records.
            string pagingCookie = null;
            string fetchXML = $@"<fetch>
                  <entity name='roleprivileges' >
                    <attribute name='privilegedepthmask' />
                    <link-entity name='privilege' from='privilegeid' to='privilegeid' >
                      <attribute name='accessright' alias='AccessRight' />
                      <filter>
                        <condition attribute='name' operator='in' >
                          <value>prvRead{tableName}</value>
                          <value>prvWrite{tableName}</value>
                          <value>prvAppend{tableName}</value>
                          <value>prvAppendTo{tableName}</value>
                          <value>prvCreate{tableName}</value>
                          <value>prvDelete{tableName}</value>
                          <value>prvShare{tableName}</value>
                          <value>prvAssign{tableName}</value>

                        </condition>
                      </filter>
                    </link-entity>
                    <link-entity name='role' from='roleid' to='roleid' >
                      <attribute name='name' alias='RoleName' />
                    </link-entity>
                  </entity>
                </fetch>";

            EntityCollection returnCollection;

            while (true)
            {
                // Build fetchXml string with the placeholders.
                string xml = CreateXml(fetchXML, pagingCookie, pageNumber, fetchCount);

                // Execute the fetch query and get the xml result.
                RetrieveMultipleRequest fetchRequest1 = new RetrieveMultipleRequest
                {
                    Query = new FetchExpression(xml)
                };

                returnCollection = ((RetrieveMultipleResponse)Service.Execute(fetchRequest1)).EntityCollection;

                // Check for morerecords, if it returns 1.
                if (returnCollection.MoreRecords)
                {
                    // Increment the page number to retrieve the next page.
                    pageNumber++;

                    // Set the paging cookie to the paging cookie returned from current results.                            
                    pagingCookie = returnCollection.PagingCookie;
                }
                else
                    // If no more records in the result nodes, exit the loop.
                    break;
            }
            var privileges = new List<PrivilegeSet>();
            foreach (Entity rolePriv in returnCollection.Entities)
                AddTablePermission(rolePriv, privileges);
            privileges.Sort(new PrivilegeComparer(SortOrder.Ascending));
            return privileges;
            // Privilage = new List<privilegeSet>();
        }

        private void AddTablePermission(Entity rolePriv, List<PrivilegeSet> privileges)
        {
            var roleName = ((AliasedValue)rolePriv.Attributes["RoleName"]).Value.ToString();

            var priSet = privileges.FirstOrDefault(i => i.Name == roleName);

            if (priSet == null)
            {
                priSet = new PrivilegeSet(roleName);
                privileges.Add(priSet);
            }

            PopulatePrivilege(priSet, rolePriv, privileges);
        }

        private ExcelPackage CreateMultiTableExcel(List<EntityMetadata> entities)
        {
            var package = new ExcelPackage();

            WorkAsync(
                new WorkAsyncInfo
                {
                    Message = "Retrieving Roles",
                    Work =
                        (w, e) =>
                            {
                                var tablePrivs = new List<TablePrivSet>();
                                foreach (var entity in entities)
                                    tablePrivs.Add(
                                        new TablePrivSet(
                                            entity.IsActivity ?? false
                                                ? "Activity"
                                                : (entity.LogicalName == "annotation")
                                                    ? "Note"
                                                    : (entity.LogicalName == "activitypointer")
                                                        ? "Activity"
                                                        : entity.LogicalName.ToLower())
                                        {
                                            Privileges = GetRolesForTable(entity, w)
                                        });

                                e.Result = tablePrivs;
                            },
                    ProgressChanged = e => SetWorkingMessage(e.UserState.ToString()),
                    PostWorkCallBack =
                        e =>
                            {
                                var tablePriviliges = e.Result as List<TablePrivSet>;

                                foreach (TablePrivSet tablePrivSet in tablePriviliges)
                                    CreateExcel(
                                        package,
                                        tablePrivSet.Privileges,
                                        null,
                                        tablePrivSet.TableName);


                            }
                });
            return package;
        }
    }
}
