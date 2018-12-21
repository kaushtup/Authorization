using CrossoverSpa.ViewModels;
using System.Collections.Generic;

namespace CrossoverSpa.ViewModel
{
    public class RoleFeatureViewModel: BaseViewModel
    {
        public RoleViewModel Role { get; set; }
        public int RoleId { get; set; }

        public FeatureViewModel Feature { get; set; }
        public int FeatureId { get; set; }

       
    }
}
