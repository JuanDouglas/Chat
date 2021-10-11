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
        [ForeignKey("TargetUserNavigation")]
        public int TargetUser { get; set; }
        [Required]
        [ForeignKey("SenderUserNavigation")]
        public int SenderUser { get; set; }
        [ForeignKey("MessageContentNavigation")]
        public int? MessageContent { get; set; }
        [Required]
        [StringLength(6000)]
        public string Text { get; set; }


        public User TargetUserNavigation { get; set; }
        public User SenderUserNavigation { get; set; }
        public MessageContent? MessageContentNavigation { get; set; }
    }
}
