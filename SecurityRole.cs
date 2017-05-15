using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoleReplicatorControl
{
     internal class   SecurityRole
    {

        Guid _roldId;

        public Guid RoldId
        {
            get { return _roldId; }
            set { _roldId = value; }
        }
        string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private EntityReference _businessUnit;

        public EntityReference BusinessUnit
        {
            get { return _businessUnit; }
            set { _businessUnit = value; }
        }

    }
}
