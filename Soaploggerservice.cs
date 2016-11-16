using System;
using System.CodeDom;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Xml;

using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System.IO;

namespace MsCrmTools.Soaplogger
{
   public class SoapLoggerOrganizationService
    {
        public IOrganizationService InnerService;
        public string SoapResult = string.Empty;
        //  private readonly TextWriter OutputWriter;
        public SoapLoggerOrganizationService( IOrganizationService service)
        {
            if (null == service)
            {
                throw new ArgumentNullException("service");
            }
            this.InnerService = service;
        }


        #region IOrganizationService Members
        public void Associate(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities)
        {
            this.ExecuteSoapRequest<AssociateRequest, AssociateResponse>(
                new AssociateRequest(entityName, entityId, relationship, relatedEntities));
        }

        public Guid Create(Entity entity)
        {
            return this.ExecuteSoapRequest<CreateRequest, CreateResponse>(new CreateRequest(entity)).Id;
        }

        public void Delete(string entityName, Guid id)
        {
            this.ExecuteSoapRequest<DeleteRequest, DeleteResponse>(new DeleteRequest(entityName, id));
        }

        public void Disassociate(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities)
        {
            this.ExecuteSoapRequest<DisassociateRequest, DisassociateResponse>(
                new DisassociateRequest(entityName, entityId, relationship, relatedEntities));
        }

        public OrganizationResponse Execute(OrganizationRequest request)
        {
            return this.ExecuteSoapRequest<ExecuteRequest, ExecuteResponse>(new ExecuteRequest(request)).Response;
        }

        public Entity Retrieve(string entityName, Guid id, ColumnSet columnSet)
        {
            return this.ExecuteSoapRequest<RetrieveRequest, RetrieveResponse>(new RetrieveRequest(entityName, id, columnSet)).Entity;
        }

        public EntityCollection RetrieveMultiple(QueryBase query)
        {
            return this.ExecuteSoapRequest<RetrieveMultipleRequest, RetrieveMultipleResponse>(
                new RetrieveMultipleRequest(query)).EntityCollection;
        }

        public void Update(Entity entity)
        {
            this.ExecuteSoapRequest<UpdateRequest, UpdateResponse>(new UpdateRequest(entity));
        }
        #endregion

        #region Private Methods
        private TResponse ExecuteSoapRequest<TRequest, TResponse>(TRequest request)
            where TRequest : RequestBase
            where TResponse : ResponseBase
        {
            OutputSoapRequest(request);

            TResponse response;
            try
            {
                response = (TResponse)request.Execute(this.InnerService);
            }
            catch (FaultException<OrganizationServiceFault> ex)
            {
                this.OutputSoapResponse(new FaultResponse(ex));
                throw;
            }

            this.OutputSoapResponse(response);
            return response;
        }

        private void OutputSoapRequest(RequestBase request)
        {
            SoapResult += System.Environment.NewLine;
            SoapResult += "HTTP REQUEST";
            SoapResult += new string('-', 50);
            SoapResult+= System.Environment.NewLine;
            SoapResult += string.Format("POST {0}/XRMServices/2011/Organization.svc/web'", "Xrm.Page.context.getClientUrl()+'");//RootServiceUri
            SoapResult += System.Environment.NewLine;
            SoapResult += "Content-Type: text/xml; charset=utf-8";
            SoapResult += System.Environment.NewLine;
            SoapResult += string.Format("SOAPAction: {0}", request.SoapAction);
            SoapResult += System.Environment.NewLine;
            this.OutputSoapEnvelope(request);
            SoapResult += System.Environment.NewLine;
            SoapResult += (new string('-', 50));
        }

        private void OutputSoapResponse(object response)
        {
            SoapResult += System.Environment.NewLine;
            SoapResult += "HTTP RESPONSE";
            SoapResult += new string('-', 50);
            SoapResult += System.Environment.NewLine;
            this.OutputSoapEnvelope(response);
            SoapResult += System.Environment.NewLine;
            SoapResult += new string('-', 50);
        }

        private void OutputSoapEnvelope(object value)
        {
            SoapResult += FormatXml(string.Format(CultureInfo.InvariantCulture,
                @"<s:Envelope xmlns:s=""http://schemas.xmlsoap.org/soap/envelope/""><s:Body>{0}</s:Body></s:Envelope>",
                Serialize(value)));
        }

