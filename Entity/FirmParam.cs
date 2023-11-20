using System.Reflection.Metadata;

namespace Entity
{
    public class FirmParam : BaseEntity
    {
        public int No { get; set; }
        public string Key { get; set; }
        public byte[] Value { get; set; }
    }
}
