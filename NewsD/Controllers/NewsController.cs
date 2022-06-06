using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsD.DataAccess;
using NewsD.DataContracts;
using NewsD.Model;
using NewsD.Services;

namespace NewsD.Controllers;

[ApiController]
[Route("[controller]")]
public class NewsController : ControllerBase
{
    private readonly NewsDDataContext _context;
    private readonly IMapper _mapper;
    private readonly IFileUpload _fileUpload;
    public NewsController(NewsDDataContext context, IMapper mapper, IFileUpload fileUpload)
    {
        _context = context;
        _mapper = mapper;
        _fileUpload = fileUpload;
    }

    [HttpPost]
    public async Task<IActionResult> CreateNews([FromForm] DataContracts.NewsRequest requestBody)
    {
        var userExists = (await _context.Users!.CountAsync(u => u.Id == requestBody.CreatorId)) >= 1;

        if (!userExists) return BadRequest("Usuário não existe");

        string imageUrl = "";
        var newsId = Guid.NewGuid();
        try
        {
            var fileName = newsId.ToString() + "-" + DateTime.Now.ToString("MM-dd-yyyy");
            imageUrl = await _fileUpload.UploadFile(fileName, "news", requestBody.Image!);
        }
        catch (Exception e)
        {
            Console.WriteLine($"{e.Message}");
        }

        var news = _mapper.Map<Model.News>(requestBody);
        news.Id = newsId;
        news.ImageUrl = imageUrl;

        await _context.News!.AddAsync(news);
        await _context.SaveChangesAsync();

        var response = _mapper.Map<DataContracts.NewsResponse>(news);

        return Created("", response);
    }

    [HttpGet]
    [Route("{id}")]
    public IActionResult GetNews([FromRoute] Guid id)
    {
        var news = _context.News!.Include(n => n.Creator).SingleOrDefault(n => n.Id == id);

        var newsResponse = _mapper.Map<NewsResponse>(news);
        return Ok(newsResponse);
    }

    [HttpGet]
    public IActionResult GetNews([FromQuery] string? term = "", [FromQuery] string? id = "")
    {
        var newsQuery = _context.News!.Include(n => n.Creator) as IQueryable<News>;

        if (!string.IsNullOrEmpty(term))
        {
            newsQuery = newsQuery.Where(n => n.Title!.ToUpper().Contains(term!.ToUpper()) || n.Creator!.Name!.ToUpper().Contains(term.ToUpper()));
        }

        if (Guid.TryParse(id, out Guid guid))
        {
            newsQuery = newsQuery.Where(n => n.CreatorId == guid);
        }

        var newsResponse = _mapper.Map<NewsResponse[]>(newsQuery.ToArray());
        return Ok(newsResponse);
    }
}
