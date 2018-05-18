using System.Drawing;
using System.Threading.Tasks;
using SlackBotCore;
using SlackBotCore.EventObjects;
using SlackBotCore.Objects;

namespace MonitorARM
{
    public class SlackBotUtils
    {
        private const string _channelId = "-";
        private const string _token = "-";

        public SlackBot GetBot()
        {
            return new SlackBot(_token);
        }

        public async Task SendMessage(string message)
        {
            var bot = GetBot();

            await bot.Connect();

            await bot.Team.GetChannel(_channelId).SendMessageAsync(message);

            await bot.Disconnect();
        }

        public async Task SendMessageAttachment()
        {
            var bot = GetBot();

            await bot.Connect();

            var attachment = new SlackAttachment()
            {
                Color = Color.Blue
            };
            attachment.Fields.Add(new SlackAttachmentField("Field 1", "LALALALALALALALALALALALALALA"));
            attachment.Fields.Add(new SlackAttachmentField("Field 2", "EFGH", true));
            attachment.Fields.Add(new SlackAttachmentField("Field 3", "IJKL", true));

            attachment.ImageUrl = "https://pldh.net/media/dreamworld/143.png";

            var msg = await bot.Team.GetChannel(_channelId).SendMessageAsync("*ATTACHMENT!*", attachment);

            await bot.Disconnect();
        }

        public async Task SendAndReceiveMessage()
        {
            var bot = GetBot();

            bot.MessageReceived += Bot_MessageReceived;

            await bot.Connect();

            // var msg = await bot.SendMessageAsync(bot.Team.GetChannel(_channelId), "Ola BOT!");

            // await bot.Disconnect();
        }

        private void Bot_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}