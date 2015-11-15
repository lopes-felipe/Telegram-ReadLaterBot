using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace ReadLaterBot
{
    public class UpdateHandler
    {
        public void HandleUpdate(Update update)
        {
            Business.LinkManager linkManager = new Business.LinkManager();
            Business.TelegramNotifier telegramConsumer = new Business.TelegramNotifier();

            if (update.Message.LeftChatParticipant != null || update.Message.NewChatParticipant != null)
                return;

            string userID = update.Message.From.Id.ToString();
            string[] messageComponents = update.Message.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            
            string command = (messageComponents.Length > 0 ? messageComponents[0] : string.Empty);
            command = command.ToLower().Replace("@readlaterbot", string.Empty);

            string argument = (messageComponents.Length > 1 ? messageComponents[1] : string.Empty);

            if (string.IsNullOrEmpty(command) || 
                (command == "/save" && string.IsNullOrEmpty(argument)) ||
                (command == "/remove" && (string.IsNullOrEmpty(argument))))
            {
                telegramConsumer.SendMessage(update, Entities.MessageType.InvalidCommand);
            }

            if (command == "/start" || command == "/help")
            {
                telegramConsumer.SendMessage(update, Entities.MessageType.Start);
            }

            else if (command == "/getlinks")
            {
                string[] links = linkManager.GetLinks(userID);
                telegramConsumer.SendLinksMessage(update, links);
            }

            else if (command == "/clear")
            {
                linkManager.ClearList(userID);
                telegramConsumer.SendMessage(update, Entities.MessageType.ClearList);
            }

            else if (command == "/remove")
            {
                int linkIndex = int.MinValue;

                if (!int.TryParse(argument, out linkIndex))
                {
                    telegramConsumer.SendMessage(update, Entities.MessageType.InvalidCommand);
                    return;
                }

                linkManager.RemoveLink(userID, --linkIndex);
                telegramConsumer.SendMessage(update, Entities.MessageType.RemoveLink);
            }

            else if (command == "/save")
            {
                linkManager.SaveLink(userID, argument);
                telegramConsumer.SendMessage(update, Entities.MessageType.SaveLink);
            }

            else if (update.Message.ForwardFrom != null)
            {
                linkManager.SaveLink(userID, command);
                telegramConsumer.SendMessage(update, Entities.MessageType.SaveLink);
            }

            else
            {
                telegramConsumer.SendMessage(update, Entities.MessageType.InvalidCommand);
            }
        }
    }
}
