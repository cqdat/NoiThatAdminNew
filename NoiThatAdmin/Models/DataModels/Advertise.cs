namespace NoiThatAdmin.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Advertise")]
    public partial class Advertise
    {
        public int AdvertiseID { get; set; }

        [StringLength(5)]
        public string AdvertiseCode { get; set; }

        [StringLength(100)]
        public string AdvertiseName { get; set; }

        [StringLength(100)]
        public string AdvertiseImage { get; set; }

        [StringLength(100)]
        public string AdvertiseURL { get; set; }

        public bool? IsActive { get; set; }

        public string AdvContent { get; set; }

        [StringLength(200)]
        public string SEOTitle { get; set; }

        [StringLength(200)]
        public string SEOUrlRewrite { get; set; }

        [StringLength(200)]
        public string SEOKeywords { get; set; }

        [StringLength(500)]
        public string SEOMetadescription { get; set; }
    }
}
