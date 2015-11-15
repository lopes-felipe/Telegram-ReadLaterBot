using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadLaterBot.Entities
{
    public enum MessageType
    {
        None = 0,

        Start = 1,

        SaveLink = 2,

        RemoveLink = 3,

        ClearList = 4,

        InvalidCommand = 5,
    }
}
