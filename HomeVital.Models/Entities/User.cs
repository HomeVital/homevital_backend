
using System.Diagnostics.CodeAnalysis;

namespace HomeVital.Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set;} = "";
        // TODO: Klára að define-a. Dummy namespace til að koma í veg fyrir compile error
        public string Kennitala { get; set; } = string.Empty;
    }
}
