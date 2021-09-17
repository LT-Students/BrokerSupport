using LT.DigitalOffice.Models.Broker.Responses.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerSupport.User.Interfaces
{
  public interface IUsersDataGetter
  {
    Task<IGetUsersDataResponse> ExecuteAsync(
      List<string> errors,
      List<Guid> usersIds);
  }
}
