using CrossoverSpa.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrossoverSpa.ViewModel
{
    public class RoleFeatureListViewModel : BaseViewModel
    {
        public RoleViewModel Role { get; set; }
        public int RoleId { get; set; }

        public List<FeatureViewModel> Feature { get; set; }
        public int FeatureId { get; set; }
    }
}
