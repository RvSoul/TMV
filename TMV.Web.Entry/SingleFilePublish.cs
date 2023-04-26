using Furion;
using System.Reflection;

namespace TMV.Web.Entry
{
    public class SingleFilePublish : ISingleFilePublish
    {
        public Assembly[] IncludeAssemblies()
        {
            return Array.Empty<Assembly>();
        }

        public string[] IncludeAssemblyNames()
        {
            return new[]
            {
            "TMV.Application",
            "TMV.Core",
            "TMV.Web.Core"
        };
        }
    }
}