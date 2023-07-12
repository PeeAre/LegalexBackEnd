using LegalexBackEnd.Models.Order;
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
                var type = model.Type == Entity.Legal ? "Юридическое лицо" : "Физическое лицо";
                await _telegramSender.SendAsync(
                    $"*Тип заявки:*  {type}\n\n*Имя:* {model.Name}\n\n*Номер телефона:*  {model.Phone}\n\n*Описание:*  {model.Description}");
            }
            catch (Exception ex)
            {
                await _appContext.SaveChangesAsync();
                return BadRequest("Failed to send notification");
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
