namespace CinemaDAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ticket")]
    public partial class Ticket
    {
        public int TicketId { get; set; }

        public int PlaceId { get; set; }

        public int? CinemaUserId { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public virtual CinemaUser CinemaUser { get; set; }

        public virtual Place Place { get; set; }
    }
}
