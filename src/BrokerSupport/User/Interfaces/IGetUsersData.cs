using LT.DigitalOffice.Models.Broker.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerSupport.User.Interfaces
{
  public interface IGetUsersData<T> where T : class
  {
    Task<List<UserData>> ExecuteAsync(
      List<string> errors,
      List<Guid> usersIds);
  }
}
