using Microsoft.ServiceFabric.Services.Remoting;
using ServiceHelper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemPattern.Model;

namespace ServiceHelper.ServiceContract
{
    public interface IServiceHelper : IService
    {
        Task<IList<IObject>> InvokeService(ServiceHelperModel serviceModel);
    }
}
