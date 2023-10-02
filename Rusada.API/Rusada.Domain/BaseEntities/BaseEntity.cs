using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rusada.Domain.BaseEntities
{
    public class BaseEntity
    {
        public int Id { get; set; }
    }

    public class BaseEntityWithSoftDelete : BaseEntityWithAudit, ISoftDeleteEntity
    {
        public bool Deleted { get; set; }
    }

    public class BaseEntityWithAudit : BaseEntity, IAuditEntity
    {
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }


    public interface ISoftDeleteEntity : IAuditEntity
    {
        bool Deleted { get; set; }
    }

    public interface IAuditEntity
    {
        string CreatedBy { get; set; }
        string UpdatedBy { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime UpdatedDate { get; set; }
    }
}