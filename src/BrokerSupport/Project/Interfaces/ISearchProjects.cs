using LT.DigitalOffice.Models.Broker.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerSupport.Project.Interfaces
{
  public interface ISearchProjects<T> where T : class
  {
    Task<List<SearchInfo>> ExecuteAsync(
      List<string> errors,
      string value);
  }
}
