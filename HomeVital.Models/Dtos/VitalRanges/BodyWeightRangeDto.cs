namespace HomeVital.Models.Dtos
{
    public class BodyWeightRangeDto
    {
        public int Id { get; set; }
        public int PatientID { get; set; }
        
        // Body Weight

        // 30 day weight loss fluctuation % range 
        public double WeightLossFluctuationPercentageGood { get; set; } = 5.0; // 0.0

        // % loss/gain from last weight
        public double WeightGainPercentageGoodMax { get; set; } = 5.0; // 0.0

        // 30 day weight gain fluctuation % range 
        public double WeightGainFluctuationPercentageGood { get; set; } = 5.0; 
    }
}