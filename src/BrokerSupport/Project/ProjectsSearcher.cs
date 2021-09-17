using BrokerSupport.Extension;
using BrokerSupport.Project.Interfaces;
using LT.DigitalOffice.Models.Broker.Requests.Project;
using LT.DigitalOffice.Models.Broker.Responses.Search;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerSupport.Project
{
  public class ProjectsSearcher : IProjectsSearcher
  {
    private readonly ILogger<ProjectsSearcher> _logger;
    private readonly IRequestClient<ISearchProjectsRequest> _requestClient;

    public ProjectsSearcher(
      ILogger<ProjectsSearcher> logger,
      IRequestClient<ISearchProjectsRequest> requestClient)
    {
      _logger = logger;
      _requestClient = requestClient;
    }

    public async Task<ISearchResponse> ExecuteAsync(List<string> errors, string value)
    {
      if (string.IsNullOrEmpty(value))
      {
        return null;
      }

      return await _requestClient.ProcessRequest<ISearchProjectsRequest, ISearchResponse>(
        ISearchProjectsRequest.CreateObj(
          value),
        errors,
        _logger);
    }
  }
}
