using LT.DigitalOffice.Models.Broker.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerSupport.User.Interfaces
{
  public interface IUsersInspector
  {
    Task<ICheckUsersExistence> ExecuteAsync(
      List<string> errors,
      List<Guid> usersIds);
  }
}
