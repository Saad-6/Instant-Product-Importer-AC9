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
    }
}
