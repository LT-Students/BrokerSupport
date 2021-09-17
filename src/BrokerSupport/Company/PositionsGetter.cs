using BrokerSupport.Company.Interfaces;
using BrokerSupport.Extension;
using LT.DigitalOffice.Models.Broker.Requests.Company;
using LT.DigitalOffice.Models.Broker.Responses.Company;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerSupport.Company
{
  public class PositionsGetter : IPositionsGetter
  {
    private readonly ILogger<PositionsGetter> _logger;
    private readonly IRequestClient<IGetPositionsRequest> _requestClient;

    public PositionsGetter(
      ILogger<PositionsGetter> logger,
      IRequestClient<IGetPositionsRequest> requestClient)
    {
      _logger = logger;
      _requestClient = requestClient;
    }

    public async Task<IGetPositionsResponse> ExecuteAsync(
      List<string> errors,
      List<Guid> positionsIds = null)
    {
      return await _requestClient.ProcessRequest<IGetPositionsRequest, IGetPositionsResponse>(
        IGetPositionsRequest.CreateObj(
          positionsIds),
        errors,
        _logger);
    }
  }
}
