using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Data.Collections;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using ServiceHelper.Model;
using ServiceHelper.ServiceContract;
using SystemPattern.Model;
using SystemPattern.ServiceContracts;

namespace ServiceHelper
{
    /// <summary>
    /// An instance of this class is created for each service replica by the Service Fabric runtime.
    /// </summary>
    [KnownType(typeof(Product))]
    internal sealed class ServiceHelper : StatefulService, IServiceHelper
    {
        public ServiceHelper(StatefulServiceContext context)
            : base(context)
        { }

        public async Task<IList<IObject>> InvokeService(ServiceHelperModel serviceModel)
        {
            string uri = serviceModel.Endpoint;
            string method = "IProductCatalogService." + serviceModel.methodName;

            var _catalogService = ServiceProxy.Create<IProductCatalogService>(
               new Uri(uri),
               new ServicePartitionKey(0));


            var methodInfo = _catalogService.GetType().GetRuntimeMethods().Where(m => m.Name == method).FirstOrDefault();
            var resultTask = (Task)methodInfo.Invoke(_catalogService, null);
            await resultTask.ConfigureAwait(false);

            var res = resultTask.GetType().GetProperty("Result");

            var resValue = (IList<IObject>)res.GetValue(resultTask);
            return resValue;
        }

        /// <summary>
        /// Optional override to create listeners (e.g., HTTP, Service Remoting, WCF, etc.) for this service replica to handle client or user requests.
        /// </summary>
        /// <remarks>
        /// For more information on service communication, see https://aka.ms/servicefabricservicecommunication
        /// </remarks>
        /// <returns>A collection of listeners.</returns>
        protected override IEnumerable<ServiceReplicaListener> CreateServiceReplicaListeners()
        {
            //return new[]
            //{
            //    new ServiceReplicaListener(context => this.CreateServiceRemotingListener(context))
            //};
            return this.CreateServiceRemotingReplicaListeners();
        }

        /// <summary>
        /// This is the main entry point for your service replica.
        /// This method executes when this replica of your service becomes primary and has write status.
        /// </summary>
        /// <param name="cancellationToken">Canceled when Service Fabric needs to shut down this service replica.</param>
        protected override async Task RunAsync(CancellationToken cancellationToken)
        {
            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
            }
        }
    }
}
