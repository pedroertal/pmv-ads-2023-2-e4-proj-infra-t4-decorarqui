using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace decorArqui.Models
{
    public class Whatsapp
    {
        private readonly string accountSid;
        private readonly string authToken;
        private readonly string twilioNumber;

        public Whatsapp(string accountSid, string authToken, string twilioNumber)
        {
            this.accountSid = accountSid;
            this.authToken = authToken;
            this.twilioNumber = twilioNumber;
        }

        public void IniciarConversa (string numeroCliente, string mensagem)
        {
            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: mensagem,
                from: new PhoneNumber($"whatsapp:{twilioNumber}"),
                to: new PhoneNumber($"whatsapp:{numeroCliente}")
            );
        }
    }
}
