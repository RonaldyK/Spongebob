using MongoDB.Driver;
using Spongebob.Components;
using Spongebob.Data;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Blazor Bootstrap component library
builder.Services.AddBlazorBootstrap();

// OpenAPI service
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Setting up MongoDB
var connectionString = builder.Configuration["Plankton:MongoDbConnection"];

if (connectionString == null)
{
    Console.WriteLine("You gotta set up the connecting string URI first in your environment variable lil pup");
    Environment.Exit(0);
}
var client = new MongoClient(connectionString);
var db = NewsletterDb.Create(client.GetDatabase("plankton-db"));

var email = db.Newsletters.First(n => n.Email == "the-maldivian@hotmail.com");
Console.WriteLine(email.Email);




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Enable Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();



// Add API endpoint extensions
app.MapFormEndpoints();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
