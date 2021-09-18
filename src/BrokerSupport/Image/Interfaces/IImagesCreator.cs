using LT.DigitalOffice.Models.Broker.Enums;
using LT.DigitalOffice.Models.Broker.Models;
using LT.DigitalOffice.Models.Broker.Responses.Image;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerSupport.Image.Interfaces
{
  public interface IImagesCreator
  {
    Task<ICreateImagesResponse> ExecuteAsync(
      List<string> errors,
      List<CreateImageData> images,
      ImageSource imageSource);
  }
}
