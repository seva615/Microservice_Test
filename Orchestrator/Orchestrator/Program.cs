using Microsoft.OpenApi.Models;
using Orchestrator.API.Interfaces;
using Orchestrator.API.Services;
using Refit;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRefitClient<IUserClient>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration["ServicesConnectionStrings:UserService"]));
builder.Services.AddRefitClient<IProductClient>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration["ServicesConnectionStrings:ProductService"]));
builder.Services.AddRefitClient<IImageClient>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration["ServicesConnectionStrings:ProductService"]));
builder.Services.AddRefitClient<ICartClient>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration["ServicesConnectionStrings:ProductService"]));
builder.Services.AddRefitClient<ICommentClient>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration["ServicesConnectionStrings:CommentService"]));

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ICommentService,  CommentService>();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddMvc();
builder.Services.AddControllers();
builder.Services.AddAuthorization();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Orchestrator.API", Version = "v1" });
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert jwt token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(
        c => {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Orchestrator.API v1");
            c.RoutePrefix = string.Empty;
            });
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

