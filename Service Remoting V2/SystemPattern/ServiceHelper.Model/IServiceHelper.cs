using Microsoft.ServiceFabric.Services.Remoting;
using Microsoft.ServiceFabric.Services.Remoting.FabricTransport;
using ServiceHelper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemPattern.Model;

[assembly: FabricTransportServiceRemotingProvider(RemotingListener = RemotingListener.V2Listener, RemotingClient = RemotingClient.V2Client)]
namespace ServiceHelper.ServiceContract
{
    public interface IServiceHelper : IService
    {
        Task<IList<IObject>> InvokeService(ServiceHelperModel serviceModel);
    }
}
