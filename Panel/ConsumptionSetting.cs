//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Panel
{
    using System;
    using System.Collections.Generic;
    
    public partial class ConsumptionSetting
    {
        public int Id { get; set; }
        public System.DateTime Date { get; set; }
        public string GeneratorName { get; set; }
        public double CurrentConsumption { get; set; }
        public double TestConsumption { get; set; }
        public double StandardConsumption { get; set; }
        public int SN { get; set; }
    }
}
