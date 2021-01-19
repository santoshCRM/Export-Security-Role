using RoleDocumenter.Properties;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace RoleDocumenter
{
    internal class MiscPriv
    {
        public MiscPriv(string name, string display)
        {
            Name = name;
            Display = display;
        }

        public string Display { get; set; }

        public string Name { get; set; }
    }

    internal class MisPrivilegeSet : Privilege
    {
        public MisPrivilegeSet(string name) : this(name, name)
        {
        }

        public MisPrivilegeSet(string name, string display) : base(name, display)
        {
            Role = Resources.organization;
            Role.Tag = "Organization";
        }

        public Image Role { get; set; }
    }

    class PrivilegeComparer : IComparer<Privilege>
    {
        SortOrder sortOrder = SortOrder.None;

        public PrivilegeComparer(SortOrder sortingOrder) { sortOrder = sortingOrder; }

        public int Compare(Privilege x, Privilege y)
        {
            int returnValue = 1;

            if (sortOrder == SortOrder.Ascending)
                returnValue = x.DisplayName.CompareTo(y.DisplayName);
            else
                returnValue = y.DisplayName.CompareTo(x.DisplayName);

            return returnValue;
        }
    }

    class Privilege
    {
        public Privilege(string name, string displayName)
        {
            Name = name;
            DisplayName = displayName;
        }

        public string DisplayName { get; set; }

        public string Name { get; set; }
    }

    internal class PrivilegeSet : Privilege
    {
        public PrivilegeSet(string logicalName) : this(logicalName, logicalName)
        {
        }

        public PrivilegeSet(string logicalName, string displayName) : base(logicalName, displayName)
        {
            //  DisplayName = displayName;
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

        public Image Append { get; set; }

        public Image AppendTo { get; set; }

        public Image Assign { get; set; }

        // public string LogicalName { get; set; }
        public Image Create { get; set; }

        public Image Delete { get; set; }

        public Image Read { get; set; }

        public Image Share { get; set; }

        public Image Write { get; set; }
    }

    internal class TablePrivSet
    {
        public string TableName { get; set; }

        public List<PrivilegeSet> Privileges = new List<PrivilegeSet>();

        public TablePrivSet(string tableName)
        {
            TableName = tableName;
        }
    }
    internal class SecurityTab
    {
        public List<string> miscPrivs { get; set; }

        public List<string> tables { get; set; }

        public string TabName { get; set; }
    }

}
