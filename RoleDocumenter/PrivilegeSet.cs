using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RoleDocumenter.Properties;

namespace RoleDocumenter
{
    class PrivelegeComparer : IComparer<Privilege>
    {
        SortOrder sortOrder = SortOrder.None;
        public PrivelegeComparer(SortOrder sortingOrder)
        {
            sortOrder = sortingOrder;
        }
        public int Compare(Privilege x, Privilege y)
        {
            int returnValue = 1;

            if (sortOrder == SortOrder.Ascending)
            {
                returnValue = x.Name.CompareTo(y.Name);
            }
            else
            {
                returnValue = y.Name.CompareTo(x.Name);
            }

            return returnValue;

        }
    }

    class Privilege
    {
        public string Name { get; set; }

        public Privilege(string name)
        {
            Name = name;
        }
    }
    internal class PrivilegeSet : Privilege
    {
        // public string LogicalName { get; set; }

        public Image Create { get; set; }

        public Image Read { get; set; }

        public Image Write { get; set; }

        public Image Delete { get; set; }

        public Image Append { get; set; }

        public Image AppendTo { get; set; }

        public Image Assign { get; set; }

        public Image Share { get; set; }

        public PrivilegeSet(string name) : base(name)
        {
            Create = Resources.none;
            Create.Tag = "None";
            Read = Resources.none;
            Read.Tag = "None";
            Write = Resources.none;
            Write.Tag = "None";
            Delete = Resources.none;
            Delete.Tag = "None";
            Append = Resources.none;
            Append.Tag = "None";
            AppendTo = Resources.none;
            AppendTo.Tag = "None";
            Assign = Resources.none;
            Assign.Tag = "None";
            Share = Resources.none;
            Share.Tag = "None";

        }




    }

    internal class MisprivilegeSet : Privilege
    {
        public Image Role { get; set; }


        public MisprivilegeSet(string name) : base(name)
        {

            Role = Resources.organization;
            Role.Tag = "Organization";


        }




    }

}
