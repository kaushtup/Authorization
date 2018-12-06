using System.Collections.Generic;

namespace CrossoverSpa.ViewModels
{
    public class MvcControllerInfo 
    {
        public string Id => $"{AreaName}:{Name}";

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string AreaName { get; set; }
        
        public string Description { get; set; }

        public string ModuleName { get; set; }
        
        public List<MvcActionInfo> Actions { get; set; }
    }
}
