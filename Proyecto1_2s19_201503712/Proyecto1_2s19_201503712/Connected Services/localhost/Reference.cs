﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Proyecto1_2s19_201503712.localhost {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="localhost.RutasSoap")]
    public interface RutasSoap {
        
        // CODEGEN: Se está generando un contrato de mensaje, ya que el nombre de elemento cadena del espacio de nombres http://tempuri.org/ no está marcado para aceptar valores nil.
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/AnalizarPruebaCQL", ReplyAction="*")]
        Proyecto1_2s19_201503712.localhost.AnalizarPruebaCQLResponse AnalizarPruebaCQL(Proyecto1_2s19_201503712.localhost.AnalizarPruebaCQLRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/AnalizarPruebaCQL", ReplyAction="*")]
        System.Threading.Tasks.Task<Proyecto1_2s19_201503712.localhost.AnalizarPruebaCQLResponse> AnalizarPruebaCQLAsync(Proyecto1_2s19_201503712.localhost.AnalizarPruebaCQLRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class AnalizarPruebaCQLRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="AnalizarPruebaCQL", Namespace="http://tempuri.org/", Order=0)]
        public Proyecto1_2s19_201503712.localhost.AnalizarPruebaCQLRequestBody Body;
        
        public AnalizarPruebaCQLRequest() {
        }
        
        public AnalizarPruebaCQLRequest(Proyecto1_2s19_201503712.localhost.AnalizarPruebaCQLRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class AnalizarPruebaCQLRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string cadena;
        
        public AnalizarPruebaCQLRequestBody() {
        }
        
        public AnalizarPruebaCQLRequestBody(string cadena) {
            this.cadena = cadena;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class AnalizarPruebaCQLResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="AnalizarPruebaCQLResponse", Namespace="http://tempuri.org/", Order=0)]
        public Proyecto1_2s19_201503712.localhost.AnalizarPruebaCQLResponseBody Body;
        
        public AnalizarPruebaCQLResponse() {
        }
        
        public AnalizarPruebaCQLResponse(Proyecto1_2s19_201503712.localhost.AnalizarPruebaCQLResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class AnalizarPruebaCQLResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string AnalizarPruebaCQLResult;
        
        public AnalizarPruebaCQLResponseBody() {
        }
        
        public AnalizarPruebaCQLResponseBody(string AnalizarPruebaCQLResult) {
            this.AnalizarPruebaCQLResult = AnalizarPruebaCQLResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface RutasSoapChannel : Proyecto1_2s19_201503712.localhost.RutasSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class RutasSoapClient : System.ServiceModel.ClientBase<Proyecto1_2s19_201503712.localhost.RutasSoap>, Proyecto1_2s19_201503712.localhost.RutasSoap {
        
        public RutasSoapClient() {
        }
        
        public RutasSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public RutasSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public RutasSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public RutasSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Proyecto1_2s19_201503712.localhost.AnalizarPruebaCQLResponse Proyecto1_2s19_201503712.localhost.RutasSoap.AnalizarPruebaCQL(Proyecto1_2s19_201503712.localhost.AnalizarPruebaCQLRequest request) {
            return base.Channel.AnalizarPruebaCQL(request);
        }
        
        public string AnalizarPruebaCQL(string cadena) {
            Proyecto1_2s19_201503712.localhost.AnalizarPruebaCQLRequest inValue = new Proyecto1_2s19_201503712.localhost.AnalizarPruebaCQLRequest();
            inValue.Body = new Proyecto1_2s19_201503712.localhost.AnalizarPruebaCQLRequestBody();
            inValue.Body.cadena = cadena;
            Proyecto1_2s19_201503712.localhost.AnalizarPruebaCQLResponse retVal = ((Proyecto1_2s19_201503712.localhost.RutasSoap)(this)).AnalizarPruebaCQL(inValue);
            return retVal.Body.AnalizarPruebaCQLResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<Proyecto1_2s19_201503712.localhost.AnalizarPruebaCQLResponse> Proyecto1_2s19_201503712.localhost.RutasSoap.AnalizarPruebaCQLAsync(Proyecto1_2s19_201503712.localhost.AnalizarPruebaCQLRequest request) {
            return base.Channel.AnalizarPruebaCQLAsync(request);
        }
        
        public System.Threading.Tasks.Task<Proyecto1_2s19_201503712.localhost.AnalizarPruebaCQLResponse> AnalizarPruebaCQLAsync(string cadena) {
            Proyecto1_2s19_201503712.localhost.AnalizarPruebaCQLRequest inValue = new Proyecto1_2s19_201503712.localhost.AnalizarPruebaCQLRequest();
            inValue.Body = new Proyecto1_2s19_201503712.localhost.AnalizarPruebaCQLRequestBody();
            inValue.Body.cadena = cadena;
            return ((Proyecto1_2s19_201503712.localhost.RutasSoap)(this)).AnalizarPruebaCQLAsync(inValue);
        }
    }
}
