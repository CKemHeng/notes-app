using Note.Data;
using INoteRepositoryRe;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Register DbContext

// Register repository interface + implementation
builder.Services.AddScoped<INoteRepository, NoteRepository>();


builder.Services.AddScoped<IDbConnection>(sp =>
    new SqlConnection(builder.Configuration.GetConnectionString("UsersConnection")));


// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontendDev",
        policy =>
        {
            policy.WithOrigins("https://localhost:5173") // Vite default dev port
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});


// Other services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
