using System.Data;
using INoteRepositoryRe;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddScoped<INoteRepository, NoteRepository>();
builder.Services.AddScoped<IDbConnection>(sp =>
    new SqlConnection(builder.Configuration.GetConnectionString("UsersConnection")));

// CORS setup
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
        policy.WithOrigins(
            "http://localhost:5173", // ⚠️ Note: use http for dev server, not https unless you're using HTTPS locally
            "https://ckemheng.github.io" // ⚠️ Use just domain, no path!
        )
        .AllowAnyHeader()
        .AllowAnyMethod());
});

// Other services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Apply correct CORS policy ONCE
app.UseCors("AllowFrontend");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
