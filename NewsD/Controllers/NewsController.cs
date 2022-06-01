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
    public IActionResult GetNews([FromQuery] string? title = "", [FromQuery] string? id = "")
    {
        IQueryable<News> newsQuery = _context.News!.Include(n => n.Creator);

        if (!string.IsNullOrEmpty(title))
        {
            newsQuery = newsQuery.Where(n => n.Title!.ToUpper().Contains(title!.ToUpper()));
        }

        if (Guid.TryParse(id, out Guid guid))
        {
            newsQuery = newsQuery.Where(n => n.CreatorId == guid);
        }


        var newsResponse = _mapper.Map<NewsResponse[]>(newsQuery.ToArray());
        return Ok(newsResponse);
    }
}
