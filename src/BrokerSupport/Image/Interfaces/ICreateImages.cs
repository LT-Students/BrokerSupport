using LT.DigitalOffice.Models.Broker.Enums;
using LT.DigitalOffice.Models.Broker.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerSupport.Image.Interfaces
{
  public interface ICreateImages<T> where T : class
  {
    Task<List<Guid>> ExecuteAsync(
      List<string> errors,
      List<CreateImageData> images,
      ImageSource imageSource);
  }
}
