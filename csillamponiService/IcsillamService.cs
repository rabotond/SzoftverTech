using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace csillamponiService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IcsillamService" in both code and config file together.
    [ServiceContract]
    public interface IcsillamService
    {
        [OperationContract]
        void DoWork();

        [OperationContract]
         void sendEmail( string to_address, string subject, string bodey );

    }
}
