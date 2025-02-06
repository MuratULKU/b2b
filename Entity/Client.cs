using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Client:BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        [MaybeNull]
        public string? VatOffice { get; set; }
        public string? VKN { get; set; }
        public string? MailAdress { get; set; }
        public string? Phone1 { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 {  get; set; }   
        public string? Town { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? FirmExecutiveName { get; set; }
        public string? FirmExecutiveSurName { get; set; }
        public string? Phone2 { get; set; }  
        public string? MailAdress2 {  get; set; }
    }
}
