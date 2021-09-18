using LT.DigitalOffice.Models.Broker.Responses.Search;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerSupport.Project.Interfaces
{
  public interface IProjectsSearcher
  {
    Task<ISearchResponse> ExecuteAsync(
      List<string> errors,
      string value);
  }
}
