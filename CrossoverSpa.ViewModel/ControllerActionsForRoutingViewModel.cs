using CrossoverSpa.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrossoverSpa.ViewModel
{
    public class ControllerActionsForRoutingViewModel:BaseViewModel
    {
        public string ControllerName { get; set; }
        public string ActionName{ get; set; }
        public string DisplayName { get; set; }
    }
}
