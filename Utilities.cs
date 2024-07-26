using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPIPlugin
{
    public static class Utilities
    {
        public static string FindNamePropertyPrefix(JToken token, string currentPath = "",string findBy = "name")
        {
            if (token.Type == JTokenType.Object)
            {
                foreach (var property in token.Children<JProperty>())
                {
                    var newPath = string.IsNullOrEmpty(currentPath) ? property.Name : $"{currentPath}.{property.Name}";
                    if (property.Name.Equals(findBy, StringComparison.OrdinalIgnoreCase))
                    {
                        return currentPath; // Return the path without appending 'name'
                    }
                    var result = FindNamePropertyPrefix(property.Value, newPath, findBy);
                    if (result != null)
                    {
                        return result;
                    }
                }
            }
            else if (token.Type == JTokenType.Array)
            {
                foreach (var item in token.Children())
                {
                    var result = FindNamePropertyPrefix(item, currentPath, findBy);
                    if (result != null)
                    {
                        return result;
                    }
                }
            }
            return null;
        }


    }
}
