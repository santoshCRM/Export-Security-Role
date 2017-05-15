using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
namespace RoleReplicatorControl
{
    internal class systemUser
    {
        private bool _select;

        public bool Select
        {
            get { return _select; }
            set { _select = value; }
        }
        Guid _systemUserID;

        public Guid SystemUserID
        {
            get { return _systemUserID; }
            set { _systemUserID = value; }
        }
        string _fullName;

        public string FullName
        {
            get { return _fullName; }
            set { _fullName = value; }
        }
        string _domainname;

        public string Domainname
        {
            get { return _domainname; }
            set { _domainname = value; }
        }

        private string _businessUnit;

        public string BusinessUnit
        {
            get { return _businessUnit; }
            set { _businessUnit = value; }
        }
        //private EntityReference _businessUnit;

        //public EntityReference BusinessUnit
        //{
        //    get { return _businessUnit; }
        //    set { _businessUnit = value; }
        //}


       

    }
}
