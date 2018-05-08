using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using ServiceHelper.Model;
using ServiceHelper.ServiceContract;
using SystemPattern.API.Model;
using SystemPattern.Model;
using SystemPattern.ServiceContracts;

namespace SystemPattern.API.Controllers
{
    [Route("api/[controller]")]
    [KnownType(typeof(Product))]
    public class ProductsController : Controller
    {
        private readonly IServiceHelper _service;

        public ProductsController()
        {
            _service = ServiceProxy.Create<IServiceHelper>(
                new Uri("fabric:/SystemPattern/ServiceHelper"),
                new ServicePartitionKey(0));
        }

        [HttpGet]
        public async Task<IList<IObject>> Get()
        {
            try
            {
                ServiceHelperModel serviceHelperModel = new ServiceHelperModel
                {
                    Endpoint = "fabric:/SystemPattern/SystemPattern.ProductCatalog",
                    methodName = "GetAllProducts"
                };
                var allP = await _service.InvokeService(serviceHelperModel);
                return allP;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        [HttpPost]
        public async Task Post([FromBody] ApiProduct product)
        {
            var newProduct = new Product()
            {
                Id = Guid.NewGuid(),
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Availability = 100
            };

            //await _service.AddProduct(newProduct);
        }
    }
}
