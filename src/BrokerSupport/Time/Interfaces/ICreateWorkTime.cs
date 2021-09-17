using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerSupport.Time.Interfaces
{
  public interface ICreateWorkTime<T> where T : class
  {
    Task<bool> ExecuteAsync(
      List<string> errors,
      Guid projectId,
      List<Guid> usersIds);
  }
}
