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

        public List<MeasurementPlanDto> MeasurementPlans { get; set; } = new List<MeasurementPlanDto>();
    }

    public class MeasurementPlanDto
    {
        public int ID { get; set; }
        public string MeasurementType { get; set; } = string.Empty; // e.g., OxygenSaturation, BloodSugar
        public int TimesPerWeek { get; set; }
        public string MeasurementSchedule { get; set; } = string.Empty; // e.g., Morning, Evening
    }
}