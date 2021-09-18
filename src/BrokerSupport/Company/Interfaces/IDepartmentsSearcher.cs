using LT.DigitalOffice.Models.Broker.Responses.Search;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerSupport.Company.Interfaces
{
  public interface IDepartmentsSearcher
  {
    Task<ISearchResponse> ExecuteAsync(
      List<string> errors,
      string value);
  }
}
