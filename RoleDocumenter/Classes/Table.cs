using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoleDocumenter
{
    class Table
    {

        public string Name => Entity.DisplayName?.UserLocalizedLabel?.Label ?? Entity.LogicalName;

        public EntityMetadata Entity;

        public Table(EntityMetadata entity)
        {
            Entity = entity;
        }

        public override string ToString()
        {
            return Name == Entity.LogicalName ? Name : Name + " (" + Entity.LogicalName + " )";
        }
    }
}
