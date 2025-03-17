namespace HomeVital.Models.InputModels
{
    public class BodyWeightRangeInputModel
    {
        public int PatientID { get; set; }

        // Body Weight
        // 1 30 day weight loss fluctuation % range 
        public double? WeightLossFluctuationPercentageGood { get; set; }
        // 2 normal weight gain % range
        public double? WeightGainPercentageGoodMax { get; set; } 
        // 3 30 day weight gain fluctuation % range 
        public double? WeightGainFluctuationPercentageGood { get; set; } 
       

    }
}