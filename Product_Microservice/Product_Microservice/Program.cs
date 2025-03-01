using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Product.Api;
using Product.Data;
using Product.Data.Interfaces;
using Product.Data.Repositories;
using Product.Service.Interfaces;
using Product.Service.Services;
using Microsoft.Extensions.Options;
using Product.Service.Options;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

var connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<DataContext>(options  => options.UseNpgsql(connection));

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

var pictureSettings = builder.Configuration.GetSection("picture");
builder.Services.Configure<ImageOptions>(pictureSettings);

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IImageRepository, ImageRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ICartRecordRepository, CartRecordRepository>();
builder.Services.AddScoped<ICartRecordService,  CartRecordService>();


builder.Services.AddSingleton(mapper);
builder.Services.AddMvc();
builder.Services.AddControllers();
builder.Services.AddAuthorization();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Pruduct.API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Orchestrator.API v1"));
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.MapControllers();

app.UseAuthorization();

app.MapStaticAssets();

app.MapRazorPages();

app.Run();
