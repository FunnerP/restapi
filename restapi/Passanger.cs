using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace restapi
{
    public class Passanger
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int RegID { get; set; }
        public Registration? Registration { get; set; }
    }
}
