namespace NoiThatAdmin.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Blog")]
    public partial class Blog
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Blog()
        {
            BlogComments = new HashSet<BlogComment>();
        }

        public int BlogID { get; set; }

        [StringLength(200)]
        public string BlogName { get; set; }

        [StringLength(200)]
        public string ImageURL { get; set; }

        [StringLength(400)]
        public string Descript { get; set; }

        public string Content { get; set; }

        public int? TypeBlog { get; set; }

        public int? CountView { get; set; }

        public bool? IsActive { get; set; }

        public int? CatetoryID { get; set; }

        public DateTime? Created { get; set; }
        public DateTime? LastModify { get; set; }

        public int? CreatedBy { get; set; }

        [StringLength(200)]
        public string SEOTitle { get; set; }

        [StringLength(200)]
        public string SEOUrlRewrite { get; set; }

        [StringLength(200)]
        public string SEOKeywords { get; set; }

        [StringLength(500)]
        public string SEOMetadescription { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BlogComment> BlogComments { get; set; }

        public virtual Category Category { get; set; }
    }
}
