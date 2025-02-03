using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("todo")]
    public class TodoController : ControllerBase
    {
        private readonly TodoRepository _repository;

        public TodoController(TodoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TodoItem>> Get() => Ok(_repository.GetAll());

        [HttpGet("{id}")]
        public ActionResult<TodoItem> Get(int id)
        {
            var item = _repository.GetById(id);
            return item != null ? Ok(item) : NotFound();
        }

        [HttpPost]
        public IActionResult Post([FromBody] TodoItem item)
        {
            _repository.Add(item);
            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] TodoItem item)
        {
            if (id != item.Id) return BadRequest();
            return _repository.Update(item) ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) => _repository.Delete(id) ? NoContent() : NotFound();
    }
}
