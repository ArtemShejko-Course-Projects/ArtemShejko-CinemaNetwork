namespace CinemaDAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CinemaUser")]
    public partial class CinemaUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CinemaUser()
        {
            Ticket = new HashSet<Ticket>();
        }

        public int CinemaUserId { get; set; }

        [Required]
        [StringLength(50)]
        public string CinemaUserLogin { get; set; }

        [Required]
        [StringLength(24)]
        public string CinemaUserPass { get; set; }

        [StringLength(64)]
        public string CinemaUserName { get; set; }

        [StringLength(50)]
        public string CinemaUserPhone { get; set; }

        [StringLength(64)]
        public string CinemaUserEmail { get; set; }

        public int CinemaUserRole { get; set; }

        public bool? CinemaUserLogged { get; set; }

        public int? CinemaUserВiscount { get; set; }

        [StringLength(2048)]
        public string CinemaUserPhoto { get; set; }

        public virtual RoleTable RoleTable { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ticket> Ticket { get; set; }
    }
}
