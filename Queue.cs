using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoleReplicatorControl
{
   public class Queue
    {
        Guid _queueId;

        public Guid QueueId
        {
            get { return _queueId; }
            set { _queueId = value; }
        }

        string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
       
    }
}
