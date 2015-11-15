using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadLaterBot.Pooling
{
    class Program
    {
        static void Main(string[] args)
        {
            ReadLaterBot.UpdatesPooling updatesPooling = new UpdatesPooling();
            updatesPooling.Start();
        }
    }
}
