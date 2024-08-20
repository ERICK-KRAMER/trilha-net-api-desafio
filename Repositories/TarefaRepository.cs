using Microsoft.EntityFrameworkCore;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Models;
using TrilhaApiDesafio.Repositories.Interfaces;

namespace TrilhaApiDesafio.Repositories
{
    public class TarefaRepository : ITarefaRepository
    {
        private readonly OrganizadorContext _dbContext;
        public TarefaRepository(OrganizadorContext context)
        {
            _dbContext = context;
        }
        public async Task<Tarefa> Atualizar(int id, Tarefa tarefa)
        {
            Tarefa tarefaAlreadyExist = await _dbContext.Tarefas.FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception("Tarefa não existe no banco de dados!");
            tarefaAlreadyExist.Data = DateTime.Now;
            tarefaAlreadyExist.Titulo = tarefa.Titulo;
            tarefaAlreadyExist.Status = tarefa.Status;
            tarefaAlreadyExist.Descricao = tarefa.Descricao;

            _dbContext.Tarefas.Update(tarefaAlreadyExist);
            await _dbContext.SaveChangesAsync();
            return tarefaAlreadyExist;
        }
        public async Task<(string message, Tarefa tarefa)> Criar(Tarefa tarefa)
        {
            await _dbContext.Tarefas.AddAsync(tarefa);
            await _dbContext.SaveChangesAsync();
            return (message: "Tareafa criada com sucesso!", tarefa);
        }
        public async Task<(string message, Tarefa tarefa)> Deletar(int id)
        {
            Tarefa tarefa = await _dbContext.Tarefas.FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception("Not found!");
            _dbContext.Tarefas.Remove(tarefa);
            await _dbContext.SaveChangesAsync();
            return (message: "Tarefa deletada com sucesso!", tarefa);
        }
        public async Task<Tarefa> ObterPorData(DateTime data)
        {
            Tarefa tarefa = await _dbContext.Tarefas.FirstOrDefaultAsync(x => x.Data == data) ?? throw new Exception("Not found!");
            return tarefa;
        }
        public async Task<Tarefa> ObterPorId(int id)
        {
            Tarefa findTarefa = await _dbContext.Tarefas.FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception("Not found!");
            return findTarefa;
        }
        public async Task<List<Tarefa>> ObterPorStatus(EnumStatusTarefa status)
        {
            List<Tarefa> tarefas = await _dbContext.Tarefas
                .Where(x => x.Status == status)
                .ToListAsync() ?? throw new Exception("Not found!");
            return tarefas;
        }
        public async Task<Tarefa> ObterPorTitulo(string titulo)
        {
            Tarefa tarefa = await _dbContext.Tarefas.FirstOrDefaultAsync(x => x.Titulo == titulo) ?? throw new Exception("Not found!");
            return tarefa;
        }
        public async Task<List<Tarefa>> ObterTodos()
        {
            List<Tarefa> tarefas = await _dbContext.Tarefas.ToListAsync()
                ?? throw new Exception("Not Found!");
            return tarefas;
        }

    }
}