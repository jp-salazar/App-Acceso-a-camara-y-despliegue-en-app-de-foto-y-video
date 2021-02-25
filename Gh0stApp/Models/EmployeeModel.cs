using System.ComponentModel.DataAnnotations;

namespace Gh0stApp.Models
{
    public class EmployeeModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
