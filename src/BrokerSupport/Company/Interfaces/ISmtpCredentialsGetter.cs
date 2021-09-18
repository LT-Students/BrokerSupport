using LT.DigitalOffice.Models.Broker.Responses.Company;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerSupport.Company.Interfaces
{
  public interface ISmtpCredentialsGetter
  {
    Task<IGetSmtpCredentialsResponse> ExecuteAsync(
      List<string> errors);
  }
}
