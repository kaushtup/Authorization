using System.Collections.Generic;

namespace CrossoverSpa.ViewModels
{
    public class MvcModuleInfo
    {
        public string Id => $"{Name}";

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public List<MvcControllerInfo> Controllers { get; set; }

       
    }
}
