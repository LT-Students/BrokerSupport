using LT.DigitalOffice.Models.Broker.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerSupport.Company.Interfaces
{
  public interface IDepartmentsInspector
  {
    Task<ICheckDepartmentsExistence> ExecuteAsync(
      List<string> errors,
      List<Guid> departmentsIds);
  }
}
