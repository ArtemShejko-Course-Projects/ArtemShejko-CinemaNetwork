namespace CinemaDAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CinemaStaff")]
    public partial class CinemaStaff
    {
        public int CinemaStaffId { get; set; }

        [Required]
        [StringLength(50)]
        public string CinemaStaffLogin { get; set; }

        [Required]
        [StringLength(24)]
        public string CinemaStaffPass { get; set; }

        [Required]
        [StringLength(64)]
        public string CinemaStaffPost { get; set; }

        [Required]
        [StringLength(64)]
        public string CinemaStaffName { get; set; }

        [Required]
        [StringLength(50)]
        public string CinemaStaffPhone { get; set; }

        [Required]
        [StringLength(64)]
        public string CinemaStaffEmail { get; set; }

        public int CinemaStaffRole { get; set; }

        [Column(TypeName = "money")]
        public decimal? CinemaStaffSalary { get; set; }

        [StringLength(2048)]
        public string CinemaStaffPhoto { get; set; }

        public DateTime? CinemaStaffDateStartWork { get; set; }

        public DateTime? CinemaStaffDateEndWork { get; set; }

        public bool? CinemaStaffLogged { get; set; }

        public virtual RoleTable RoleTable { get; set; }
    }
}
