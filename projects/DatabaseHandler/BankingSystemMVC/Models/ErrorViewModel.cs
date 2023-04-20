namespace BankingSystemMVC.Models
{
    /// <summary>
    /// Model used for error handling
    /// </summary>
    public class ErrorViewModel
    {
        public string? RequestId 
        { 
            get;
            set;
        }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}