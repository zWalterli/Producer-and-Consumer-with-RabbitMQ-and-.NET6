namespace Producer.Models
{
    public class MessageInputModel
    {
        public string Destinatarios { get; set; } = string.Empty;
        public string Assunto { get; set; } = string.Empty;
        public string Corpo { get; set; } = string.Empty;
    }
}