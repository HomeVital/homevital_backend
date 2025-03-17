namespace HomeVital.Models.Entities
{
    public class BodyWeightRange 
    {
        public int Id { get; set; }
        public int PatientID { get; set; }
        // Body Weight
        // 30 day weight loss fluctuation % range
        public double WeightLossFluctuationPercentageGood { get; set; } = 5.0; // 0.0
        public double WeightGainPercentageGoodMax { get; set; } = 5.0; // 0.0
        // 30 day weight gain fluctuation % range
        public double WeightGainFluctuationPercentageGood { get; set; } = 5.0;
    }
}