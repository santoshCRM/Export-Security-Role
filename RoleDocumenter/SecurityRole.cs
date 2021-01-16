using Microsoft.Xrm.Sdk;
using System;

namespace RoleDocumenter
{
    internal class SecurityRole
    {
        Guid _roldId;

        public Guid RoldId { get => _roldId; set => _roldId = value; }

        string _name;

        public string Name { get => _name; set => _name = value; }

        private EntityReference _businessUnit;

        public EntityReference BusinessUnit { get => _businessUnit; set => _businessUnit = value; }
    }
}
