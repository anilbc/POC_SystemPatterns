using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHelper.Model
{
    public class ServiceHelperModel
    {
        public string Endpoint { get; set; }
        public string ServiceName { get; set; }
        public string methodName { get; set; }
        public List<Parameter> Parameters { get; set; }
    }
}
