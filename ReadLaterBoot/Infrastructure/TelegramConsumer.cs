using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace ReadLaterBot.Infrastructure
{
    public class TelegramConsumer
    {
        public TelegramConsumer()
        {
            this.telegramBotAPI = new Telegram.Bot.Api("152546227:AAGocGrq7DjfcH6UB6l4sHgLaltm6wfTNtU");
        }

        private Telegram.Bot.Api telegramBotAPI = null;
        
        public void SendMessage(int chatID, string textMessage)
        {
            Task<Message> responseMessageTask = this.telegramBotAPI.SendTextMessage(chatID, textMessage);
            responseMessageTask.Wait();

            Message responseMessage = responseMessageTask.Result;

            if (responseMessage == null)
                throw new Exception(string.Format("Error sending message"));
        }

        public Update[] GetUpdates(int offset)
        {
            Task<Update[]> updatesTask = this.telegramBotAPI.GetUpdates(offset, 1000, 0);
            updatesTask.Wait();

            Update[] updates = updatesTask.Result;
            return updates;
        }
    }
}
