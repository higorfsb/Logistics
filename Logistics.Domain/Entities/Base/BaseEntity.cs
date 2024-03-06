using Logistics.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Domain.Entities.Base
{
    public class BaseEntity : IBaseEntity
    {
        protected BaseEntity()
        {
            CreationDate = DateTime.UtcNow;
            SetAsActive();
        }

        public int Id { get; private set; }
        public DateTime CreationDate { get; private set; }
        public bool IsActive { get; protected set; }

        public void SetAsInactive()
        {
            IsActive = false;
        }

        public void SetAsActive()
        {
            IsActive = true;
        }
    }
}
