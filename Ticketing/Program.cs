using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Serilog;
using Ticketing.Core.Services;
using Ticketing.Core.Services.Interfaces;
using Ticketing.DataLayer.Context;
using Ticketing.DataLayer.Entities.TicketAnswer;
using Ticketing.DataLayer.Entities.User;

#region Logging Configuration

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("Logs/Ticketing.txt",rollingInterval: RollingInterval.Day)
    .CreateLogger();

#endregion

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region Add Serilog

builder.Host.UseSerilog();

#endregion

#region AddController

builder.Services.AddControllers(options =>
    {
        options.ReturnHttpNotAcceptable = true;
    })
    .AddNewtonsoftJson(option =>
    {
        option.SerializerSettings.ReferenceLoopHandling =
            ReferenceLoopHandling.Ignore;
    } )
    .AddXmlDataContractSerializerFormatters();

#endregion


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

#region Contexts
builder.Services.AddDbContext<TicketingContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
#endregion

#region IOC

builder.Services.AddTransient<ITicketService, TicketService>();
builder.Services.AddTransient<IMailServices, MailServices>();
builder.Services.AddTransient<IUserServices, UserServices>();
builder.Services.AddTransient<IAdminServices, AdminServices>();
builder.Services.AddTransient<ITicketQuestionServices, TicketQuestionServices>();
builder.Services.AddTransient<ITicketAnswerServices, TicketAnswerServices>();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
