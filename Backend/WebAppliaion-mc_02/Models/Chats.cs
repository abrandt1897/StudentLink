using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApplication_mc_02.Models
{
    public class Chats
    {
        [Key]
        public int Record { get; set; } = 0;
        public int ChatID { get; set; }
        public int SenderID { get; set; }
        public string Data { get; set; }
    }
}
