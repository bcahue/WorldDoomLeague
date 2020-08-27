using WorldDoomLeague.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WorldDoomLeague.Application.Files.Queries.GetRoundJsonFiles
{
    public class GetRoundJsonFilesQuery : IRequest<IEnumerable<string>>
    {
        public string JsonDirectory { get; }

        public GetRoundJsonFilesQuery(string jsonDirectory)
        {
            JsonDirectory = jsonDirectory;
        }
    }

    public class GetRoundJsonFilesQueryHandler : IRequestHandler<GetRoundJsonFilesQuery, IEnumerable<string>>
    {
        private readonly IDirectoryBrowser _directory;

        public GetRoundJsonFilesQueryHandler(IDirectoryBrowser directory)
        {
            _directory = directory;
        }

        public async Task<IEnumerable<string>> Handle(GetRoundJsonFilesQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_directory.GetDirectoryListing(request.JsonDirectory, "*.json"));
        }
    }
}
