using System;
using System.Collections.Generic;

namespace HomeVital.Models.Dtos
{
    public class PatientPlanDto
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int PatientID { get; set; }
        public string Instructions { get; set; } = string.Empty;

        public bool IsActive => EndDate == null || EndDate > DateTime.UtcNow;
        public int WeightMeasurementFrequency { get; set; } // Number of times Weight is measured per week
        public int BloodSugarMeasurementFrequency { get; set; } // Number of times Blood Sugar is measured per week
        public int BloodPressureMeasurementFrequency { get; set; } // Number of times Blood Pressure is measured per week
        public int OxygenSaturationMeasurementFrequency { get; set; } // Number of times Oxygen Saturation is measured per week
        public int BodyTemperatureMeasurementFrequency { get; set; } // Number of times Body Temperature is measured per week
        
        }

    
}