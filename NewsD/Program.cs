using NewsD.DataAccess;
using NewsD.Services;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllers();
services.AddTransient<IFileUpload, FileUpload>();
services.AddDbContext<NewsDDataContext>(ServiceLifetime.Transient);
services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();

app.UseCors(_ => _.AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowAnyOrigin());

app.UseAuthentication();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
