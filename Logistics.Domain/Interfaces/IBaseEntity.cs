using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Domain.Interfaces
{
    public interface IBaseEntity
    {
        int Id { get; }
        bool IsActive { get; }
        DateTime CreationDate { get; }
        void SetAsActive();
        void SetAsInactive();
    }
}
