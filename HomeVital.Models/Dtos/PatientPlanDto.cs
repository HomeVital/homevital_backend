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
        public int TeamID { get; set; } // ID of the team assigned to the patient plan

        public bool IsActive => EndDate == null || EndDate > DateTime.UtcNow;

        public int[] WeightMeasurementDays { get; set; } = new int[7]; // 0 = no measurement, 1 = measurement
        public int[] BloodSugarMeasurementDays { get; set; } = new int[7]; // 0 = no measurement, 1 = measurement
        public int[] BloodPressureMeasurementDays { get; set; } = new int[7]; // 0 = no measurement, 1 = measurement
        public int[] OxygenSaturationMeasurementDays { get; set; } = new int[7]; // 0 = no measurement, 1 = measurement
        public int[] BodyTemperatureMeasurementDays { get; set; } = new int[7]; // 0 = no measurement, 1 = measurement
        
        
        }

    
}