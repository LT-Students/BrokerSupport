using LT.DigitalOffice.Models.Broker.Responses.Search;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerSupport.User.Interfaces
{
  public interface IUsersSearcher
  {
    Task<ISearchResponse> ExecuteAsync(
      List<string> errors,
      string value);
  }
}
