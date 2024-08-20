using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Repositories.Interfaces
{
    public interface ITarefaRepository
    {
        Task<Tarefa> ObterPorId(int id);
        Task<List<Tarefa>> ObterTodos();
        Task<Tarefa> ObterPorTitulo(string titulo);
        Task<Tarefa> ObterPorData(DateTime data);
        Task<List<Tarefa>> ObterPorStatus(EnumStatusTarefa status);
        Task<(string message, Tarefa tarefa)> Criar(Tarefa tarefa);
        Task<Tarefa> Atualizar(int id, Tarefa tarefa);
        Task<(string message, Tarefa tarefa)> Deletar(int id);
    }
}