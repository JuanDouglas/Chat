using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Server.Dal.SqlServer.Models
{
    [Table("Users")]
    public class User
    {
        [Required]
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column("Username")]
        [StringLength(50, MinimumLength = 4)]
        public string Username { get; set; }

        [NotMapped]
        public List<Message> SendMessages { get; set; }
    }
}
