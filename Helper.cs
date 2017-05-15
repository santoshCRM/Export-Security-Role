using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Description;
using Microsoft.Xrm.Sdk.Client;
using System.IO;
using System.Xml;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Crm.Sdk.Messages;
namespace RoleReplicatorControl
{
    public static class Helper
    {

       
        private static List<SecurityRole> _securityRoles;
        private static List<systemUser> _systemUser;
        internal static List<systemUser> SystemUser
        {
            get { return Helper._systemUser; }
            set { Helper._systemUser = value; }
        }
        private static List<systemUser> _systemUsergrid;
        internal static List<systemUser> SystemUsergrid
        {
            get { return Helper._systemUsergrid; }
            set { Helper._systemUsergrid = value; }
        }
        internal static List<SecurityRole> SecurityRoles
        {
            get { return Helper._securityRoles; }
            set { Helper._securityRoles = value; }
        }
        private static List<Team> _teams;
        internal static List<Team> Teams
        {
            get { return Helper._teams; }
            set { Helper._teams = value; }
        }
        private static List<Queue> _queues;

        public static List<Queue> Queues
        {
            get { return Helper._queues; }
            set { Helper._queues = value; }
        }

        private static OrganizationServiceProxy _serviceProxy;
        public static OrganizationServiceProxy ServiceProxy
        {
            get { return Helper._serviceProxy; }
            set { Helper._serviceProxy = value; }
        }
        private static string _OrganizationUri;
        public static string OrganizationUri
        {
            get { return Helper._OrganizationUri; }
            set { Helper._OrganizationUri = value; }
        }
        private static ClientCredentials _Credentials = null;
        public static ClientCredentials Credentials
        {
            get { return Helper._Credentials; }
            set { Helper._Credentials = value; }
        }
        internal static void createConn(IOrganizationService Service)
        {

            _serviceProxy = (OrganizationServiceProxy)Service;
           
        }
        internal static List<systemUser> getUsers()
        {

            SystemUser = new List<systemUser>();
            SystemUsergrid = new List<systemUser>();
            int fetchCount = 5000;// Initialize the page number.
            int pageNumber = 1;// Initialize the number of records.
            string pagingCookie = null;
            string fetchXML = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>  <entity name='systemuser'>    <attribute name='fullname' />    <attribute name='businessunitid' />    <attribute name='domainname' />    <attribute name='systemuserid' />    <order attribute='fullname' descending='false' />    <filter type='and'>      <condition attribute='isdisabled' operator='eq' value='0' />      <condition attribute='accessmode' operator='ne' value='3' />    </filter>  </entity></fetch>";
            while (true)
            {
                // Build fetchXml string with the placeholders.
                string xml = CreateXml(fetchXML, pagingCookie, pageNumber, fetchCount);

                // Excute the fetch query and get the xml result.
                RetrieveMultipleRequest fetchRequest1 = new RetrieveMultipleRequest
                {
                    Query = new FetchExpression(xml)
                };

                EntityCollection returnCollection = ((RetrieveMultipleResponse)_serviceProxy.Execute(fetchRequest1)).EntityCollection;

                foreach (Entity ent in returnCollection.Entities)
                {
                    systemUser user = new systemUser();
                    user.Domainname = ent.Attributes["domainname"].ToString();
                    user.FullName = ent.Attributes["fullname"].ToString();
                    user.BusinessUnit = ((EntityReference)ent.Attributes["businessunitid"]).Name;
                    user.SystemUserID = ent.Id;
                    SystemUser.Add(user);
                    SystemUsergrid.Add(user);
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


            return SystemUser;
        }
        internal static string CreateXml(string xml, string cookie, int page, int count)
        {
            StringReader stringReader = new StringReader(xml);
            XmlTextReader reader = new XmlTextReader(stringReader);

            // Load document
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);

            return CreateXml(doc, cookie, page, count);
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
        internal static List<SecurityRole> getSecurityRole(Guid systemUserId)
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
    + "<link-entity name='systemuserroles' from='roleid' to='roleid' visible='false' intersect='true'>"
      + "<link-entity name='systemuser' from='systemuserid' to='systemuserid' alias='ab'>"
        + "<filter type='and'>"
          + "<condition attribute='systemuserid' operator='eq' uitype='systemuser'  value='{"+systemUserId+"}' />"
        + "</filter>"
      + "</link-entity>"
    + "</link-entity>"
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

                 returnCollection = ((RetrieveMultipleResponse)_serviceProxy.Execute(fetchRequest1)).EntityCollection;
                 foreach (Entity ent in returnCollection.Entities)
                 {
                     SecurityRole SecurityRole = new SecurityRole();

                     SecurityRole.Name = ent.Attributes["name"].ToString();
                     SecurityRole.BusinessUnit = (EntityReference)ent.Attributes["businessunitid"];
                     SecurityRole.RoldId = ent.Id;
                     accessRoles.Add(SecurityRole);
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
        internal static List<Team> getTeambyUser(Guid systemUserId, string buName)
        {
            List<Team> userTeams = new List<Team>();
            int fetchCount = 5000;// Initialize the page number.
            int pageNumber = 1;// Initialize the number of records.
            string pagingCookie = null;
            string fetchXML =
                "<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='true'>"
  + "<entity name='team'>"
    + "<attribute name='name' />"
    + "<attribute name='businessunitid' />"
    + "<attribute name='teamid' />"
    + "<attribute name='teamtype' />"
     + "<filter>"
      + "<condition attribute='name' operator='neq' value='" + buName + "' />"
    + "</filter>"
    + "<order attribute='name' descending='false' />"
    + "<link-entity name='teammembership' from='teamid' to='teamid' visible='false' intersect='true'>"
      + "<link-entity name='systemuser' from='systemuserid' to='systemuserid' alias='ac'>"
        + "<filter type='and'>"
           + "<condition attribute='systemuserid' operator='eq' uitype='systemuser'  value='{" + systemUserId + "}' />"
        + "</filter>"
      + "</link-entity>"
    + "</link-entity>"
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

                EntityCollection returnCollection = ((RetrieveMultipleResponse)_serviceProxy.Execute(fetchRequest1)).EntityCollection;
                foreach (Entity ent in returnCollection.Entities)
                {
                    Team team = new Team();
                    team.Name = ent.Attributes["name"].ToString();
                    team.BusinessUnit = (EntityReference)ent.Attributes["businessunitid"];
                    team.TeamId = ent.Id;
                    userTeams.Add(team);
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


            return userTeams;
        }
        internal static List<Queue> getQueuebyUser(Guid systemUserId, string buName)
        {
            List<Queue> userQueues = new List<Queue>();
            RetrieveUserQueuesRequest userQueuesReq = new RetrieveUserQueuesRequest
            {
                UserId=systemUserId,
                IncludePublic=false,
            };
            RetrieveUserQueuesResponse res = (RetrieveUserQueuesResponse)_serviceProxy.Execute(userQueuesReq);
            foreach (Entity ent in res.EntityCollection.Entities)
            {
                if ( ((OptionSetValue) ent.Attributes["queueviewtype"]).Value.ToString()== "1" && !ent.Attributes["name"].ToString().StartsWith("<"))
                {
                    Queue queue = new Queue();
                    queue.Name = ent.Attributes["name"].ToString();
                    queue.QueueId = ent.Id;
                    userQueues.Add(queue);
                }
            }
            return userQueues;
        }

        internal static void copyRole(Guid[]  toUserID,string[] BUs,bool role,bool team,bool queue)
        {
           
            if (role)
            {
                removeuserRole(toUserID, BUs);
                adduserRole(toUserID);
            }
            if (team)
            {
                removeuserTeam(toUserID, BUs);
                adduserTeam(toUserID);
            }
            if (queue)
            {
                removeuserQueue(toUserID, BUs);
                adduserQueue(toUserID);
            }
        }
        internal static void removeuserRole(Guid[] toUserID, string[] BUs)
        {
            for (int x = 0; x < toUserID.Count(); x++)
            {
                Guid userid = toUserID[x];
                List<SecurityRole> userRoles = getSecurityRole(userid);
                foreach (SecurityRole role in userRoles)
                {
                    _serviceProxy.Disassociate(
                  "systemuser",
                  userid,
                  new Relationship("systemuserroles_association"),
                  new EntityReferenceCollection() { new EntityReference("role", role.RoldId) });

                }

             
            }
        }
        private static void adduserRole(Guid[] toUserID)
        {
            foreach (SecurityRole role in Helper.SecurityRoles)
            {
                if (role.RoldId != Guid.Empty)
                {
                    foreach (Guid userid in toUserID)
                    {
                        _serviceProxy.Associate(
                      "systemuser",
                      userid,
                      new Relationship("systemuserroles_association"),
                      new EntityReferenceCollection() { new EntityReference("role", role.RoldId) });

                    }


                }

            }
           
        }

        internal static void removeuserTeam(Guid[] toUserID, string[] BUs)
        {
            for (int x = 0; x < toUserID.Count();x++ )
            {
                Guid userid = toUserID[x];
                List<Team> userTeamss = getTeambyUser(userid, BUs[x]);
                foreach (Team team in userTeamss)
                {
                    if (team.TeamId != Guid.Empty)
                    {

                        _serviceProxy.Execute(new RemoveMembersTeamRequest
                        {
                            TeamId = team.TeamId,
                            MemberIds = toUserID
                        });

                    }

                }
            }
        }
        private static void adduserTeam(Guid[] toUserID)
        {
            foreach (Team team in Helper.Teams)
            {
                if (team.TeamId != Guid.Empty || toUserID[0] != Guid.Empty)
                {

                    _serviceProxy.Execute(new AddMembersTeamRequest
                    {
                        TeamId = team.TeamId,
                        MemberIds = toUserID
                    });

                }

            }
        }

        internal static void removeuserQueue(Guid[] toUserID, string[] BUs)
        {
            foreach (Queue queue in Helper.Queues)
            {
                if (queue.QueueId != Guid.Empty || toUserID[0] != Guid.Empty)
                {
                   


                }

            }
        }
        private static void adduserQueue(Guid[] toUserID)
        {
            try
            {
                foreach (Queue queue in Helper.Queues)
                {

                    foreach (Guid userid in toUserID)
                    {
                        //AddPrincipalToQueueRequest addPrincipalToQueueRequest = new AddPrincipalToQueueRequest
                        //{
                        //    Principal = new Entity("systemuser", userid),
                        //    QueueId = queue.QueueId
                        //};
                        //AddPrincipalToQueueResponse res = (AddPrincipalToQueueResponse)_serviceProxy.Execute(addPrincipalToQueueRequest);
                    }

                }
            }
            catch(Exception ex)
            {


            }

        }
    }
}
