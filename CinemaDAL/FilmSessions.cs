namespace CinemaDAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class FilmSessions
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FilmSessions()
        {
            Ticket = new HashSet<Ticket>();
        }

        public int FilmSessionsId { get; set; }

        public int FilmId { get; set; }

        public int HallId { get; set; }

        public int Places { get; set; }

        public int FreePlaces { get; set; }

        public DateTime SessionDate { get; set; }

        public virtual Films Films { get; set; }

        public virtual Halls Halls { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ticket> Ticket { get; set; }
    }
}
