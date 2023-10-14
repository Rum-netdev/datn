namespace EcommercialWebApp.Core.Helpers.Email.Models
{
    public class SendMailResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public ICollection<string> Errors { get; set; }
        public DateTime ExecutedTime { get; set; }
        public string? From { get; set; }
        public string? To { get; set; }
    }
}
