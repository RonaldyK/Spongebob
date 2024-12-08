// So instead of adding api endpoints to the program.cs file and making it messy, I'll create an extension class
// Read more:
// https://www.mattbutton.com/minimal-apis-dotnet/
// https://learn.microsoft.com/en-us/aspnet/core/fundamentals/apis?view=aspnetcore-9.0

using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using Spongebob.Data;
using Spongebob.Models;

public static class FormEndpoints
{
    public static void MapFormEndpoints(this WebApplication app)
    {
        // Footer form data endpoint----------------------------------
        // Add a post endpoint for form submission
        app.MapPost("/submit-form", async (HttpRequest request, NewsletterDb database) =>
        {
            // Just parsing the form data
            var form = await request.ReadFormAsync();
            string email = form["email"];

            // Some simple input validation (Is it really needed if the input field already has validation?)
            if (string.IsNullOrEmpty(email))
            {
                return Results.BadRequest("Email is required.");
            }

            // Check if the email already exists
            var existingEmail = await database.Newsletters.AsNoTracking().FirstOrDefaultAsync(n => n.Email == email);
            if (existingEmail != null)
            {
                Console.WriteLine($"Email is already in database: {existingEmail.Email}");
            }
            else
            {
                // Adds new email to database
                var newNewsletter = new Newsletter { Id = ObjectId.GenerateNewId(), Email = email, Date = DateTime.Now };
                database.Newsletters.Add(newNewsletter);
                await database.SaveChangesAsync();

                Console.WriteLine($"Email submitted: {newNewsletter.Email}");
            }
            

            // Get the referrer so the user gets redirected to exactly where the button was pressed
            string referrer = request.Headers.Referer.ToString();
            if (string.IsNullOrEmpty(referrer))
            {
                // Defaults to the homepage
                referrer = "/";
            }


            // Return a success response
            //return Results.Ok(new { message = "Submitted succesfully" });

            // Return to the caller
            return Results.Redirect($"{referrer}#emailForm");
        })
            .WithName("SubmitForm")
            .WithDescription("Submits a user email")
            .WithSummary("Submit a form with an email address")
            .WithOpenApi();
    }
}