        private string FormatXml(string xml)
        {
            using (StringReader stringReader = new StringReader(xml))
            {
                XmlReaderSettings readerSettings = new XmlReaderSettings();
                readerSettings.IgnoreWhitespace = true;
                readerSettings.ConformanceLevel = ConformanceLevel.Fragment;

                XmlDocument doc = new XmlDocument();
                using (XmlReader reader = XmlReader.Create(stringReader, readerSettings))
                {
                    doc.XmlResolver = null;
                    doc.Load(reader);
                }

                XmlWriterSettings writerSettings = new XmlWriterSettings();
                writerSettings.Indent = true;
                writerSettings.OmitXmlDeclaration = true;

                using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
                {
                    using (XmlWriter writer = XmlWriter.Create(stringWriter, writerSettings))
                    {
                        doc.Save(writer);
                    }

                    return stringWriter.ToString();
                }
            }
        }

        private string Serialize(object value)
        {
            if (null == value)
            {
                return null;
            }

            using (MemoryStream stream = new MemoryStream())
            {
                DataContractSerializer serializer = new DataContractSerializer(value.GetType(), null, int.MaxValue, true, false,
                    new StrongToLooseTypeSurrogate(), new KnownTypesResolver());
                serializer.WriteObject(stream, value);
                stream.Seek(0, SeekOrigin.Begin);

                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
            return null;
        }
        #endregion

        #region Private Classes
        private sealed class StrongToLooseTypeSurrogate : IDataContractSurrogate
        {
            #region IDataContractSurrogate Members
            public object GetCustomDataToExport(Type clrType, Type dataContractType)
            {
                return null;
            }

            public object GetCustomDataToExport(System.Reflection.MemberInfo memberInfo, Type dataContractType)
            {
                return null;
            }

            public Type GetDataContractType(Type type)
            {
                return type;
            }

            public object GetDeserializedObject(object obj, Type targetType)
            {
                return obj;
            }

            public void GetKnownCustomDataTypes(Collection<Type> customDataTypes)
            {
                return;
            }

            public object GetObjectToSerialize(object obj, Type targetType)
            {
                if (null != obj && typeof(Entity).IsAssignableFrom(obj.GetType()))
                {
                    return ((Entity)obj).ToEntity<Entity>();
                }

                return obj;
            }

            public Type GetReferencedTypeOnImport(string typeName, string typeNamespace, object customData)
            {
                return null;
            }

            public CodeTypeDeclaration ProcessImportedType(CodeTypeDeclaration typeDeclaration, CodeCompileUnit compileUnit)
            {
                return typeDeclaration;
            }
            #endregion
        }

        [DataContract]
        private abstract class RequestBase
        {
            private const string SoapActionPrefix = "http://schemas.microsoft.com/xrm/2011/Contracts/Services/IOrganizationService/";

            protected RequestBase()
            {
                DataContractAttribute[] attributes = (DataContractAttribute[])this.GetType().GetCustomAttributes(
                    typeof(DataContractAttribute), true);
                if (null == attributes || 0 == attributes.Length)
                {
                    this.SoapAction = null;
                    return;
                }

                this.SoapAction = SoapActionPrefix + attributes[0].Name;
            }

            public string SoapAction { get; private set; }

            public abstract ResponseBase Execute(IOrganizationService service);
        }

        [DataContract]
        private abstract class ResponseBase
        {
        }

        [DataContract(Name = "Fault", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
        private sealed class FaultResponse : ResponseBase
        {
            #region Constructors
            public FaultResponse()
            {
            }

            public FaultResponse(FaultException<OrganizationServiceFault> exception)
            {
                if (null == exception)
                {
                    throw new ArgumentNullException("exception");
                }

                this.FaultCode = new FaultCode(exception.Code);
                this.FaultString = new FaultReason(exception.Message);
                this.Detail = exception.Detail;
            }
            #endregion

            #region Properties
            [DataMember(Name = "Code", Order = 1)]
            public FaultCode FaultCode { get; private set; }

            [DataMember(Name = "Reason", Order = 2)]
            public FaultReason FaultString { get; private set; }

            [DataMember(Name = "Detail", Order = 3)]
            public OrganizationServiceFault Detail { get; private set; }
            #endregion
        }

        [DataContract(Namespace = "http://www.w3.org/2003/05/soap-envelope")]
        private sealed class FaultCode : ResponseBase
        {
            #region Constructors
            public FaultCode()
            {
            }

            public FaultCode(System.ServiceModel.FaultCode code)
            {
                string ns = null;
                if (!string.IsNullOrWhiteSpace(code.Namespace))
                {
                    ns = code.Namespace + ":";
                }

                this.Value = ns + code.Name;
            }
            #endregion

            #region Properties
            [DataMember(Name = "Value", Order = 1)]
            public string Value { get; private set; }
            #endregion
        }

        [DataContract(Namespace = "http://www.w3.org/2003/05/soap-envelope")]
        private sealed class FaultReason : ResponseBase
        {
            #region Constructors
            public FaultReason()
            {
            }

            public FaultReason(string text)
            {
                this.Text = text;
            }
            #endregion

            #region Properties
            [DataMember(Name = "Text", Order = 1)]
            public string Text { get; private set; }
            #endregion
        }

        [DataContract(Name = "Associate", Namespace = "http://schemas.microsoft.com/xrm/2011/Contracts/Services")]
        private sealed class AssociateRequest : RequestBase
        {
            public AssociateRequest(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities)
            {
                this.EntityName = entityName;
                this.EntityId = entityId;
                this.Relationship = relationship;
                this.RelatedEntities = relatedEntities;
            }

            #region Properties
            [DataMember(Name = "entityName", Order = 1)]
            public string EntityName { get; private set; }

            [DataMember(Name = "entityId", Order = 2)]
            public Guid EntityId { get; private set; }

            [DataMember(Name = "relationship", Order = 3)]
            public Relationship Relationship { get; private set; }

            [DataMember(Name = "relatedEntities", Order = 4)]
            public EntityReferenceCollection RelatedEntities { get; private set; }
            #endregion

            #region Methods
            public override ResponseBase Execute(IOrganizationService service)
            {
                service.Associate(this.EntityName, this.EntityId, this.Relationship, this.RelatedEntities);
                return new AssociateResponse();
            }
            #endregion
        }

        [DataContract(Name = "AssociateResponse", Namespace = "http://schemas.microsoft.com/xrm/2011/Contracts/Services")]

        private sealed class AssociateResponse : ResponseBase
        {
        }

        [DataContract(Name = "Disassociate", Namespace = "http://schemas.microsoft.com/xrm/2011/Contracts/Services")]
        private sealed class DisassociateRequest : RequestBase
        {
            public DisassociateRequest(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities)
            {
                this.EntityName = entityName;
                this.EntityId = entityId;
                this.Relationship = relationship;
                this.RelatedEntities = relatedEntities;
            }

            #region Properties
            [DataMember(Name = "entityName", Order = 1)]
            public string EntityName { get; private set; }

            [DataMember(Name = "entityId", Order = 2)]
            public Guid EntityId { get; private set; }

            [DataMember(Name = "relationship", Order = 3)]
            public Relationship Relationship { get; private set; }

            [DataMember(Name = "relatedEntities", Order = 4)]
            public EntityReferenceCollection RelatedEntities { get; private set; }
            #endregion

            #region Methods
            public override ResponseBase Execute(IOrganizationService service)
            {
                service.Disassociate(this.EntityName, this.EntityId, this.Relationship, this.RelatedEntities);
                return new DisassociateResponse();
            }
            #endregion
        }

        [DataContract(Name = "DisassociateResponse", Namespace = "http://schemas.microsoft.com/xrm/2011/Contracts/Services")]
        private sealed class DisassociateResponse : ResponseBase
        {
        }

        [DataContract(Name = "Create", Namespace = "http://schemas.microsoft.com/xrm/2011/Contracts/Services")]
        private sealed class CreateRequest : RequestBase
        {
            public CreateRequest(Entity entity)
            {
                this.Entity = entity;
            }

            #region Properties
            [DataMember(Name = "entity", Order = 1)]
            public Entity Entity { get; private set; }
            #endregion

            #region Methods
            public override ResponseBase Execute(IOrganizationService service)
            {
                return new CreateResponse(service.Create(this.Entity));
            }
            #endregion
        }

        [DataContract(Name = "CreateResponse", Namespace = "http://schemas.microsoft.com/xrm/2011/Contracts/Services")]
        private sealed class CreateResponse : ResponseBase
        {
            #region Constructors
            public CreateResponse()
            {
            }

            public CreateResponse(Guid id)
            {
                this.Id = id;
            }
            #endregion

            #region Properties
            [DataMember(Name = "CreateResult", Order = 1)]
            public Guid Id { get; private set; }
            #endregion
        }

        [DataContract(Name = "Delete", Namespace = "http://schemas.microsoft.com/xrm/2011/Contracts/Services")]
        private sealed class DeleteRequest : RequestBase
        {
            public DeleteRequest(string entityName, Guid id)
            {
                this.EntityName = entityName;
                this.EntityId = id;
            }

            #region Properties
            [DataMember(Name = "entityName", Order = 1)]
            public string EntityName { get; private set; }

            [DataMember(Name = "id", Order = 2)]
            public Guid EntityId { get; private set; }
            #endregion

            #region Methods
            public override ResponseBase Execute(IOrganizationService service)
            {
                service.Delete(this.EntityName, this.EntityId);
                return new DeleteResponse();
            }
            #endregion
        }

        [DataContract(Name = "DeleteResponse", Namespace = "http://schemas.microsoft.com/xrm/2011/Contracts/Services")]
        private sealed class DeleteResponse : ResponseBase
        {
        }

        [DataContract(Name = "Execute", Namespace = "http://schemas.microsoft.com/xrm/2011/Contracts/Services")]
        private sealed class ExecuteRequest : RequestBase
        {
            public ExecuteRequest(OrganizationRequest request)
            {
                this.Request = request;
            }

            #region Properties
            [DataMember(Name = "request", Order = 1)]
            public OrganizationRequest Request { get; private set; }
            #endregion

            #region Methods
            public override ResponseBase Execute(IOrganizationService service)
            {
                return new ExecuteResponse(service.Execute(this.Request));
            }
            #endregion
        }

        [DataContract(Name = "ExecuteResponse", Namespace = "http://schemas.microsoft.com/xrm/2011/Contracts/Services")]
        private sealed class ExecuteResponse : ResponseBase
        {
            #region Constructors
            public ExecuteResponse()
            {
            }

            public ExecuteResponse(OrganizationResponse response)
            {
                this.Response = response;
            }
            #endregion

            #region Properties
            [DataMember(Name = "ExecuteResult", Order = 1)]
            public OrganizationResponse Response { get; private set; }
            #endregion
        }

        [DataContract(Name = "Retrieve", Namespace = "http://schemas.microsoft.com/xrm/2011/Contracts/Services")]
        private sealed class RetrieveRequest : RequestBase
        {
            public RetrieveRequest(string entityName, Guid id, ColumnSet columnSet)
            {
                this.EntityName = entityName;
                this.Id = id;
                this.Columns = columnSet;
            }

            #region Properties
            [DataMember(Name = "entityName", Order = 1)]
            public string EntityName { get; private set; }

            [DataMember(Name = "id", Order = 2)]
            public Guid Id { get; private set; }

            [DataMember(Name = "columnSet", Order = 3)]
            public ColumnSet Columns { get; private set; }
            #endregion

            #region Methods
            public override ResponseBase Execute(IOrganizationService service)
            {
                return new RetrieveResponse(service.Retrieve(this.EntityName, this.Id, this.Columns));
            }
            #endregion
        }

        [DataContract(Name = "RetrieveResponse", Namespace = "http://schemas.microsoft.com/xrm/2011/Contracts/Services")]
        private sealed class RetrieveResponse : ResponseBase
        {
            #region Constructors
            public RetrieveResponse()
            {
            }

            public RetrieveResponse(Entity entity)
            {
                this.Entity = entity;
            }
            #endregion

            #region Properties
            [DataMember(Name = "RetrieveResult", Order = 1)]
            public Entity Entity { get; private set; }
            #endregion
        }

        [DataContract(Name = "RetrieveMultiple", Namespace = "http://schemas.microsoft.com/xrm/2011/Contracts/Services")]
        private sealed class RetrieveMultipleRequest : RequestBase
        {
            public RetrieveMultipleRequest(QueryBase query)
            {
                this.Query = query;
            }

            #region Properties
            [DataMember(Name = "query", Order = 1)]
            public QueryBase Query { get; private set; }
            #endregion

            #region Methods
            public override ResponseBase Execute(IOrganizationService service)
            {
                return new RetrieveMultipleResponse(service.RetrieveMultiple(this.Query));
            }
            #endregion
        }

        [DataContract(Name = "RetrieveMultipleResponse", Namespace = "http://schemas.microsoft.com/xrm/2011/Contracts/Services")]
        private sealed class RetrieveMultipleResponse : ResponseBase
        {
            #region Constructors
            public RetrieveMultipleResponse()
            {
            }

            public RetrieveMultipleResponse(EntityCollection results)
            {
                this.EntityCollection = results;
            }
            #endregion

            #region Properties
            [DataMember(Name = "RetrieveMultipleResult", Order = 1)]
            public EntityCollection EntityCollection { get; private set; }
            #endregion
        }

        [DataContract(Name = "Update", Namespace = "http://schemas.microsoft.com/xrm/2011/Contracts/Services")]
        private sealed class UpdateRequest : RequestBase
        {
            public UpdateRequest(Entity entity)
            {
                this.Entity = entity;
            }

            #region Properties
            [DataMember(Name = "entity", Order = 1)]
            public Entity Entity { get; private set; }
            #endregion

            #region Methods
            public override ResponseBase Execute(IOrganizationService service)
            {
                service.Update(this.Entity);
                return new UpdateResponse();
            }
            #endregion
        }

        [DataContract(Name = "UpdateResponse", Namespace = "http://schemas.microsoft.com/xrm/2011/Contracts/Services")]
        private sealed class UpdateResponse : ResponseBase
        {
        }
        #endregion
    }
}
