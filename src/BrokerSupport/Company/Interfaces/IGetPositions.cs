using LT.DigitalOffice.Models.Broker.Models.Company;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerSupport.Company.Interfaces
{
  public interface IGetPositions<T> where T : class
  {
    Task<List<PositionData>> ExecuteAsync(
      List<string> errors,
      List<Guid> positionsIds = null);
  }
}
