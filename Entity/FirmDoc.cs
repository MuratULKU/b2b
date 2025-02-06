using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class FirmDoc : BaseEntity
    {
        public int InfoType { get; set; }
        public int LineNumber { get; set; }
        public Guid ProtuctId { get; set; }
        public byte[] LData { get; set; }
        [NotMapped]
       public string Code { get; set; }

    }
}
