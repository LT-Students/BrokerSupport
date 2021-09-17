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
  public class GetImages<T> : IGetImages<T> where T : class
  {
    private readonly ILogger<GetImages<T>> _logger;
    private readonly IRequestClient<IGetImagesRequest> _requestClient;

    public GetImages(
      ILogger<GetImages<T>> logger,
      IRequestClient<IGetImagesRequest> requestClient)
    {
      _logger = logger;
      _requestClient = requestClient;
    }

    public async Task<List<ImageData>> ExecuteAsync(
      List<string> errors,
      List<Guid> imagesIds,
      ImageSource imageSource)
    {
      if (imagesIds == null || !imagesIds.Any())
      {
        return null;
      }

      try
      {
        var response =
          await _requestClient.GetResponse<IOperationResult<IGetImagesResponse>>(
           IGetImagesRequest.CreateObj(imagesIds, imageSource));

        if (response.Message.IsSuccess && response.Message.Body != null)
        {
          return response.Message.Body.ImagesData;
        }

        _logger.LogWarning(
          "Errors while getting images ids: {ImagesIds}.\n Reason: {Errors}",
          string.Join(", ", imagesIds),
          string.Join('\n', response.Message.Errors));
      }
      catch (Exception exc)
      {
        _logger.LogError(exc, "Сan't get images ids: {ImagesIds}.", string.Join(", ", imagesIds));
      }

      errors.Add("Сan't get images. Please try again later.");

      return null;
    }
  }
}
