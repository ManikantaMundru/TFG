using System.ComponentModel.DataAnnotations;

namespace TFG.Assessment.Domain.Entities
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
