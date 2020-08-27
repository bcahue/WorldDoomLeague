using WorldDoomLeague.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using WorldDoomLeague.Application.MatchModel;

namespace WorldDoomLeague.Application.Files.Queries.GetRoundObject
{
    public class GetRoundObjectQuery : IRequest<Round>
    {
        public string JsonDirectory { get; }
        public string FileName { get; }

        public GetRoundObjectQuery(string jsonDirectory, string fileName)
        {
            JsonDirectory = jsonDirectory;
            FileName = fileName;
        }
    }

    public class GetRoundJsonFilesQueryHandler : IRequestHandler<GetRoundObjectQuery, Round>
    {
        private readonly IGetMatchJson _getMatchJson;

        public GetRoundJsonFilesQueryHandler(IGetMatchJson getMatchJson)
        {
            _getMatchJson = getMatchJson;
        }

        public async Task<Round> Handle(GetRoundObjectQuery request, CancellationToken cancellationToken)
        {
            return await _getMatchJson.GetRoundObject(request.JsonDirectory, request.FileName);
        }
    }
}
