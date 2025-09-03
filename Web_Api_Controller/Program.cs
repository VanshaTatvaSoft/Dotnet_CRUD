using Web_Api_Controller.Extension;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();

//register service and repository
builder.Services.RegisterDependencies();

//show model state error
builder.Services.ModelStateValidator();

//api versioning
builder.Services.ApiVersioning();

//api rate limiting
builder.Services.RateLimiting(builder.Configuration);

//dbcontext configuration
builder.Services.RegisterDbConnection(builder.Configuration);

// In Memory Database configuration
// builder.Services.AddDbContext<ProductManagementContext>(options =>
//     options.UseInMemoryDatabase("TestDb"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRateLimiter();

app.MapControllers().RequireRateLimiting("fixed");

//Seeding data for In Memory
// using(var scope = app.Services.CreateScope())
// {
//     var context = scope.ServiceProvider.GetRequiredService<ProductManagementContext>();
//     InMemorySeeder.InMemmoryDbSeeder(context);
// }

app.Run();

