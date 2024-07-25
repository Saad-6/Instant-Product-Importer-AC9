using System.Collections.Generic;

namespace TPIPlugin
{
    public class TvMaze
    {
        public double? score { get; set; }
        public Show show { get; set; }
    }
    public class Show
    {
        public int? id { get; set; }
        public string url { get; set; }
        public string name { get; set; }
        public string language { get; set; }
        public List<string> genres { get; set; }
        public int? runtime { get; set; }
        public string premiered { get; set; }
        public Rating rating { get; set; }
        public Image image { get; set; }
        public string summary { get; set; }

    }
    public class Image
    {
        public string medium { get; set; }
        public string original { get; set; }
    }
    public class Rating
    {
        public double? average { get; set; }
    }
}
