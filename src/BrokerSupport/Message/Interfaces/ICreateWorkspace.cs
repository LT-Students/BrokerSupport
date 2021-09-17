using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerSupport.Message.Interfaces
{
  public interface ICreateWorkspace<T> where T : class
  {
    Task<bool> ExecuteAsync(
      List<string> errors,
      string name,
      List<Guid> usersIds);
  }
}
