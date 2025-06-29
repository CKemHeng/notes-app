
using System.Data;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Note.Data;
using Note.Models;

namespace INoteRepositoryRe
{
    public interface INoteRepository
    {
        
        Task<IEnumerable<NoteModel>> FetchNoteAsync();
        Task<NoteModel?> GetByIdAsync(int id);
        Task AddAsync(NoteModel note);
        Task UpdateAsync(int id,NoteModel note);
        Task DeleteAsync(int id);
    }


    public class NoteRepository : INoteRepository
    {
        
  
        private readonly IDbConnection _db;

       public NoteRepository(IDbConnection db)
        {
            _db = db;
        }
          public async Task<IEnumerable<NoteModel>> FetchNoteAsync()
            {
                var sql = "SELECT * FROM Notes ORDER BY CreatedAt DESC";
                return await _db.QueryAsync<NoteModel>(sql);
            }

         public async Task<NoteModel?> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Notes WHERE Id = @Id";
            return await _db.QueryFirstOrDefaultAsync<NoteModel>(sql, new { Id = id });
        }
          public async Task AddAsync(NoteModel note)
        {
            var sql = @"
                INSERT INTO Notes (Title, Content, CreatedAt, UpdatedAt)
                VALUES (@Title, @Content, @CreatedAt, @UpdatedAt);
                SELECT CAST(SCOPE_IDENTITY() as int);
            ";

            var id = await _db.ExecuteScalarAsync<int>(sql, new
            {
                note.Title,
                note.Content,
                CreatedAt = DateTime.Now,
                UpdatedAt = (DateTime?)null // Initially null for new notes
              
            });

            note.Id = id;
        }

        public async Task UpdateAsync(int id, NoteModel note)
        {
            var sql = @"
                UPDATE Notes
                SET Title = @Title,
                    Content = @Content,
                    UpdatedAt = @UpdatedAt
                WHERE Id = @Id;
            ";

            await _db.ExecuteAsync(sql, new
            {
                Id = id,
                note.Title,
                note.Content,
                UpdatedAt = DateTime.Now
            });
        }

        public async Task DeleteAsync(int id)
        {
            var sql = "DELETE FROM Notes WHERE Id = @Id";
            await _db.ExecuteAsync(sql, new { Id = id });
        }
    }

}