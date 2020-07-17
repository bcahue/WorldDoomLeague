using WorldDoomLeague.Application.TodoLists.Queries.ExportTodos;
using WorldDoomLeague.Application.Rounds.Queries.ExportRounds;
using System.Collections.Generic;

namespace WorldDoomLeague.Application.Common.Interfaces
{
    public interface ICsvFileBuilder
    {
        byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
        byte[] BuildRoundRecordsFile(IEnumerable<RoundRecord> records);
    }
}
