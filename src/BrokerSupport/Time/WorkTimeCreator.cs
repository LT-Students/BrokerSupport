using BrokerSupport.Extension;
using BrokerSupport.Time.Interfaces;
using LT.DigitalOffice.Kernel.Broker;
using LT.DigitalOffice.Models.Broker.Requests.Time;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrokerSupport.Time
{
  public class WorkTimeCreator : IWorkTimeCreator
  {
    private readonly ILogger<WorkTimeCreator> _logger;
    private readonly IRequestClient<ICreateWorkTimeRequest> _requestClient;

    public WorkTimeCreator(
      ILogger<WorkTimeCreator> logger,
      IRequestClient<ICreateWorkTimeRequest> requestClient)
    {
      _logger = logger;
      _requestClient = requestClient;
    }

    public async Task<bool> ExecuteAsync(
      List<string> errors,
      Guid projectId,
      List<Guid> usersIds)
    {
      if (usersIds == null || !usersIds.Any() || usersIds.Contains(Guid.Empty))
      {
        return false;
      }

      return await _requestClient.ProcessRequest<ICreateWorkTimeRequest, bool> (
        ICreateWorkTimeRequest.CreateObj(
          projectId,
          usersIds),
        errors,
        _logger);
    }
  }
}
