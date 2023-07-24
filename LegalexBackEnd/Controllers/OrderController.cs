using LegalexBackEnd.Models.Order;
using LegalexBackEnd.Models.Order.Types;
using LegalexBackEnd.Services.Senders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace LegalexBackEnd.Controllers
{
    public class OrderController : BaseApiController
    {
        private readonly ILogger<OrderController> _logger;
        private readonly ApplicationContext _appContext;
        private TelegramSender _telegramSender;

        public OrderController(ILogger<OrderController> logger, ApplicationContext appContext, TelegramSender telegramSender)
        {
            _logger = logger;
            _appContext = appContext;
            _telegramSender = telegramSender;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> Post([FromBody] Order model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model isn't valid");
            }

            try
            {
                await _appContext.Orders.AddAsync(model);

            }
            catch
            {
                return BadRequest("Failed to add this model");
            }

            try
            {
                string service = string.Empty;
                switch (model.Service)
                {
                    case Service.NonSelected:
                        service = "Не выбран";
                        break;
                    case Service.Legal:
                        service = "Юридическая";
                        break;
                    case Service.Finance:
                        service = "Финансовая";
                        break;
                    case Service.Accounting:
                        service = "Бухгалтерская";
                        break;
                    case Service.HR:
                        service = "Кадровая";
                        break;
                }
                var type = model.Type == Entity.Legal ? "Юридическое лицо" : "Физическое лицо";
                var email = model.Email != string.Empty ? $"\n\n*Электронная почта:* {model.Email}" : string.Empty;
                await _telegramSender.SendAsync(
                    $"*Тип заявки:*  {type}\n\n*Тип услуги:* {service}\n\n*Имя:* {model.Name}\n\n*Номер телефона:* " +
                    $"{model.Phone}{email}\n\n*Описание:*  {model.Description}");
            }
            catch (Exception ex)
            {
                await _appContext.SaveChangesAsync();
                return BadRequest($"Failed to send notification: {ex.Message}");
            }

            await _appContext.SaveChangesAsync();

            return Ok("Order was added");
        }

        [HttpGet]
        public async Task<ActionResult<Order>> Get()
        {
            var orders = await _appContext.Orders.ToListAsync();
            if (orders.IsNullOrEmpty())
            {
                return BadRequest("No orders");
            }

            return Ok(orders);
        }
    }
}
