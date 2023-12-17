using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Angular_and_Dotnet.Model
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public DateTime DOJ { get; set; }
        public string Gender { get; set; }
        public bool IsActive { get; set; }
        public bool IsMarried { get; set; }
        public Guid DesignationId { get; set; }
        [NotMapped]
        public Designation Designation { get; set; }
    }
}
