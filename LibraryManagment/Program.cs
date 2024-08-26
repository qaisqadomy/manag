using Application.Repos;
using Application.Services;
using Domain.IRepo;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using LibraryManagment.EndPoints;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<BookService>();
builder.Services.AddScoped<MemberService>();
builder.Services.AddScoped<LibraryService>();

builder.Services.AddTransient<IMemberRepo, MemberRepo>();
builder.Services.AddTransient<ILibraryRepo, LibraryRepo>();
builder.Services.AddTransient<IBookRepo, BookRepo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapMemberEndpoints();
app.MapBookEndpoints();
app.MapLibraryEndpoints();
app.Run();