using LT.DigitalOffice.Models.Broker.Responses.Company;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerSupport.Company.Interfaces
{
  public interface IDepartmentsGetter
  {
    Task<IGetDepartmentsResponse> ExecuteAsync(
      List<string> errors,
      List<Guid> departmentsIds = null);
  }
}
