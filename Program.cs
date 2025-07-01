using Note.Data;
using INoteRepositoryRe;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection; // For CreateScope and GetRequiredService
using Microsoft.Extensions.Logging; // For ILogger (optional, but good for error logging)
using Note.Data; // Assuming YourDbContext is in Note.Data namespace
using Note.Models; 

var builder = WebApplication.CreateBuilder(args);


// Register DbContext

// Register repository interface + implementation
builder.Services.AddScoped<INoteRepository, NoteRepository>();


builder.Services.AddScoped<IDbConnection>(sp =>
    new SqlConnection(builder.Configuration.GetConnectionString("UsersConnection")));


// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
        policy.WithOrigins(
            "https://localhost:5173",                    // Dev server access
            "https://ckemheng.github.io/notes-app/"      // Optional: full path
        )
        .AllowAnyHeader()
        .AllowAnyMethod());
});

// Other services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors("AllowFrontend");
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowFrontendDev");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
