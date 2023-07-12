using Telegram.Bot.Types;

namespace LegalexBackEnd.Services.Senders
{
    public interface ISender
    {
        Task SendAsync(string message);
    }
}
