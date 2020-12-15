namespace CinemaDAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Place")]
    public partial class Place
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Place()
        {
            Ticket = new HashSet<Ticket>();
        }

        public int PlaceId { get; set; }

        public int HallId { get; set; }

        public int PlaceRow { get; set; }

        public int PlaceColumn { get; set; }

        public int PlaceState { get; set; }

        public decimal PlacePriceMultiplier { get; set; }

        [StringLength(128)]
        public string PlaceFIO { get; set; }

        public virtual Halls Halls { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ticket> Ticket { get; set; }
    }
}
