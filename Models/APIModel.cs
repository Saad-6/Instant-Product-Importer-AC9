namespace TPIPlugin.Models
{
    public class APIModel
    {
        public string ApiUrl { get; set; } 
        public string View { get; set; }
        public ApiResponse ApiResponse { get; set; }
        

    }
    public class ApiResponse
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string Parameter { get; set; }
    }
    public class MappedResponse
    {
        public JsonEntity Name { get; set; }
        public JsonEntity Description { get; set; }
        public JsonEntity Price { get; set; }
        public JsonEntity Summary { get; set; }
        public JsonEntity Image { get; set; }

        public void AddPrefixes()
        {
            foreach (var property in this.GetType().GetProperties())
            {
                    JsonEntity entity = (JsonEntity)property.GetValue(this);
                    if (entity != null && !string.IsNullOrEmpty(entity.Prefix))
                    {
                        entity.EntityName = entity.Prefix + "." + entity.EntityName;
                    }               
            }
        }
    }
}
