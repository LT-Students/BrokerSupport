using BrokerSupport.Extension;
using BrokerSupport.Image.Interfaces;
using LT.DigitalOffice.Models.Broker.Enums;
using LT.DigitalOffice.Models.Broker.Requests.Image;
using LT.DigitalOffice.Models.Broker.Responses.Image;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrokerSupport.Image
{
  public class ImagesGetter : IImagesGetter
  {
    private readonly ILogger<ImagesGetter> _logger;
    private readonly IRequestClient<IGetImagesRequest> _requestClient;

    public ImagesGetter(
      ILogger<ImagesGetter> logger,
      IRequestClient<IGetImagesRequest> requestClient)
    {
      _logger = logger;
      _requestClient = requestClient;
    }

    public async Task<IGetImagesResponse> ExecuteAsync(
      List<string> errors,
      List<Guid> imagesIds,
      ImageSource imageSource)
    {
      if (imagesIds == null || !imagesIds.Any())
      {
        return default;
      }

      return await _requestClient.ProcessRequest<IGetImagesRequest, IGetImagesResponse>(
        IGetImagesRequest.CreateObj(
          imagesIds,
          imageSource),
        errors,
        _logger);
    }
  }
}
