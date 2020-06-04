using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace PromotionEngineAPI
{
    [ApiController]
    [Route("[controller]")]
    public class PromotionEngineController : ControllerBase
    {        
        private readonly ILogger<PromotionEngineController> _logger;

        public PromotionEngineController(ILogger<PromotionEngineController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public ActionResult Post([FromBody]Cart cart)
        {
            var promotionEngine = new PromotionEngine();
            var discountedPrice = promotionEngine.GetBestDiscountedPrice(cart, _logger);
            _logger.Log(LogLevel.Information, $"Discounted Price derived @ {DateTime.Now} for the cart: {cart} is {discountedPrice}");
            return new OkResult();
        }
    }
}
