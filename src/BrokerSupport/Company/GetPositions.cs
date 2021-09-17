using BrokerSupport.Company.Interfaces;
using LT.DigitalOffice.Kernel.Broker;
using LT.DigitalOffice.Models.Broker.Models.Company;
using LT.DigitalOffice.Models.Broker.Requests.Company;
using LT.DigitalOffice.Models.Broker.Responses.Company;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerSupport.Company
{
  public class GetPositions<T> : IGetPositions<T> where T : class
  {
    private readonly ILogger<GetPositions<T>> _logger;
    private readonly IRequestClient<IGetPositionsRequest> _requestClient;

    public GetPositions(
      ILogger<GetPositions<T>> logger,
      IRequestClient<IGetPositionsRequest> requestClient)
    {
      _logger = logger;
      _requestClient = requestClient;
    }

    public async Task<List<PositionData>> ExecuteAsync(
      List<string> errors,
      List<Guid> positionsIds = null)
    {
      try
      {
        var response = await _requestClient.GetResponse<IOperationResult<IGetPositionsResponse>>(
          IGetPositionsRequest.CreateObj(positionsIds));

        if (response.Message.IsSuccess && response.Message.Body != null)
        {
          return response.Message.Body.Positions;
        }

        if (positionsIds != null)
        {
          _logger.LogWarning(
            "Errors while getting positions with positions ids: {PositionsIds}.\n Reason: {Errors}",
            string.Join(", ", positionsIds),
            string.Join('\n', response.Message.Errors));
        }
        else
        {
          _logger.LogWarning(
            "Errors while getting all positions.\n Reason: {Errors}",
            string.Join('\n', response.Message.Errors));
        }
      }
      catch (Exception exc)
      {
        if (positionsIds != null)
        {
          _logger.LogError(exc, "Сan't get positions with positions ids: {PositionsIds}.", positionsIds);
        }
        else
        {
          _logger.LogError(exc, "Сan't get all positions.");
        }
      }

      errors.Add("Сan't get department users. Please try again later.");

      return null;
    }
  }
}
