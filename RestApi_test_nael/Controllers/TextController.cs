using Microsoft.AspNetCore.Mvc;
using projet_test_nael_data;
using projet_test_nael_data.Models;

namespace RestApi_test_nael.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TextController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly ILogger<TextController> _logger;

        public TextController(MyDbContext context, ILogger<TextController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [Route("api/[controller]/{id}")]
        public async Task<ActionResult<Text?>> GetById(int id, CancellationToken cancellationToken)
        {
            var text = await _context.Texts.FindAsync(new object?[] { id }, cancellationToken: cancellationToken);

            if (text == null)
            {
                return NotFound("Text not found");
            }

            return Ok(text);
        }


        [HttpPost]
        [Route("api/[controller]")]
        public async Task<ActionResult<Text?>> Add(Text text, CancellationToken cancellationToken)
        {
            //return string.Empty;

            _context.Texts.Add(text);
            await _context.SaveChangesAsync(cancellationToken);

            return Ok(text);
        }

        //[HttpPost]
        //[Route("api/[controller]")]
        //public string Add(string text)
        //{
        //    return "Sent text: " + text;
        //}

        [HttpPut]
        [Route("api/[controller]/updatetext")]
        public async Task<ActionResult<Text?>> update(int id, string newText, CancellationToken cancellationToken)
        {
            var text = await _context.Texts.FindAsync(new object?[] { id }, cancellationToken: cancellationToken);

            text.Content = newText;
            if (text == null)
            {
                return NotFound("Text not found");
            }
            await _context.SaveChangesAsync(cancellationToken);

            return Ok(text);
        }

        //[HttpGet]
        //[Route("api/[controller]/GetWeatherForecast")]
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateTime.Now.AddDays(index),
        //        TemperatureC = Random.Shared.Next(-20, 55),
        //        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}
    }
}