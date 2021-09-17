using BrokerSupport.Image.Interfaces;
using LT.DigitalOffice.Kernel.Broker;
using LT.DigitalOffice.Models.Broker.Enums;
using LT.DigitalOffice.Models.Broker.Models;
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
  public class CreateImages<T> : ICreateImages<T> where T : class
  {
    private readonly ILogger<CreateImages<T>> _logger;
    private readonly IRequestClient<ICreateImagesRequest> _requestClient;

    public CreateImages(
      ILogger<CreateImages<T>> logger,
      IRequestClient<ICreateImagesRequest> requestClient)
    {
      _logger = logger;
      _requestClient = requestClient;
    }

    public async Task<List<Guid>> ExecuteAsync(
      List<string> errors,
      List<CreateImageData> images,
      ImageSource imageSource)
    {
      if (images == null || !images.Any() || images.Contains(null))
      {
        return null;
      }

      try
      {
        var response =
          await _requestClient.GetResponse<IOperationResult<ICreateImagesResponse>>(
           ICreateImagesRequest.CreateObj(images, imageSource));

        if (response.Message.IsSuccess && response.Message.Body != null)
        {
          return response.Message.Body.ImagesIds;
        }

        _logger.LogWarning(
          "Errors while ctating images for sourse '{ImageSourse}'.\n Reason: {Errors}",
          imageSource,
          string.Join('\n', response.Message.Errors));
      }
      catch (Exception exc)
      {
        _logger.LogError(exc, "Сan't ctate images for sourse '{ImageSourse}'.", imageSource);
      }

      errors.Add("Сan't add images. Please try again later.");

      return null;
    }
  }
}
