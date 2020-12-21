using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using RoleDocumenter.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using XrmToolBox.Extensibility;

namespace RoleDocumenter
{
    public partial class RoleDocumenterCtl
    {
        private List<EntityMetadata> entities;

        protected List<EntityMetadata> Entities
        {
            get
            {
                if (entities == null)
                {// Get localised names
                    var query = new RetrieveAllEntitiesRequest();
                    entities = ((RetrieveAllEntitiesResponse)Service.Execute(query)).EntityMetadata.ToList();

                }
                return entities;
            }
        }

        private List<string> nonCustom;
        protected List<string> NonCustom
        {
            get
            {
                if (nonCustom == null)
                {
                    nonCustom = new List<string>(BPFs);

                    foreach (SecurityTab secTab in D365Tabs)
                    {
                       nonCustom = nonCustom.Concat(secTab.tables).ToList();
                    }

                }
                return nonCustom;
            }
        }
        private List<string> bpfs;
        protected List<string> BPFs
        {
            get
            {
                if (bpfs == null)
                {
                    var workflowQry = new QueryExpression("workflow");
                    workflowQry.ColumnSet.AddColumn("uniquename");
                    workflowQry.Criteria.AddCondition("category", ConditionOperator.Equal, "4");
                    var wkFLows = Service.RetrieveMultiple(workflowQry);
                    bpfs = wkFLows.Entities.Select(r => r.GetAttributeValue<string>("uniquename")).ToList();

                }
                return bpfs;
            }
        }

        internal static List<MisprivilegeSet> MisPrivilegeSets { get; set; }
        internal static List<PrivilegeSet> PrivilegeSets { get; set; }

