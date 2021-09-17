using LT.DigitalOffice.Models.Broker.Responses.Project;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerSupport.Project.Interfaces
{
  public interface IProjectsGetter
  {
    Task<IGetProjectsResponse> ExecuteAsync(
      List<string> errors,
      List<Guid> projectsIds = null,
      Guid? departmentId = null,
      Guid? userId = null,
      bool includeUsers = false,
      int? skipCount = null,
      int? takeCount = null);
  }
}
