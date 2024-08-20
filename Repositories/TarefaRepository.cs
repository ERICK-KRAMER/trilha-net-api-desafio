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

        public async Task<Tarefa> ObterPorData(DateTime data)
        {
            Tarefa tarefa = await _dbContext.Tarefas.FirstOrDefaultAsync(x => x.Data == data);
            return tarefa;
        }

        public async Task<Tarefa> ObterPorId(int id)
        {
            Tarefa findTarefa = await _dbContext.Tarefas.FirstOrDefaultAsync(x => x.Id == id);
            return findTarefa;
        }

        public async Task<Tarefa> ObterPorStatus(EnumStatusTarefa status)
        {
            Tarefa tarefa = await _dbContext.Tarefas.FirstOrDefaultAsync(x => x.Status == status);
            return tarefa;
        }

        public async Task<Tarefa> ObterPorTitulo(string titulo)
        {
            Tarefa tarefa = await _dbContext.Tarefas.FirstOrDefaultAsync(x => x.Titulo == titulo);
            return tarefa;
        }

        public async Task<List<Tarefa>> ObterTodos()
        {
            List<Tarefa> tarefas = await _dbContext.Tarefas.ToListAsync();
            return tarefas;
        }
    }
}