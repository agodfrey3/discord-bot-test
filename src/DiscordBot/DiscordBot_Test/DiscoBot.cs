using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot_Test
{
    public class DiscoBot
    {
        private DiscordClient bot;

        public DiscoBot()
        {
            bot = new DiscordClient();

            bot.ExecuteAndWait(async() =>
            {
                await bot.Connect("USN", "PW");
            });
        }
    }

}

