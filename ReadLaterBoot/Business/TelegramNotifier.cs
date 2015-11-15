using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace ReadLaterBot.Business
{
    public class TelegramNotifier
    {
        public TelegramNotifier()
        {
            this.telegramConsumer = new Infrastructure.TelegramConsumer();
        }

        private Infrastructure.TelegramConsumer telegramConsumer = null;
        
        public void SendMessage(Update updateMessage, Entities.MessageType messageType)
        {
            string message = string.Empty;
            string commandListMessage = "Para salvar um link, envie /save [LINK] ou apenas me encaminhe a mensagem com o link à ser salvo. \nPara ver seus links salvos, envie /getlinks. \nPara remover um link, envie /remove [INDICE]. \nPara limpar a sua lista, envie /clear";

            switch (messageType)
            {
                case Entities.MessageType.Start:
                    message = string.Format("Olá {0}. Eu sou o Read Later Bot e vou ajudá-lo à salvar os seus link. \n\n{1}.", updateMessage.Message.Chat.FirstName, commandListMessage);
                    break;
                case Entities.MessageType.ClearList:
                    message = "Lista limpa com sucesso.";
                    break;
                case Entities.MessageType.SaveLink:
                    message = "Link salvo com sucesso.";
                    break;
                case Entities.MessageType.RemoveLink:
                    message = "Link removido com sucesso.";
                    break;
                case Entities.MessageType.InvalidCommand:
                    message = string.Format("Comando inválido. \n\n{0}.", commandListMessage);
                    break;
            }

            this.telegramConsumer.SendMessage(updateMessage.Message.Chat.Id, message);
        }
        
        public void SendLinksMessage(Update updateMessage, string[] links)
        {
            string message = string.Empty;

            if (links == null || links.Length == 0)
                message = "Você não possui nenhum link salvo. \nEnvie /save [LINK] ou apenas me encaminhe a mensagem com o link à ser salvo.";
            else
            {
                message = "Links salvos:\n";

                for(int i = 0; i < links.Length; i++)
                    message += string.Format("{0} - {1}\n", i + 1, links[i]);
            }

            this.telegramConsumer.SendMessage(updateMessage.Message.Chat.Id, message);
        }
    }
}
