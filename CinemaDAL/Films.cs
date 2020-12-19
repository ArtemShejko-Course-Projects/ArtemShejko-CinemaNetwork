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

        public int? FilmGenre { get; set; }

        [Required]
        public string FilmActors { get; set; }

        [Required]
        public string FilmDescription { get; set; }

        public int FilmDuration { get; set; }

        public string FilmImageUri { get; set; }

        public DateTime FilmStartDate { get; set; }

        public DateTime FilmEndDate { get; set; }

        public virtual Genre Genre { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FilmSessions> FilmSessions { get; set; }
    }
}
