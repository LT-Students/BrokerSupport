using BrokerSupport.Image.Interfaces;
using LT.DigitalOffice.Kernel.Broker;
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
  public class RemoveImages<T> : IRemoveImages<T> where T : class
  {
    private readonly ILogger<RemoveImages<T>> _logger;
    private readonly IRequestClient<IRemoveImagesRequest> _requestClient;

    public RemoveImages(
      ILogger<RemoveImages<T>> logger,
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

      try
      {
        var response =
          await _requestClient.GetResponse<IOperationResult<bool>>(
           IRemoveImagesRequest.CreateObj(imagesIds, imageSource));

        if (response.Message.IsSuccess && response.Message.Body)
        {
          return true;
        }

        _logger.LogWarning(
          "Errors while removing images ids: {ImagesIds}.\n Reason: {Errors}",
          string.Join(", ", imagesIds),
          string.Join('\n', response.Message.Errors));
      }
      catch (Exception exc)
      {
        _logger.LogError(exc, "Сan't remove images ids: {ImagesIds}.", string.Join(", ", imagesIds));
      }

      errors.Add("Сan't remove images. Please try again later.");

      return false;
    }
  }
}
