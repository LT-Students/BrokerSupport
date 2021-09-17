using LT.DigitalOffice.Models.Broker.Enums;
using LT.DigitalOffice.Models.Broker.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerSupport.Image.Interfaces
{
  public interface IGetImages<T> where T : class
  {
    Task<List<ImageData>> ExecuteAsync(
      List<string> errors,
      List<Guid> imagesIds,
      ImageSource imageSource);
  }
}
