using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceModel.Description;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;
namespace RoleReplicatorControl
{
    public partial class RoleReplicator : PluginControlBase
    {
        public RoleReplicator()
        {
             InitializeComponent();

           
        }

        private void btn_retUsers_Click(object sender, EventArgs e)
        {
            Helper.createConn(Service);
            if (Service!=null)
            {
                drp_users.Items.Clear();
                List<systemUser> systemUsers = Helper.getUsers();
                systemUsers.Sort((p, q) => p.FullName.CompareTo(q.FullName));
                systemUser Select = new systemUser();
                Select.FullName = "--Select--";
                Select.SystemUserID = Guid.Empty;
                systemUsers.Insert(0, Select);
                drp_users.DataSource = systemUsers;
                drp_users.DisplayMember = "FullName";
                drp_users.ValueMember = "SystemUserID";

                grdview_user.DataSource = Helper.SystemUsergrid;
                grdview_user.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                grdview_user.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                grdview_user.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                grdview_user.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                grdview_user.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            }
        }

        private void drp_users_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool flag = this.drp_users.SelectedIndex > 0;
            if (flag)
            {
                systemUser user = (systemUser)drp_users.SelectedItem;
                List<SecurityRole> userRoles = Helper.getSecurityRole(user.SystemUserID);
                Helper.SecurityRoles = userRoles;
                lstview_sRole.DataSource = Helper.SecurityRoles;
                lstview_sRole.DisplayMember = "Name";
                lstview_sRole.ValueMember = "RoldId";

                List<Team> userTeams = Helper.getTeambyUser(user.SystemUserID, user.BusinessUnit);
                Helper.Teams = userTeams;
                lstbox_Teams.DataSource = Helper.Teams;
                lstbox_Teams.DisplayMember = "Name";
                lstbox_Teams.ValueMember = "TeamId";

                List<Queue> userQueues = Helper.getQueuebyUser(user.SystemUserID, user.BusinessUnit);
                Helper.Queues = userQueues;
                lstbox_queues.DataSource = Helper.Queues;
                lstbox_queues.DisplayMember = "Name";
                lstbox_queues.ValueMember = "QueueId";


            }
        }

        private void btn_copyRole_Click(object sender, EventArgs e)
        {
            if (Service != null)
            {
                Guid[] selecteUsers = new Guid[grdview_user.SelectedRows.Count];
                string[] selecteUserBU = new string[grdview_user.SelectedRows.Count];
                int x = 0;
                foreach (DataGridViewRow row in grdview_user.SelectedRows)
                {
                    selecteUsers[x] = (Guid)row.Cells[1].Value;
                    selecteUserBU[x] = (string)row.Cells[4].Value;
                    x++;
                }
                if (selecteUsers.Count() > 0)
                    Helper.copyRole(selecteUsers, selecteUserBU, chk_role.Checked, chk_team.Checked, chk_queue.Checked);
            }
        }

        private void txt_filter_TextChanged(object sender, EventArgs e)
        {
            if (Service != null)
            {
                if (!string.IsNullOrEmpty(txt_filter.Text))
                    grdview_user.DataSource = Helper.SystemUsergrid.Where(x => x.FullName.Contains(txt_filter.Text)).ToList();
                else
                    grdview_user.DataSource = Helper.SystemUsergrid;
            }
        }

        private void grdview_user_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            foreach (DataGridViewRow row in grdview_user.SelectedRows)
            {
                if (row.Cells[0].ValueType == typeof(bool))
                {
                    if ((bool)row.Cells[0].Value == false)
                    {
                        row.Cells[0].Value = true;
                        systemUser user = Helper.SystemUsergrid.Where(x => x.SystemUserID.Equals(row.Cells[1].Value)).First();
                        user.Select = true;

                    }
                    else
                    {
                        row.Cells[0].Value = !(bool)row.Cells[0].Value;
                        systemUser user = Helper.SystemUsergrid.Where(x => x.SystemUserID.Equals(row.Cells[1].Value)).First();
                        user.Select = false;
                    }
                }
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            base.CloseTool();
        }
    }
}
