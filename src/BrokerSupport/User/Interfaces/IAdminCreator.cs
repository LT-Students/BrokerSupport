using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerSupport.User.Interfaces
{
  public interface IAdminCreator
  {
    Task<bool> ExecuteAsync(
      List<string> errors,
      string firstName,
      string middleName,
      string lastName,
      string email,
      string login,
      string password);
  }
}
