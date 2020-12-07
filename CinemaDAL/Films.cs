namespace CinemaDAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Films
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Films()
        {
            FilmSessions = new HashSet<FilmSessions>();
        }

        [Key]
        public int FilmId { get; set; }

        [Required]
        [StringLength(64)]
        public string FilmName { get; set; }

        [Required]
        [StringLength(64)]
        public string FilmGenre { get; set; }

        [Required]
        public string FilmActors { get; set; }

        [Required]
        public string FilmDescription { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FilmSessions> FilmSessions { get; set; }
    }
}
