namespace NoiThatAdmin.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Slide")]
    public partial class Slide
    {
        public int SlideID { get; set; }

        [StringLength(100)]
        public string ImageURL { get; set; }

        [StringLength(100)]
        public string Slogun1 { get; set; }

        [StringLength(100)]
        public string Slogun2 { get; set; }

        [StringLength(200)]
        public string SlideTitle { get; set; }

        [StringLength(500)]
        public string LinkBanner { get; set; }

        [StringLength(20)]
        public string LinkTarget { get; set; }

        public int? CategoryID { get; set; }

        public bool? IsActive { get; set; }

        public int? Sort { get; set; }

        public DateTime? Created { get; set; }

        public int? CreateBy { get; set; }

        public virtual User User { get; set; }
    }
}
