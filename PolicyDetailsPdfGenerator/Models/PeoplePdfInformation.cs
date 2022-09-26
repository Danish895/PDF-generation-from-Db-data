using System.ComponentModel.DataAnnotations.Schema;

namespace PolicyDetailsPdfGenerator.Models
{
    
    public class PeoplePdfInformation
    {
        public int Id { get; set; }
        public string ObjectCode { get; set; } = String.Empty;
        public DateTime ObjectName { get; set; } = DateTime.Now;
        public string ReferenceType { get; set; } = "dkhdj";
        public int ReferenceNumber { get; set; } = 9;
        public byte[] Content { get; set; } = new byte[] {12,34};
        public string FileName { get; set; } = String.Empty;
        public string FileExtension { get; set; } = String.Empty;
        public string LanguageCode { get; set; } = String.Empty;
        public string CreatedUser { get; set; } = "dkjdk";
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        
    }
}
