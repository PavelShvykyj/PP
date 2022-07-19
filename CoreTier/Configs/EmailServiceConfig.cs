using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreTier.Configs
{
    public class EmailServiceConfig
    {
        public string SMTP { get; set; }
        public string POP { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public int SMTPPort { get; set; }
        public string Login { get; set; }
    }
}
