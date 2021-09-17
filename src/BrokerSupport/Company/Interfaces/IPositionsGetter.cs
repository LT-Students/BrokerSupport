using LT.DigitalOffice.Models.Broker.Responses.Company;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerSupport.Company.Interfaces
{
  public interface IPositionsGetter
  {
    Task<IGetPositionsResponse> ExecuteAsync(
      List<string> errors,
      List<Guid> positionsIds = null);
  }
}
