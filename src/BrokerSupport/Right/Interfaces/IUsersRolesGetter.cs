using LT.DigitalOffice.Models.Broker.Responses.Rights;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerSupport.Right.Interfaces
{
  public interface IUsersRolesGetter
  {
    Task<IGetUserRolesResponse> ExecuteAsync(
      List<string> errors,
      List<Guid> usersIds);
  }
}
