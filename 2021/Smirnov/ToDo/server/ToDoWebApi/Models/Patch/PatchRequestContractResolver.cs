using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ToDoWebApi.Models.Patch
{
    public class PatchRequestContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);
            property.PropertyName = char.ToLower(property.PropertyName[0]) + property.PropertyName[1..];
            property.SetIsSpecified += (obj, obj1) =>
            {
                if (obj is PatchDtoBase patchDtoBase)
                {
                    patchDtoBase.SetHasProperty(property.PropertyName);
                }
            };

            return property;
        }
    }
}