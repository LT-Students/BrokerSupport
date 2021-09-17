using LT.DigitalOffice.Models.Broker.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerSupport.Image.Interfaces
{
  public interface IRemoveImages<T> where T : class
  {
    Task<bool> ExecuteAsync(
      List<string> errors,
      List<Guid> imagesIds,
      ImageSource imageSource);
  }
}
