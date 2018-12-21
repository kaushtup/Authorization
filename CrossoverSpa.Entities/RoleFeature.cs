using System;
using System.Collections.Generic;
using System.Text;

namespace CrossoverSpa.Entities
{
    public class RoleFeature : EntityBase
    {

        public Role Role { get; set; }
        public int RoleId { get; set; }


        public Feature Feature { get; set; }
        public int FeatureId { get; set; }

    }
}
