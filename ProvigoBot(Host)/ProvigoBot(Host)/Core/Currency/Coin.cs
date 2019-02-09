using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace ProvigoBot.Core.Currency
{
    public class Coin : ModuleBase<ShardedCommandContext>
    {
        //Shop buy
        [Group("Coins"), Alias("Coin"), Summary("Group to manage stuff to do with coins")]
        public class CoinsGroup : ModuleBase<ShardedCommandContext>
        {
            [Command(""), Alias("me", "my"), Summary("Show all your current stones")]
            public async Task me()
            {

            }

            [Command("give"), Alias("gift"), Summary("Used to give people coins")]
            public async Task Give(IUser User = null, int Amount = 0)
            {
                //coins give @name 1000
                //group cmd  user  amount

                //Checks
                //Does the user have permission?
                //Does the user have enough coins?
                if (User == null)
                {
                    await Context.Channel.SendMessageAsync(":x: You didn't mention a user to give the Coins to! Please use this syntax: P.Coins give **<@user>** <amount>");
                    return;
                }

                if (User.IsBot)
                {
                    await Context.Channel.SendMessageAsync(":x: Bots can't use money, **so don't ever do this again CUNT!**");
                    return;
                }

                if (Amount == 0)
                {
                    await Context.Channel.SendMessageAsync($":x: Invalid or NULL number, try using a **number** this time, {User.Username} will be sad if you don't!");
                    return;
                }

                SocketGuildUser User1 = Context.User as SocketGuildUser;
                if (!User1.GuildPermissions.Administrator)
                {
                    await Context.Channel.SendMessageAsync($":x: You don't have administrator permissions in this discord server! Ask a moderator or the owner to execute this command!");
                    return;
                }
                //Execution
                //calculation (games)
                //Telling the user what he has gotten

                //saving the data
                //save the data to the database
                //save a file

            }
        }
    }
}
