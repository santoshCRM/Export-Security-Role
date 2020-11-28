using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using RoleDocumenter.Properties;
using XrmToolBox.Extensibility;

namespace RoleDocumenter
{
    public partial class RoleDocumenterCtl
    {
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
            grdview_misRole.Columns["Name"].MinimumWidth = 200;
            grdview_misRole.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdview_misRole.Columns["Name"].DisplayIndex = 0;
            grdview_misRole.Columns["Role"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
        }

        private void InitTableGrid()
        {
            grdview_role.Columns["Name"].MinimumWidth = 200;
            grdview_role.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdview_role.Columns["Name"].DisplayIndex = 0;
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
            var privilege = new List<PrivilegeSet>();
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
            PrivilegeSet pri = null;

            bool check = true;
            foreach (Entity ent in returnCollection.Entities)
            {
                if (((AliasedValue)ent.Attributes["objecttype.objecttypecode"]).Value.ToString() == "none")
                {
                    MisprivilegeSet mispri = new MisprivilegeSet(((AliasedValue)ent.Attributes["privilege.name"]).Value.ToString());
                    MisPrivilegeSets.Add(mispri);
                }
                else
                {
                    var item = privilege.FirstOrDefault(i => i.Name == ((AliasedValue)ent.Attributes["objecttype.objecttypecode"]).Value.ToString());

                    if (item != null)
                    {
                        pri = (PrivilegeSet)item; check = false;
                    }
                    else
                    {
                        pri = new PrivilegeSet(((AliasedValue)ent.Attributes["objecttype.objecttypecode"]).Value.ToString());
                        check = true;
                    }


                    #region Switch Case
                    switch (((AliasedValue)ent.Attributes["privilege.accessright"]).Value.ToString())
                    {
                        case "1"://READ
                            {
                                if (ent.Attributes["privilegedepthmask"].ToString() == "1")//User
                                {
                                    pri.Read = Resources.user;
                                    pri.Read.Tag = "User";
                                }
                                if (ent.Attributes["privilegedepthmask"].ToString() == "2")//Business Unit
                                {
                                    pri.Read = Resources.BU;
                                    pri.Read.Tag = "Business Unit";
                                }
                                if (ent.Attributes["privilegedepthmask"].ToString() == "4")//Parent: Child Business Unit
                                {
                                    pri.Read = Resources.P_BU;
                                    pri.Read.Tag = "Parent: Child Business Unit";
                                }
                                if (ent.Attributes["privilegedepthmask"].ToString() == "8")//Organisation
                                {
                                    pri.Read = Resources.organization;
                                    pri.Read.Tag = "Organization";
                                }
                                break;
                            }
                        case "2"://WRITE
                            {
                                if (ent.Attributes["privilegedepthmask"].ToString() == "1")//User
                                {
                                    pri.Write = Resources.user;
                                    pri.Write.Tag = "User";
                                }
                                if (ent.Attributes["privilegedepthmask"].ToString() == "2")//Business Unit
                                {
                                    pri.Write = Resources.BU;
                                    pri.Write.Tag = "Business Unit";
                                }
                                if (ent.Attributes["privilegedepthmask"].ToString() == "4")//Parent: Child Business Unit
                                {
                                    pri.Write = Resources.P_BU;
                                    pri.Write.Tag = "Parent: Child Business Unit";
                                }
                                if (ent.Attributes["privilegedepthmask"].ToString() == "8")//Organisation
                                {
                                    pri.Write = Resources.organization;
                                    pri.Write.Tag = "Organization";
                                }
                                break;
                            }
                        case "4"://APPEND
                            {
                                if (ent.Attributes["privilegedepthmask"].ToString() == "1")//User
                                {
                                    pri.Append = Resources.user;
                                    pri.Append.Tag = "User";
                                }
                                if (ent.Attributes["privilegedepthmask"].ToString() == "2")//Business Unit
                                {
                                    pri.Append = Resources.BU;
                                    pri.Append.Tag = "Business Unit";
                                }
                                if (ent.Attributes["privilegedepthmask"].ToString() == "4")//Parent: Child Business Unit
                                {
                                    pri.Append = Resources.P_BU;
                                    pri.Append.Tag = "Parent: Child Business Unit";
                                }
                                if (ent.Attributes["privilegedepthmask"].ToString() == "8")//Organisation
                                {
                                    pri.Append = Resources.organization;
                                    pri.Append.Tag = "Organization";
                                }
                                break;
                            }
                        case "16"://APPENDTO
                            {
                                if (ent.Attributes["privilegedepthmask"].ToString() == "1")//User
                                {
                                    pri.AppendTo = Resources.user;
                                    pri.AppendTo.Tag = "User";
                                }
                                if (ent.Attributes["privilegedepthmask"].ToString() == "2")//Business Unit
                                {
                                    pri.AppendTo = Resources.BU;
                                    pri.AppendTo.Tag = "Business Unit";
                                }
                                if (ent.Attributes["privilegedepthmask"].ToString() == "4")//Parent: Child Business Unit
                                {
                                    pri.AppendTo = Resources.P_BU;
                                    pri.AppendTo.Tag = "Parent: Child Business Unit";
                                }
                                if (ent.Attributes["privilegedepthmask"].ToString() == "8")//Organisation
                                {
                                    pri.AppendTo = Resources.organization;
                                    pri.AppendTo.Tag = "Organization";
                                }
                                break;
                            }
                        case "32"://CREATE
                            {
                                if (ent.Attributes["privilegedepthmask"].ToString() == "1")//User
                                {
                                    pri.Create = Resources.user;
                                    pri.Create.Tag = "User";
                                }
                                if (ent.Attributes["privilegedepthmask"].ToString() == "2")//Business Unit
                                {
                                    pri.Create = Resources.BU;
                                    pri.Create.Tag = "Business Unit";
                                }
                                if (ent.Attributes["privilegedepthmask"].ToString() == "4")//Parent: Child Business Unit
                                {
                                    pri.Create = Resources.P_BU;
                                    pri.Create.Tag = "Parent: Child Business Unit";
                                }
                                if (ent.Attributes["privilegedepthmask"].ToString() == "8")//Organisation
                                {
                                    pri.Create = Resources.organization;
                                    pri.Create.Tag = "Organization";
                                }
                                break;
                            }
                        case "65536"://DELETE
                            {
                                if (ent.Attributes["privilegedepthmask"].ToString() == "1")//User
                                {
                                    pri.Delete = Resources.user;
                                    pri.Delete.Tag = "User";
                                }
                                if (ent.Attributes["privilegedepthmask"].ToString() == "2")//Business Unit
                                {
                                    pri.Delete = Resources.BU;
                                    pri.Delete.Tag = "Business Unit";
                                }
                                if (ent.Attributes["privilegedepthmask"].ToString() == "4")//Parent: Child Business Unit
                                {
                                    pri.Delete = Resources.P_BU;
                                    pri.Read.Tag = "Parent: Child Business Unit";
                                }
                                if (ent.Attributes["privilegedepthmask"].ToString() == "8")//Organisation
                                {
                                    pri.Delete = Resources.organization;
                                    pri.Delete.Tag = "Organization";
                                }
                                break;
                            }
                        case "262144"://SHARE
                            {
                                if (ent.Attributes["privilegedepthmask"].ToString() == "1")//User
                                {
                                    pri.Share = Resources.user;
                                    pri.Share.Tag = "User";
                                }
                                if (ent.Attributes["privilegedepthmask"].ToString() == "2")//Business Unit
                                {
                                    pri.Share = Resources.BU;
                                    pri.Share.Tag = "Business Unit";
                                }
                                if (ent.Attributes["privilegedepthmask"].ToString() == "4")//Parent: Child Business Unit
                                {
                                    pri.Share = Resources.P_BU;
                                    pri.Share.Tag = "Parent: Child Business Unit";
                                }
                                if (ent.Attributes["privilegedepthmask"].ToString() == "8")//Organisation
                                {
                                    pri.Share = Resources.organization;
                                    pri.Share.Tag = "Organization";
                                }
                                break;
                            }
                        case "524288"://ASSIGN
                            {
                                if (ent.Attributes["privilegedepthmask"].ToString() == "1")//User
                                {
                                    pri.Assign = Resources.user;
                                    pri.Assign.Tag = "User";
                                }
                                if (ent.Attributes["privilegedepthmask"].ToString() == "2")//Business Unit
                                {
                                    pri.Assign = Resources.BU;
                                    pri.Assign.Tag = "Business Unit";
                                }
                                if (ent.Attributes["privilegedepthmask"].ToString() == "4")//Parent: Child Business Unit
                                {
                                    pri.Assign = Resources.P_BU;
                                    pri.Assign.Tag = "Parent: Child Business Unit";
                                }
                                if (ent.Attributes["privilegedepthmask"].ToString() == "8")//Organisation
                                {
                                    pri.Assign = Resources.organization;
                                    pri.Assign.Tag = "Organization";
                                }
                                break;
                            }
                    }
                    #endregion
                    if (check)
                        privilege.Add(pri);
                }
            }

            return privilege;
        }


        internal object saveExcel(string excelname)
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
