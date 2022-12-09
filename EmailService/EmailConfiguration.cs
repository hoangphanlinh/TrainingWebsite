using System;
using System.Collections.Generic;
using System.Text;

namespace EmailService
{
    class EmailConfiguration
    {
        public string From { get; set; }
        public string SMTPServer { get; set; }
        public int Port { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
