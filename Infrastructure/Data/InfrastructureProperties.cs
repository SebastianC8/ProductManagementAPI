using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class InfrastructureProperties
    {
        public string SMTP_SERVER { get; set; }
        public int SMTP_PORT { get; set; }
        public string SMTP_USER { get; set; }
        public string SMTP_PASSWORD { get; set; }
    }
}
