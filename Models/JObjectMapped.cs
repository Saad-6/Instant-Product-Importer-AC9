using Newtonsoft.Json.Linq;

namespace TPIPlugin.Models
{

    public class JObjectMapped
    {
        public JObject JsonObject { get; set; }
        public MappedResponse MappedResponse { get; set; }
    }
    
}
