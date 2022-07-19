using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreTier.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string emailAddres, string emailSubject, string emailBody);
    }
}
