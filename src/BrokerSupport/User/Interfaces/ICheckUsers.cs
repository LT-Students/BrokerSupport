using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerSupport.User.Interfaces
{
  public interface ICheckUses<T> where T : class
  {
    Task<List<Guid>> ExecuteAsync(
      List<string> errors,
      List<Guid> usersIds);
  }
}
