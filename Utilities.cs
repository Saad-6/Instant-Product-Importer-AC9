using Newtonsoft.Json.Linq;
using System;
using TPIPlugin.Models;

namespace TPIPlugin
{
    public static class Utilities
    {
        public static MappedResponse GetMappedResponse(APIModel apiModel,JObject jObject)
        {
            MappedResponse mappedResponse = new MappedResponse();
            mappedResponse.Name = new JsonEntity
            {
                EntityName = apiModel.ApiResponse?.Name,
                Prefix = FindNamePropertyPrefix(jObject, "", apiModel.ApiResponse?.Name)
            };
            mappedResponse.Price = new JsonEntity
            {
                EntityName = apiModel.ApiResponse?.Price,
                Prefix = FindNamePropertyPrefix(jObject, "", apiModel.ApiResponse?.Price)
            };
            mappedResponse.Description = new JsonEntity
            {
                EntityName = apiModel.ApiResponse?.Description,
                Prefix = FindNamePropertyPrefix(jObject, "", apiModel.ApiResponse?.Description)
            };
            mappedResponse.Summary = new JsonEntity
            {
                EntityName = apiModel.ApiResponse?.Summary,
                Prefix = FindNamePropertyPrefix(jObject, "", apiModel.ApiResponse?.Summary)
            };
            mappedResponse.Image = new JsonEntity
            {
                EntityName = apiModel.ApiResponse?.Image,
                Prefix = FindNamePropertyPrefix(jObject, "", apiModel.ApiResponse?.Image)
            };
            mappedResponse.AddPrefixes();
            return mappedResponse;
        }
        public static string FindNamePropertyPrefix(JToken token, string currentPath = "", string findBy = "name")
        {
            if (token.Type == JTokenType.Object)
            {
                foreach (var property in token.Children<JProperty>())
                {
                    var newPath = string.IsNullOrEmpty(currentPath) ? property.Name : $"{currentPath}.{property.Name}";
                    if (property.Name.Equals(findBy, StringComparison.OrdinalIgnoreCase))
                    {
                        return currentPath; // Return the path 
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
