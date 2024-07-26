using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPIPlugin.Models
{
    public class ResponseViewModel
    {
        public JArray JArray { get; set; }
        public ApiResponse ApiResponse { get; set; }
    }
}
