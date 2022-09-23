using Microsoft.EntityFrameworkCore;
using PolicyDetailsPdfGenerator.DataAccessLayer.PolicydetailDbContext;
using PolicyDetailsPdfGenerator.DataAccessLayer.PolicyDetailRepository;
using PolicyDetailsPdfGenerator.PersonPolicyService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PolicyDetailDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IPersonPolicyDetailService, PersonPolicyDetailService>();
builder.Services.AddScoped<IPersonPolicyDetailRepository, PersonPolicyDetailRepository>();

builder.Services.AddScoped<HtmlToPdfConverterService>();

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
