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
            if (!string.IsNullOrEmpty(Name.Prefix))
            {
                Name.EntityName = Name.Prefix + "." + Name.EntityName;
            }
            if (!string.IsNullOrEmpty(Description.Prefix))
            {
                Description.EntityName = Description.Prefix + "." + Description.EntityName;
            }
            if (!string.IsNullOrEmpty(Price.Prefix))
            {
                Price.EntityName = Price.Prefix + "." + Price.EntityName;
            }
            if (!string.IsNullOrEmpty(Summary.Prefix))
            {
                Summary.EntityName = Summary.Prefix + "." + Summary.EntityName;
            }
            if (!string.IsNullOrEmpty(Image.Prefix))
            {
                Image.EntityName = Image.Prefix + "." + Image.EntityName;
            }
        }
    }
}
