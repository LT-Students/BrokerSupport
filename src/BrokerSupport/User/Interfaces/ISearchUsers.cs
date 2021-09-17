using LT.DigitalOffice.Models.Broker.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerSupport.User.Interfaces
{
  public interface ISearchUsers<T> where T : class
  {
    Task<List<SearchInfo>> ExecuteAsync(
      List<string> errors,
      string value);
  }
}
