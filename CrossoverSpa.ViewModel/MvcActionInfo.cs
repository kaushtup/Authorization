

namespace CrossoverSpa.ViewModels
{
    public class MvcActionInfo 
    {
        public string ActionId => $"{ControllerId}:{Name}";

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string ControllerId { get; set; }

        public string Description { get; set; }

        public MvcControllerInfo Controller { get; set; }

    }
}
