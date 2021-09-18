using LT.DigitalOffice.Models.Broker.Enums;
using LT.DigitalOffice.Models.Broker.Responses.Image;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerSupport.Image.Interfaces
{
  public interface IImagesGetter
  {
    Task<IGetImagesResponse> ExecuteAsync(
      List<string> errors,
      List<Guid> imagesIds,
      ImageSource imageSource);
  }
}
