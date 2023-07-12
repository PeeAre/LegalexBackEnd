namespace LegalexBackEnd.Services.Senders
{
    public static class Extensions
    {
        private static ConfigurationManager _configurationManager { get; set; }

        public static void AddSenders(this IServiceCollection services)
        {
            services.AddTransient<TelegramSender>();
        }
    }
}
