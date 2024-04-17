namespace ExceleTech.Domain.Options
{
    public class EmailOptions
    {
        public string SenderEmailAdress { get; set; }
        
        public string SenderEmailPassword { get; set; }
        
        public int Port { get; set; }
        
        public string Server { get; set; }
    }
}