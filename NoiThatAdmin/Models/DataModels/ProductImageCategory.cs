namespace NoiThatAdmin.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductImageCategory")]
    public partial class ProductImageCategory
    {
        [Key]
        public int ImageID { get; set; }

        public int? CategoryIDParent { get; set; }

        public int? CategoryID { get; set; }

        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(100)]
        public string URLImage { get; set; }

        [StringLength(100)]
        public string ImagesThumb { get; set; }

        public DateTime? Created { get; set; }

        public virtual Category Category { get; set; }

        public virtual Category Category1 { get; set; }
    }
}
