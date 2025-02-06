using Microsoft.AspNetCore.Components;
using System.Collections.ObjectModel;

namespace B2B.Components.NotificationService
{
    public enum NotificationSeverity
    {
        Info,
        Success,
        Warning,
        Error,
    }
    
    public class NotificationService
    {
        public ObservableCollection<NotificationMessage> Messages { get; private set; } = new ObservableCollection<NotificationMessage>();

        public void Notify(NotificationMessage message)
        {
            var newMessage = new NotificationMessage()
            {
                Duration = message != null && message.Duration.HasValue ? message.Duration : 3000,
                Severity = message.Severity,
                Summary = message.Summary,
                Detail = message.Detail,
                Style = message.Style
            };

            if (!Messages.Contains(newMessage))
            {
                Messages.Add(newMessage);
            }
        }

        
        public void Notify(NotificationSeverity severity = NotificationSeverity.Info, string summary = "", string detail = "", int duration = 3000)
        {
            var newMessage = new NotificationMessage()
            {
                Duration = duration,
                Severity = severity,
                Summary = summary,
                Detail = detail
            };

            if (!Messages.Contains(newMessage))
            {
                Messages.Add(newMessage);
            }
        }
    }

    
    public class NotificationMessage : ComponentBase
    {
    
        public int? Duration { get; set; }
    
        public NotificationSeverity Severity { get; set; }
    
        public string Summary { get; set; }
    
        public string Detail { get; set; }
    
        public string Style { get; set; }
    }
}
