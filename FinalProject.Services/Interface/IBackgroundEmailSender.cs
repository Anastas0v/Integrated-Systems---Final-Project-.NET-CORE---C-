using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Services.Interface
{
    public interface IBackgroundEmailSender
    {
        Task DoWork();
    }
}
