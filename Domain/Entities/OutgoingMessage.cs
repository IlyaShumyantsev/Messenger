namespace Domain.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class OutgoingMessage
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int UserToId { get; set; }

        [Required]
        [StringLength(1024)]
        public string Text { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreateDate { get; set; }
    }
}
