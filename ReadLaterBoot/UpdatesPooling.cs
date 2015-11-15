using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace ReadLaterBot
{
    public class UpdatesPooling
    {
        public void Start()
        {
            Infrastructure.TelegramConsumer telegramConsumer = new Infrastructure.TelegramConsumer();
            UpdateHandler updateHandler = new UpdateHandler();

            int offeset = 0;

            while (true)
            {
                Update[] updates = telegramConsumer.GetUpdates(offeset);

                foreach (Update update in updates)
                {
                    updateHandler.HandleUpdate(update);

                    offeset = update.Id + 1;
                }

                Thread.Sleep(5 * 1000);
            }
        }
    }
}
