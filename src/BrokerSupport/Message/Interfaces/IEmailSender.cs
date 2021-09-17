using LT.DigitalOffice.Models.Broker.Enums;
using LT.DigitalOffice.Models.Broker.Responses.Image;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerSupport.Message.Interfaces
{
  public interface IEmailSender
  {
    Task<ICreateImagesResponse> ExecuteAsync(
      List<string> errors,
      string email,
      Dictionary<string, string> templateValues,
      EmailTemplateType templateType = EmailTemplateType.Notification,
      string templateLanguage = "en",
      Guid? templateId = null);
  }
}
