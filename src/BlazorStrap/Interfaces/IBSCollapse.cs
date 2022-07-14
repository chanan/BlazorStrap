using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorStrap.Interfaces
{
    public interface IBSCollapse
    {
        bool Shown { get; }
        string DataId { get; }

        Task ToggleAsync();
    }
}
