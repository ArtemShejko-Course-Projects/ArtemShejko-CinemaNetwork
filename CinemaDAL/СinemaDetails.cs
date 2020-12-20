namespace CinemaDAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class СinemaDetails
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public СinemaDetails()
        {
            Halls = new HashSet<Halls>();
        }

        public int СinemaDetailsId { get; set; }

        [Required]
        [StringLength(256)]
        public string СinemaDetailsTitle { get; set; }

        [Required]
        [StringLength(256)]
        public string СinemaDetailsAddress { get; set; }

        [Required]
        [StringLength(64)]
        public string СinemaDetailsPhone { get; set; }

        public int СinemaDetailsCity { get; set; }

        [StringLength(2048)]
        public string СinemaDetailsPicture { get; set; }

        public virtual City City { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Halls> Halls { get; set; }
    }
}
