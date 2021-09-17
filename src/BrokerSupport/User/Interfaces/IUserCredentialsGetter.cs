using LT.DigitalOffice.Models.Broker.Responses.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerSupport.User.Interfaces
{
  public interface IUserCredentialsGetter
  {
    Task<IGetUserCredentialsResponse> ExecuteAsync(
      List<string> errors,
      string loginData);
  }
}
