using AzureFunctionApp.Infrastructure.CustomAttributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text.Json;

namespace AzureFunctionApp.Infrastructure.Models.Entities.Dataverse
{
    public class AbstractAudit
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? LogicalName { get; set; }
        public string? Name { get; set; }
        public dynamic? ExtensionData { get; set; }

        public static AbstractAudit? CastFrom(object obj)
        {
            if (obj == null) return null;
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };


            string json = JsonConvert.SerializeObject(obj, settings);
            return JsonConvert.DeserializeObject<AbstractAudit>(json);
        }
    }
}
