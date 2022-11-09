using Aplication.Helpers.MappingProfiles;
using Aplication.Posts;
using Doiman;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);
//Configuracao do Identity
builder.Services.AddIdentity<User,IdentityRole>().AddDefaultTokenProviders().AddEntityFrameworkStores<DataContext>();
builder.Services.AddAutoMapper(typeof(MappingProfiles));
//usamos para definir servico de DB
builder.Services.AddDbContext<DataContext>(optionsAction =>
{
    optionsAction.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"));
});

//So fazemos uma vez
builder.Services.AddMediatR(typeof(CreatePost.CreatePostCommand).Assembly);//configuracao do mediatr


// Add services to the container.
builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
app.UseAuthorization();
app.MapControllers();
app.Run();