using System;
using System.Collections;
using System.Collections.Generic;
using CrossoverSpa.ViewModels;
using Microsoft.AspNetCore.Http;

namespace CrossoverSpa.Core.Services
{
    public interface IFeatureDiscovery
    {

           Dictionary<string,List<string>> GetFeatures();

    }
}
