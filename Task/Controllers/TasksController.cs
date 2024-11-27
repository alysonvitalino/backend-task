using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task.Models;

namespace Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private static List<ModelTask> listaTasks = new List<ModelTask>();

        [HttpGet]
        public ActionResult<List<ModelTask>> BuscarTasks()
        {
            return Ok(listaTasks);
        }

        [HttpPost]
        public ActionResult<List<ModelTask>> AdicionarTask( ModelTask novo)
        {
            if (novo.Id == 0 && listaTasks.Count > 0)
                novo.Id = listaTasks[listaTasks.Count - 1].Id + 1;

            if (novo.Description.Length < 10)
                return BadRequest("Descrição Insuficiente (Min 10 caracteres)");

            listaTasks.Add(novo);
            return Ok(listaTasks);
        }

        [HttpDelete("{id}")]
        public ActionResult DeletarTask(int id)
        {
            var pesquisa = listaTasks.Find(x => x.Id == id);

            if (pesquisa == null)
                return NotFound("Task Não encontrada");

            listaTasks.Remove(pesquisa);

            return Ok("Task: '" + pesquisa.Title + "' deletada com sucesso.");
        }
    }
}
