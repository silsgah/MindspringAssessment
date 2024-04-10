using Demo.API.Dto;
using Entity;
using Entity.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Demo.API.Controllers
{
    public class FunController : BaseController
    {
        private readonly BackContext _backContext;
        private readonly IFunTranslateRepository _repository;
        public FunController(IFunTranslateRepository repository, BackContext context)
        {
            _repository = repository;
            _backContext = context;
        }
        [HttpGet("v1")]
        public async Task<ActionResult<List<TblResponse>>> GetFunTranslations()
        {
            var funs = await _repository.GetFunAsync();
            return Ok(funs);
        }
        [HttpGet("v1/{usertext}")]
        public async Task<ActionResult> GetFunTranslation([FromRoute] string usertext)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://api.funtranslations.com/");
            var responnse = await client.GetAsync($"translate/dothraki?text={usertext}");
            if (responnse != null)
            {
                if(responnse.IsSuccessStatusCode)
                {
                    var result = await responnse.Content.ReadFromJsonAsync<Total>();
                    return Ok(result.contents);
                }
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<string>> CreateFunTranslate([FromBody] FunRequest userrequest)
        {
            //course.Instructor = User.Identity.Name;
            var client = new HttpClient();
            TblResponse newresponse = new TblResponse();
            
            client.BaseAddress = new Uri("http://api.funtranslations.com/");
            var responnse = await client.GetAsync($"translate/dothraki?text={userrequest.userText}");
            if (responnse != null)
            {
                if (responnse.IsSuccessStatusCode)
                {
                    var result = await responnse.Content.ReadFromJsonAsync<Total>();
                    newresponse.text = userrequest.userText;
                    newresponse.translation = result.contents.translation;
                    newresponse.translated = result.contents.translated;
                    newresponse.date = DateTime.UtcNow.Date;
                    await _backContext.TblResponses.AddAsync(newresponse);
                    _backContext.SaveChangesAsync();
                    return Ok(result.contents);
                }
            }
            return BadRequest();
        }

    }
}
