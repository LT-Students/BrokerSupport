using LT.DigitalOffice.Models.Broker.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerSupport.Right.Interfaces
{
  public interface IGetUsersRoles<T> where T : class
  {
    Task<List<RoleData>> ExecuteAsync(
      List<string> errors,
      List<Guid> usersIds);
  }
}
