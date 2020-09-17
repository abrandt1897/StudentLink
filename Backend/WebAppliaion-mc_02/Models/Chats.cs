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
        public int ChatID { get; set; }
        public int Sender { get; set; }
        public string Data { get; set; }
    }
}
