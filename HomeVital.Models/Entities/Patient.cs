namespace HomeVital.Models.Entities
{
    public class Patient
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int TeamID { get; set; } = 0;
    }
}