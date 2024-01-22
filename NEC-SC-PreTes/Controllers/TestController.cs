using Common.Model;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace NEC_SC_PreTes.Controllers
{
    [ApiController]
    public class TestController : Controller
    {
        private readonly IProductService _productService;

        public TestController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpGet("CheckProductDeadLine")]
        public async Task<BaseReturn> CheckProductDeadLine(string statusCode)
        {
            var nowTime = DateTime.Now;
            var result = BaseReturn.BadReturn();
            result = await _productService.CheckProductDeadLine(statusCode, nowTime);

            return result;
        }

        [HttpGet("CheckProductDeadLines")]
        public async Task<List<BaseReturn>> CheckProductDeadLine2(List<string> statusCode)
        {
            var result = new List<BaseReturn>();

            foreach (var item in statusCode)
            {
                var nowTime = DateTime.Now;
                var itemResult = await _productService.CheckProductDeadLine(item,nowTime);
                result.Add(itemResult);
            }

            return result;
        }
    }
}
