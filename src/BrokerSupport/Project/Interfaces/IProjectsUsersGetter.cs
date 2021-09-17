using LT.DigitalOffice.Models.Broker.Responses.Project;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerSupport.Project.Interfaces
{
  public interface IProjectsUsersGetter
  {
    Task<IGetProjectsUsersResponse> ExecuteAsync(
      List<string> errors,
      List<Guid> usersIds = null,
      List<Guid> projectsIds = null,
      int? skipCount = null,
      int? takeCount = null,
      bool includeDisactiveted = false);
  }
}
