﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AdatKezelő.csillamService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="csillamService.IcsillamService")]
    public interface IcsillamService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IcsillamService/DoWork", ReplyAction="http://tempuri.org/IcsillamService/DoWorkResponse")]
        void DoWork();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IcsillamService/DoWork", ReplyAction="http://tempuri.org/IcsillamService/DoWorkResponse")]
        System.Threading.Tasks.Task DoWorkAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IcsillamService/SendMail", ReplyAction="http://tempuri.org/IcsillamService/SendMailResponse")]
        bool SendMail(string toaddress, string subject, string body);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IcsillamService/SendMail", ReplyAction="http://tempuri.org/IcsillamService/SendMailResponse")]
        System.Threading.Tasks.Task<bool> SendMailAsync(string toaddress, string subject, string body);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IcsillamServiceChannel : AdatKezelő.csillamService.IcsillamService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class IcsillamServiceClient : System.ServiceModel.ClientBase<AdatKezelő.csillamService.IcsillamService>, AdatKezelő.csillamService.IcsillamService {
        
        public IcsillamServiceClient() {
        }
        
        public IcsillamServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public IcsillamServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public IcsillamServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public IcsillamServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void DoWork() {
            base.Channel.DoWork();
        }
        
        public System.Threading.Tasks.Task DoWorkAsync() {
            return base.Channel.DoWorkAsync();
        }
        
        public bool SendMail(string toaddress, string subject, string body) {
            return base.Channel.SendMail(toaddress, subject, body);
        }
        
        public System.Threading.Tasks.Task<bool> SendMailAsync(string toaddress, string subject, string body) {
            return base.Channel.SendMailAsync(toaddress, subject, body);
        }
    }
}
