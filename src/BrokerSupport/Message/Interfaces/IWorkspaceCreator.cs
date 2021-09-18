using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerSupport.Message.Interfaces
{
  public interface IWorkspaceCreator
  {
    Task<bool> ExecuteAsync(
      List<string> errors,
      string name,
      List<Guid> usersIds);
  }
}