        private void PopulateRoles()
        {
            List<SecurityRole> MyEntitys = new List<SecurityRole>();
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Retrieving Security Roles",
                Work = (w, e) =>
                {
                    MyEntitys = GetSecurityRoles();
                    MyEntitys.Sort((p, q) => p.Name.CompareTo(q.Name));
                    SecurityRole Select = new SecurityRole();
                    Select.Name = "--Select--";
                    Select.RoldId = Guid.Empty;
                    MyEntitys.Insert(0, Select);
                },
                ProgressChanged = e =>
                {
                    SetWorkingMessage(e.UserState.ToString());
                },
                PostWorkCallBack = e =>
                {
                    drp_roles.DataSource = null;
                    drp_roles.Items.Clear();

                    drp_roles.DataSource = MyEntitys;
                    drp_roles.DisplayMember = "Name";
                    drp_roles.ValueMember = "RoldId";

                    grdview_role.DataSource = null;
                    grdview_misRole.DataSource = null;
                }
            });
        }

        private List<SecurityRole> GetSecurityRoles()
        {
            EntityCollection returnCollection;
            List<SecurityRole> accessRoles = new List<SecurityRole>();
            int fetchCount = 5000;// Initialize the page number.
            int pageNumber = 1;// Initialize the number of records.
            string pagingCookie = null;
            string fetchXML = "<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='true'>"
  + "<entity name='role'>"
    + "<attribute name='name' />"
    + "<attribute name='businessunitid' />"
    + "<attribute name='roleid' />"
    + "<order attribute='name' descending='false' />"
        + "<filter type='and'>"
          + "<condition attribute='businessunitid' operator='eq-businessid'  />"
        + "</filter>"
  + "</entity>"
+ "</fetch>";
            while (true)
            {

                // Build fetchXml string with the placeholders.
                string xml = CreateXml(fetchXML, pagingCookie, pageNumber, fetchCount);

                // Excute the fetch query and get the xml result.
                RetrieveMultipleRequest fetchRequest1 = new RetrieveMultipleRequest
                {
                    Query = new FetchExpression(xml)
                };

                returnCollection = ((RetrieveMultipleResponse)Service.Execute(fetchRequest1)).EntityCollection;
                foreach (Entity ent in returnCollection.Entities)
                {
                    accessRoles.Add(new SecurityRole
                    {
                        Name = ent.Attributes["name"].ToString(),
                        BusinessUnit = (EntityReference)ent.Attributes["businessunitid"],
                        RoldId = ent.Id
                    });
                }

                // Check for morerecords, if it returns 1.
                if (returnCollection.MoreRecords)
                {
                    // Increment the page number to retrieve the next page.
                    pageNumber++;
                    // Set the paging cookie to the paging cookie returned from current results.                            
                    pagingCookie = returnCollection.PagingCookie;
                }
                else
                {
                    // If no more records in the result nodes, exit the loop.
                    break;
                }
            }


            return accessRoles;
        }

        private string CreateXml(string fetchXml, string pagingCookie, int pageNumber, int fetchCount)
        {
            StringReader stringReader = new StringReader(fetchXml);
            XmlTextReader reader = new XmlTextReader(stringReader);

            // Load document
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);

            return CreateXml(doc, pagingCookie, pageNumber, fetchCount);
        }

        internal static string CreateXml(XmlDocument doc, string cookie, int page, int count)
        {
            XmlAttributeCollection attrs = doc.DocumentElement.Attributes;

            if (cookie != null)
            {
                XmlAttribute pagingAttr = doc.CreateAttribute("paging-cookie");
                pagingAttr.Value = cookie;
                attrs.Append(pagingAttr);
            }

            XmlAttribute pageAttr = doc.CreateAttribute("page");
            pageAttr.Value = System.Convert.ToString(page);
            attrs.Append(pageAttr);

            XmlAttribute countAttr = doc.CreateAttribute("count");
            countAttr.Value = System.Convert.ToString(count);
            attrs.Append(countAttr);

            StringBuilder sb = new StringBuilder(1024);
            StringWriter stringWriter = new StringWriter(sb);

            XmlTextWriter writer = new XmlTextWriter(stringWriter);
            doc.WriteTo(writer);
            writer.Close();

            return sb.ToString();
        }

        private void GetRole(SecurityRole seletectRole)
        {
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Retrieving Role Detail",
                Work = (w, e) =>
                {
                    var priv = RolePrivileges(seletectRole);
                    e.Result = priv;
                    //MyEntitys = Helper.getSecurityRole();
                    //MyEntitys.Sort((p, q) => p.Name.CompareTo(q.Name));
                    //SecurityRole Select = new SecurityRole();
                    //Select.Name = "--Select--";
                    //Select.RoldId = Guid.Empty;
                    //MyEntitys.Insert(0, Select);
                },
                ProgressChanged = e =>
                {
                    SetWorkingMessage(e.UserState.ToString());
                },
                PostWorkCallBack = e =>
                {
                    PrivilegeSets = e.Result as List<PrivilegeSet>;
                    grdview_role.DataSource = PrivilegeSets;
                    InitTableGrid();

                    grdview_misRole.DataSource = MisPrivilegeSets;
                    InitMiscGrid();




                }
            });
        }

        private void InitMiscGrid()
        {
            grdview_misRole.Columns["Name"].Visible = false;
            grdview_misRole.Columns["DisplayName"].MinimumWidth = 200;
            grdview_misRole.Columns["DisplayName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdview_misRole.Columns["DisplayName"].DisplayIndex = 0;
            grdview_misRole.Columns["DisplayName"].HeaderText = "Name";


            grdview_misRole.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdview_misRole.Columns["Name"].DisplayIndex = 0;
            grdview_misRole.Columns["Role"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
        }

        private void InitTableGrid()
        {
            grdview_role.Columns["DisplayName"].MinimumWidth = 200;
            grdview_role.Columns["DisplayName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdview_role.Columns["DisplayName"].DisplayIndex = 0;

            grdview_role.Columns["DisplayName"].HeaderText = "Name";

            grdview_role.Columns["Name"].Visible = false;
            grdview_role.Columns["Create"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

            grdview_role.Columns["Read"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            grdview_role.Columns["Write"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            grdview_role.Columns["Delete"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            grdview_role.Columns["Append"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            grdview_role.Columns["AppendTo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            grdview_role.Columns["Assign"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            grdview_role.Columns["Share"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
        }

        private object RolePrivileges(SecurityRole selectedRole)
        {
            EntityCollection returnCollection;
            var privileges = new List<PrivilegeSet>();
            MisPrivilegeSets = new List<MisprivilegeSet>();
            int fetchCount = 5000;// Initialize the page number.
            int pageNumber = 1;// Initialize the number of records.
            string pagingCookie = null;
            string fetchXML = "<fetch returntotalrecordcount='true' >" +
"  <entity name='roleprivileges' >" +
"    <attribute name='privilegedepthmask' />" +
"    <link-entity name='role' from='roleid' to='roleid' alias='role' >" +
"      <attribute name='name' />" +
"      <filter>" +
"        <condition attribute='name' operator='eq' value='" + selectedRole.Name + "' />" +
"      </filter>" +
"    </link-entity>" +
"    <link-entity name='privilegeobjecttypecodes' from='privilegeid' to='privilegeid' alias='objecttype' >" +
"      <attribute name='objecttypecode' />" +
"    </link-entity>" +
"    <link-entity name='privilege' from='privilegeid' to='privilegeid'  alias='privilege' >" +
"      <attribute name='accessright' />" +
"      <attribute name='name' />" +
      "<attribute name = 'canbeentityreference' />" +
 "    </link-entity>" +
"  </entity>" +
"</fetch>";
            while (true)
            {
                // Build fetchXml string with the placeholders.
                string xml = CreateXml(fetchXML, pagingCookie, pageNumber, fetchCount);

                // Excute the fetch query and get the xml result.
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
                {
                    // If no more records in the result nodes, exit the loop.
                    break;
                }
            }

            foreach (Entity ent in returnCollection.Entities)
            {
                var logicalName = ((AliasedValue)ent.Attributes["objecttype.objecttypecode"]).Value.ToString();
                var isEntity = (bool)((AliasedValue)ent.Attributes["privilege.canbeentityreference"]).Value;
                var priName = ((AliasedValue)ent.Attributes["privilege.name"]).Value.ToString();
                if (logicalName == "activitypointer") AddPermission(ent, privileges);
                else if (TableExlusionList.Contains(logicalName)) { }
                //   else if (CustomisationList.Contains(logicalName)) AddPermission(ent, "Customization");
                else if (PrvExclusionList.Contains(priName)) { }
                else if (TablePrefixes.Any(tp => priName.Contains(tp))) AddPermission(ent, privileges);
                else if (!isEntity) // not an entity
                {
                    AddMiscPriv(ent);
                }
                else
                {
                    AddPermission(ent, privileges);

                }
            }

            return privileges;
        }

        private void AddMiscPriv(Entity ent)
        {
            AddMiscPriv(ent, ((AliasedValue)ent.Attributes["privilege.name"]).Value.ToString());
        }

        private void AddMiscPriv(Entity ent, string privName)
        {
            MisprivilegeSet mispri = new MisprivilegeSet(privName, GetMiscDisplay(privName));
            // CC Some misc settings have depth
            switch (ent.Attributes["privilegedepthmask"].ToString())
            {
                case "1":
                    mispri.Role = Resources.user;
                    mispri.Role.Tag = "User";
                    break;
                case "2":
                    mispri.Role = Resources.BU;
                    mispri.Role.Tag = "Business Unit";
                    break;
                case "4":
                    mispri.Role = Resources.P_BU;
                    mispri.Role.Tag = "Parent: Child Business Unit";
                    break;
                case "8":
                    mispri.Role = Resources.organization;
                    mispri.Role.Tag = "Organisation";
                    break;
                default:
                    mispri.Role = Resources.organization;
                    mispri.Role.Tag = "Organisation";
                    break;
            }
            MisPrivilegeSets.Add(mispri);
        }

        private void AddPermission(Entity ent, List<PrivilegeSet> privileges)
        {
            //  PrivilegeSet pri = null;
            bool check = true;
            var logicalName = ((AliasedValue)ent.Attributes["objecttype.objecttypecode"]).Value.ToString();
            if (CustomisationList.Contains(logicalName)) logicalName = "Customizations";

            var priSet = privileges.FirstOrDefault(i => i.Name == logicalName);

            if (priSet == null)
            {
                priSet = new PrivilegeSet(logicalName, chkLocalised.Checked ? (Entities.FirstOrDefault(entity => entity.LogicalName == logicalName)?.DisplayName?.UserLocalizedLabel?.Label ?? logicalName) : logicalName);
                privileges.Add(priSet);
                //check = true;
            }


            #region Switch Case
            switch (((AliasedValue)ent.Attributes["privilege.accessright"]).Value.ToString())
            {
                case "1"://READ
                    {
                        if (ent.Attributes["privilegedepthmask"].ToString() == "1")//User
                        {
                            priSet.Read = Resources.user;
                            priSet.Read.Tag = "User";
                        }
                        if (ent.Attributes["privilegedepthmask"].ToString() == "2")//Business Unit
                        {
                            priSet.Read = Resources.BU;
                            priSet.Read.Tag = "Business Unit";
                        }
                        if (ent.Attributes["privilegedepthmask"].ToString() == "4")//Parent: Child Business Unit
                        {
                            priSet.Read = Resources.P_BU;
                            priSet.Read.Tag = "Parent: Child Business Unit";
                        }
                        if (ent.Attributes["privilegedepthmask"].ToString() == "8")//Organisation
                        {
                            priSet.Read = Resources.organization;
                            priSet.Read.Tag = "Organization";
                        }
                        break;
                    }
                case "2"://WRITE
                    {
                        if (ent.Attributes["privilegedepthmask"].ToString() == "1")//User
                        {
                            priSet.Write = Resources.user;
                            priSet.Write.Tag = "User";
                        }
                        if (ent.Attributes["privilegedepthmask"].ToString() == "2")//Business Unit
                        {
                            priSet.Write = Resources.BU;
                            priSet.Write.Tag = "Business Unit";
                        }
                        if (ent.Attributes["privilegedepthmask"].ToString() == "4")//Parent: Child Business Unit
                        {
                            priSet.Write = Resources.P_BU;
                            priSet.Write.Tag = "Parent: Child Business Unit";
                        }
                        if (ent.Attributes["privilegedepthmask"].ToString() == "8")//Organisation
                        {
                            priSet.Write = Resources.organization;
                            priSet.Write.Tag = "Organization";
                        }
                        break;
                    }
                case "4"://APPEND
                    {
                        if (ent.Attributes["privilegedepthmask"].ToString() == "1")//User
                        {
                            priSet.Append = Resources.user;
                            priSet.Append.Tag = "User";
                        }
                        if (ent.Attributes["privilegedepthmask"].ToString() == "2")//Business Unit
                        {
                            priSet.Append = Resources.BU;
                            priSet.Append.Tag = "Business Unit";
                        }
                        if (ent.Attributes["privilegedepthmask"].ToString() == "4")//Parent: Child Business Unit
                        {
                            priSet.Append = Resources.P_BU;
                            priSet.Append.Tag = "Parent: Child Business Unit";
                        }
                        if (ent.Attributes["privilegedepthmask"].ToString() == "8")//Organisation
                        {
                            priSet.Append = Resources.organization;
                            priSet.Append.Tag = "Organization";
                        }
                        break;
                    }
                case "16"://APPENDTO
                    {
                        if (ent.Attributes["privilegedepthmask"].ToString() == "1")//User
                        {
                            priSet.AppendTo = Resources.user;
                            priSet.AppendTo.Tag = "User";
                        }
                        if (ent.Attributes["privilegedepthmask"].ToString() == "2")//Business Unit
                        {
                            priSet.AppendTo = Resources.BU;
                            priSet.AppendTo.Tag = "Business Unit";
                        }
                        if (ent.Attributes["privilegedepthmask"].ToString() == "4")//Parent: Child Business Unit
                        {
                            priSet.AppendTo = Resources.P_BU;
                            priSet.AppendTo.Tag = "Parent: Child Business Unit";
                        }
                        if (ent.Attributes["privilegedepthmask"].ToString() == "8")//Organisation
                        {
                            priSet.AppendTo = Resources.organization;
                            priSet.AppendTo.Tag = "Organization";
                        }
                        break;
                    }
                case "32"://CREATE
                    {
                        if (ent.Attributes["privilegedepthmask"].ToString() == "1")//User
                        {
                            priSet.Create = Resources.user;
                            priSet.Create.Tag = "User";
                        }
                        if (ent.Attributes["privilegedepthmask"].ToString() == "2")//Business Unit
                        {
                            priSet.Create = Resources.BU;
                            priSet.Create.Tag = "Business Unit";
                        }
                        if (ent.Attributes["privilegedepthmask"].ToString() == "4")//Parent: Child Business Unit
                        {
                            priSet.Create = Resources.P_BU;
                            priSet.Create.Tag = "Parent: Child Business Unit";
                        }
                        if (ent.Attributes["privilegedepthmask"].ToString() == "8")//Organisation
                        {
                            priSet.Create = Resources.organization;
                            priSet.Create.Tag = "Organization";
                        }
                        break;
                    }
                case "65536"://DELETE
                    {
                        if (ent.Attributes["privilegedepthmask"].ToString() == "1")//User
                        {
                            priSet.Delete = Resources.user;
                            priSet.Delete.Tag = "User";
                        }
                        if (ent.Attributes["privilegedepthmask"].ToString() == "2")//Business Unit
                        {
                            priSet.Delete = Resources.BU;
                            priSet.Delete.Tag = "Business Unit";
                        }
                        if (ent.Attributes["privilegedepthmask"].ToString() == "4")//Parent: Child Business Unit
                        {
                            priSet.Delete = Resources.P_BU;
                            priSet.Read.Tag = "Parent: Child Business Unit";
                        }
                        if (ent.Attributes["privilegedepthmask"].ToString() == "8")//Organisation
                        {
                            priSet.Delete = Resources.organization;
                            priSet.Delete.Tag = "Organization";
                        }
                        break;
                    }
                case "262144"://SHARE
                    {
                        if (ent.Attributes["privilegedepthmask"].ToString() == "1")//User
                        {
                            priSet.Share = Resources.user;
                            priSet.Share.Tag = "User";
                        }
                        if (ent.Attributes["privilegedepthmask"].ToString() == "2")//Business Unit
                        {
                            priSet.Share = Resources.BU;
                            priSet.Share.Tag = "Business Unit";
                        }
                        if (ent.Attributes["privilegedepthmask"].ToString() == "4")//Parent: Child Business Unit
                        {
                            priSet.Share = Resources.P_BU;
                            priSet.Share.Tag = "Parent: Child Business Unit";
                        }
                        if (ent.Attributes["privilegedepthmask"].ToString() == "8")//Organisation
                        {
                            priSet.Share = Resources.organization;
                            priSet.Share.Tag = "Organization";
                        }
                        break;
                    }
                case "524288"://ASSIGN
                    {
                        if (ent.Attributes["privilegedepthmask"].ToString() == "1")//User
                        {
                            priSet.Assign = Resources.user;
                            priSet.Assign.Tag = "User";
                        }
                        if (ent.Attributes["privilegedepthmask"].ToString() == "2")//Business Unit
                        {
                            priSet.Assign = Resources.BU;
                            priSet.Assign.Tag = "Business Unit";
                        }
                        if (ent.Attributes["privilegedepthmask"].ToString() == "4")//Parent: Child Business Unit
                        {
                            priSet.Assign = Resources.P_BU;
                            priSet.Assign.Tag = "Parent: Child Business Unit";
                        }
                        if (ent.Attributes["privilegedepthmask"].ToString() == "8")//Organisation
                        {
                            priSet.Assign = Resources.organization;
                            priSet.Assign.Tag = "Organization";
                        }
                        break;
                    }
            }
            #endregion
            //if (check)
            //    privileges.Add(priSet);
        }

        private string GetMiscDisplay(string miscName)
        {
            return MiscPrivs.FirstOrDefault(mp => mp.Name == miscName)?.Display ?? miscName;
        }

        internal string saveExcel(string excelname)
        {

            SaveFileDialog sfd = new SaveFileDialog();// Create save the Excel
            sfd.Filter = "Text File|*.xlsx";// filters for text files only
            sfd.DefaultExt = "xlsx";
            sfd.AddExtension = true;
            sfd.FileName = excelname + ".xlsx";
            sfd.Title = "Save Excel File";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                return sfd.FileName;
            }
            else
            {
                return null;
            }
        }
    }
}
