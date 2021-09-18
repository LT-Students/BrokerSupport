using BrokerSupport.Extension;
using BrokerSupport.Image.Interfaces;
using LT.DigitalOffice.Models.Broker.Enums;
using LT.DigitalOffice.Models.Broker.Requests.Image;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrokerSupport.Image
{
  public class ImagesRemover : IImagesRemover
  {
    private readonly ILogger<ImagesRemover> _logger;
    private readonly IRequestClient<IRemoveImagesRequest> _requestClient;

    public ImagesRemover(
      ILogger<ImagesRemover> logger,
      IRequestClient<IRemoveImagesRequest> requestClient)
    {
      _logger = logger;
      _requestClient = requestClient;
    }

    public async Task<bool> ExecuteAsync(
      List<string> errors,
      List<Guid> imagesIds,
      ImageSource imageSource)
    {
      if (imagesIds == null || !imagesIds.Any() || imagesIds.Contains(Guid.Empty))
      {
        return false;
      }

      return await _requestClient.ProcessRequest<IRemoveImagesRequest, bool>(
        IRemoveImagesRequest.CreateObj(
          imagesIds,
          imageSource),
        errors,
        _logger);
    }
  }
}
