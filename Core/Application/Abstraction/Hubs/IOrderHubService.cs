using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstraction.Hubs
{
    public interface IOrderHubService
    {
        Task OrderAddedMessageAsync(string Message);
    }
}
