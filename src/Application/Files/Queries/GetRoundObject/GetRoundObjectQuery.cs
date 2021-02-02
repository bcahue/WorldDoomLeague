using WorldDoomLeague.Application.Common.Interfaces;
using WorldDoomLeague.Application.Common.Security;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using WorldDoomLeague.Domain.MatchModel;
using Microsoft.Extensions.Options;
using WorldDoomLeague.Application.ConfigModels;

namespace WorldDoomLeague.Application.Files.Queries.GetRoundObject
{
    [Authorize(Roles = "Administrator,StatsRunner")]
    public class GetRoundObjectQuery : IRequest<Round>
    {
        public string FileName { get; }

        public GetRoundObjectQuery(string fileName)
        {
            FileName = fileName;
        }
    }

    public class GetRoundJsonFilesQueryHandler : IRequestHandler<GetRoundObjectQuery, Round>
    {
        private readonly IGetMatchJson _getMatchJson;
        private readonly IOptionsSnapshot<DataDirectories> _optionsDelegate;

        public GetRoundJsonFilesQueryHandler(IGetMatchJson getMatchJson, IOptionsSnapshot<DataDirectories> optionsDelegate)
        {
            _getMatchJson = getMatchJson;
            _optionsDelegate = optionsDelegate;
        }

        public async Task<Round> Handle(GetRoundObjectQuery request, CancellationToken cancellationToken)
        {
            return await _getMatchJson.GetRoundObject(_optionsDelegate.Value.JsonMatchDirectory, request.FileName);
        }
    }
}
