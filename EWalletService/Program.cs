using EWalletService.Application;
using EWalletService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
//Added for read Body from http request
app.Use(async (context, next) =>
{
    context.Request.EnableBuffering();
    await next();
});
app.UseAuthorization();
//app.UseXDigestValidation();
//app.UseXUserIdValidation();

app.MapControllers();

app.Run();
