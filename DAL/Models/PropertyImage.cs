namespace realstate.DAL.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class PropertyImage : BaseEntity
    {
        [Required]
        [ForeignKey("Property")]
        public int PropertyId { get; set; }

        [Required]
        [StringLength(500)]
        public string ImageUrl { get; set; } = string.Empty;

        [StringLength(200)]
        public string? Caption { get; set; }

        [Required]
        public bool IsPrimary { get; set; } = false;

        [Required]
        public int DisplayOrder { get; set; } = 0;

        [StringLength(50)]
        public string? ImageType { get; set; } // Interior, Exterior, Aerial, etc.

        // Navigation Properties
        public virtual Property Property { get; set; } = null!;
    }

}
