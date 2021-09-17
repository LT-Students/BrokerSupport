using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerSupport.Message.Interfaces
{
  public interface ISmtpCredentialsUpdater
  {
    Task<bool> ExecuteAsync(
      List<string> errors,
      string host,
      int port,
      bool enableSsl,
      string email,
      string password);
  }
}
