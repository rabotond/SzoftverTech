using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ReklamServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IServiceReklam
    {

        [OperationContract]
        bool ReklamEmail(string adat);


    }

    [DataContract]
    public class CompositeType
    {
        string adat;

        [DataMember]
        public string Adat
        {
            get { return adat; }
            set { adat = value; }
        }
    }
}
