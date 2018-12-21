
using CrossoverSpa.ViewModels;
using System.Collections.Generic;


namespace CrossoverSpa.Core.Services
{
    public interface IMvcControllerDiscovery
    {
        IEnumerable<MvcModuleInfo> GetControllers();
    }
}
