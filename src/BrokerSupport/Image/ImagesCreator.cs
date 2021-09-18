using BrokerSupport.Extension;
using BrokerSupport.Image.Interfaces;
using LT.DigitalOffice.Models.Broker.Enums;
using LT.DigitalOffice.Models.Broker.Models;
using LT.DigitalOffice.Models.Broker.Requests.Image;
using LT.DigitalOffice.Models.Broker.Responses.Image;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrokerSupport.Image
{
  public class ImagesCreator : IImagesCreator
  {
    private readonly ILogger<ImagesCreator> _logger;
    private readonly IRequestClient<ICreateImagesRequest> _requestClient;

    public ImagesCreator(
      ILogger<ImagesCreator> logger,
      IRequestClient<ICreateImagesRequest> requestClient)
    {
      _logger = logger;
      _requestClient = requestClient;
    }

    public async Task<ICreateImagesResponse> ExecuteAsync(
      List<string> errors,
      List<CreateImageData> images,
      ImageSource imageSource)
    {
      if (images == null || !images.Any() || images.Contains(null))
      {
        return null;
      }

      return await _requestClient.ProcessRequest<ICreateImagesRequest, ICreateImagesResponse>(
        ICreateImagesRequest.CreateObj(
          images,
          imageSource),
        errors,
        _logger);
    }
  }
}
