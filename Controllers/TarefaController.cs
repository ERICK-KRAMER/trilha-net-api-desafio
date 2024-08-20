using Microsoft.AspNetCore.Mvc;
using TrilhaApiDesafio.Models;
using TrilhaApiDesafio.Repositories.Interfaces;

namespace TrilhaApiDesafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly ITarefaRepository _tarefaRepository;

        public TarefaController(ITarefaRepository tarefaRepository)
        {
            _tarefaRepository = tarefaRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tarefa>> ObterPorId(int id)
        {
            Tarefa tarefa = await _tarefaRepository.ObterPorId(id);
            return Ok(tarefa);
        }

        [HttpGet("ObterTodos")]

        public async Task<ActionResult<List<Tarefa>>> ObterTodos()
        {
            List<Tarefa> tarefas = await _tarefaRepository.ObterTodos();
            return Ok(tarefas);
        }

        [HttpGet("ObterPorTitulo")]
        public async Task<ActionResult<Tarefa>> ObterPorTitulo(string titulo)
        {
            Tarefa tarefa = await _tarefaRepository.ObterPorTitulo(titulo);
            return Ok(tarefa);
        }

        [HttpGet("ObterPorData")]
        public async Task<ActionResult<Tarefa>> ObterPorData(DateTime data)
        {
            Tarefa tarefa = await _tarefaRepository.ObterPorData(data);
            return Ok(tarefa);
        }

        [HttpGet("ObterPorStatus")]
        public async Task<ActionResult<List<Tarefa>>> ObterPorStatus(EnumStatusTarefa status)
        {
            List<Tarefa> tarefas = await _tarefaRepository.ObterPorStatus(status);
            return Ok(tarefas);
        }

        [HttpPost]
        public async Task<ActionResult<(string message, Tarefa tarefa)>> Criar(Tarefa tarefa)
        {
            var newTarefa = await _tarefaRepository.Criar(tarefa);
            return Ok(newTarefa);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Tarefa>> Atualizar(int id, Tarefa tarefa)
        {
            Tarefa updateTarefa = await _tarefaRepository.Atualizar(id, tarefa);
            return Ok(updateTarefa);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<(string message, Tarefa tarefa)>> Deletar(int id)
        {
            var removeTarefa = await _tarefaRepository.Deletar(id);
            return Ok(removeTarefa);
        }
    }
}
