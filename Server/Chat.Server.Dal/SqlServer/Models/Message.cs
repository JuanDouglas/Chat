using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Server.Dal.SqlServer.Models
{
    [Table("Messages")]
    public class Message
    {
        [Required]
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int TargetUser { get; set; }
        [Required]
        public int SenderUser { get; set; }
        public int? MessageContent { get; set; }
        [Required]
        [StringLength(6000)]
        public string Text { get; set; }

        [NotMapped]
        public User TargetUserNavigation { get; set; }
        [NotMapped]
        public User SenderUserNavigation { get; set; }
        [NotMapped]
        public MessageContent? MessageContentNavigation { get; set; }
    }
}
