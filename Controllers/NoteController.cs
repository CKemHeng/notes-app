using INoteRepositoryRe;
using Microsoft.AspNetCore.Mvc;
using Note.Models;


[ApiController]
[Route("api/[controller]")]
public class NoteController : ControllerBase
{
    private readonly INoteRepository _repo;

    public NoteController(INoteRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var notes = await _repo.FetchNoteAsync();
        return Ok(notes);
    }

    [HttpPost]
    public async Task<IActionResult> createNote(NoteModel note)
    {
        await _repo.AddAsync(note);
        return Ok(new { message = "Note Added successfully" });
    }

        // PUT api/Note/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateNote(int id, NoteModel note)
        {
            if (id != note.Id) return BadRequest("ID mismatch");

            await _repo.UpdateAsync(id, note);
            
            return Ok(new { message = "Note updated successfully" });
        }


    // DELETE api/Note/{id}
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> deleteNote(int id)
    {
     

        await _repo.DeleteAsync(id);
        return NoContent();
    }
    

    // Add more endpoints (GetById, Update, Delete) as needed
}
