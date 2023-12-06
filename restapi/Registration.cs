using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace restapi
{
    public class Registration
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public double? Weight { get; set; }
        List<Passanger> Passangers { get; set; } = new();
    }
}
