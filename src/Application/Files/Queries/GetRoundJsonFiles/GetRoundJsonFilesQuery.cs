using WorldDoomLeague.Application.Common.Interfaces;
using WorldDoomLeague.Application.Common.Security;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using WorldDoomLeague.Application.ConfigModels;

namespace WorldDoomLeague.Application.Files.Queries.GetRoundJsonFiles
{
    //[Authorize(Roles = "Administrator,StatsRunner")]
    public class GetRoundJsonFilesQuery : IRequest<IEnumerable<string>>
    {
    }

    public class GetRoundJsonFilesQueryHandler : IRequestHandler<GetRoundJsonFilesQuery, IEnumerable<string>>
    {
        private readonly IDirectoryBrowser _directory;
        private readonly IOptionsSnapshot<DataDirectories> _optionsDelegate;

        public GetRoundJsonFilesQueryHandler(IDirectoryBrowser directory, IOptionsSnapshot<DataDirectories> optionsDelegate)
        {
            _directory = directory;
            _optionsDelegate = optionsDelegate;
        }

        public async Task<IEnumerable<string>> Handle(GetRoundJsonFilesQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_directory.GetDirectoryListing(_optionsDelegate.Value.JsonMatchDirectory, "*.json"));
        }
    }
}
