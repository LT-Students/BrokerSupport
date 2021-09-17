using LT.DigitalOffice.Models.Broker.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerSupport.Message.Interfaces
{
  public interface IEmailSender
  {
    Task<bool> ExecuteAsync(
      List<string> errors,
      string email,
      Dictionary<string, string> templateValues,
      EmailTemplateType templateType = EmailTemplateType.Notification,
      string templateLanguage = "en",
      Guid? templateId = null);
  }
}
