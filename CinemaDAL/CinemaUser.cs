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
    }
}
