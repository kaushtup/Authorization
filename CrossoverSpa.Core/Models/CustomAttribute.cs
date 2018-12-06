using System;

namespace CrossoverSpa.Core.Models
{
    [AttributeUsage(AttributeTargets.All)]
    public class CustomAttribute: Attribute
    {
        private string moduleName;
        private string displayName;
        private string description;

        public CustomAttribute(string moduleName,string displayName,string description)
        {
            this.moduleName = moduleName;
            this.displayName = displayName;
            this.description = description;
        }

        public string ModuleName { get { return moduleName; } }
        public string DisplayName { get { return displayName; } }
        public string Description { get { return description; } }
    }
}
