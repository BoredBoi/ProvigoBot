using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http;
using HtmlAgilityPack;
using Discord;
using Discord.Commands;

namespace ProvigoBot.Core.Commands
{
    public class HelloWorld : ModuleBase<SocketCommandContext>
    {
        [Command("Calling"), Alias("Call"), Summary("Hello world command")]
        public async Task Test()
        {
            await Context.Channel.SendMessageAsync($"command called at {DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}");
        }

        [Command("Embed"), Summary("Embed test command")]
        public async Task Embed([Remainder]string Input0 = "None")
        {
            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithThumbnailUrl("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ5vu330c0nfG4GyPJ4QTXcovCC6RMuE5-m7tT5PZflG2SwVidz");
            Embed.WithAuthor("Author : ", Context.User.GetAvatarUrl());
            Embed.WithColor(207, 70, 38);
            Embed.WithFooter($"I want to {Input0}", Context.Guild.Owner.GetAvatarUrl());
            Embed.WithDescription("**Dummy** description + \n" +
                        "( https://google.com/ )[favorite __website__] \n" +
                        "[cool website](https://google.com)");
            Embed.AddInlineField("User input :", Input0);

            await Context.Channel.SendMessageAsync("", false, Embed.Build());
        }

        [Command("SecretMeme"), Summary("Easter egg")]
        public async Task Secret()
        {
            await Context.Message.DeleteAsync();
            await Context.Channel.SendFileAsync("DannyDevito/boy1.png");
            await Context.Channel.SendFileAsync("DannyDevito/boy2.png");
            await Context.Channel.SendFileAsync("DannyDevito/boy3.png");
        }

        [Command("Random"), Alias("RNumber", "R"), Summary("Generate random number")]
        public async Task Generate()
        {
            Random r = new Random();
            int number = r.Next(1, 9999);
            Console.WriteLine($"case : {number}");
            await Context.Channel.SendMessageAsync($"Your number : {number}");

        }

        [Command("Help"), Alias("help"), Summary("Basic help command")]
        public async Task Help([Remainder]string Input1 = "")
        {
            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithAuthor("ProvigoBot :");
            Embed.WithColor(207, 70, 38);
            Embed.WithDescription("** Help Menu **\n Here's all the commands i offer, ~~exept for some hidden by my creator~~ \n \n" +
                "** [Click here to go to my website](https://bot4discord.wordpress.com/) ** \n" +
                "** Description** : What i do is explained here \n" +
                "** CheckPrice ** : Open products prices menu \n" +
                "** Disclaimer ** : read the disclaimer" +
                //following those are for testing the discord api and my knowlege
                "** Random **     : generate random number" +
                "** Hello **      : generate random hello" +
                "** Calling **    : for testing purpose" +
                "More coming soon if i feel like it...");


            await Context.Channel.SendMessageAsync("", false, Embed.Build());
        }

        [Command("Hello"), Alias("Oy", "Howdy", "hello", "oy", "howdy"), Summary("greeting someone")]
        public async Task Greeting()
        {
            Random r = new Random();
            int number = r.Next(1, 5);
            Console.WriteLine($"Somebody called Hello and got Greeting : {number}");
            string hellot = "";
            switch (number)
            {
                case 1:
                    hellot = "Howdy !";
                    break;
                case 2:
                    hellot = "Oy there !";
                    break;
                case 3:
                    hellot = "Greeting form Florida !";
                    break;
                case 4:
                    hellot = "want sum tea old champ ?";
                    break;
                default:
                    hellot = "Kangaroo and shit m8 !";
                    break;
            }
            await Context.Channel.SendMessageAsync($"{hellot}");
        }

        [Command("CheckPrice"), Summary("Check of the price of provigo products")]
        public async Task CheckPrice([Remainder]string Input2 = "")
        {
            var user = Context.User.Username;
            string Url1 = "";

            if (Input2 == "Mushrooms")
            {
                Url1 = "https://www.provigo.ca/Food/Fruits-%26-Vegetables/Vegetables/Mushrooms-%26-Truffles/White-Mushrooms%2C-Whole/p/20776239_EA";
            }

            if (Input2 == "")
            {
                EmbedBuilder Embed = new EmbedBuilder();
                Embed.WithAuthor("Price checking menu :");
                Embed.WithColor(207, 70, 38);
                Embed.WithDescription("** To check prices write \"P.CheckPrice NameOfTheProduct\"  **\n" +
                    "ex : P.CheckPrice Mushrooms \n \n" +
                    "All the product curently checkable : \n" +
                    "-------------------- \n" +
                    "Mushrooms \n");

                await Context.Channel.SendMessageAsync("", false, Embed.Build());
            }

            var httpClients = new HttpClient();
            var html = await httpClients.GetStringAsync(Url1);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var ProductHtmlRabais = htmlDocument.DocumentNode.DescendantsAndSelf("span")
                .Where(node => node.GetAttributeValue("class", "")
                .Equals("sale-price-text")).FirstOrDefault().InnerText;

            if (ProductHtmlRabais != "")
            {
                var ProductHtmlOld = htmlDocument.DocumentNode.DescendantsAndSelf("span")
                .Where(node => node.GetAttributeValue("class", "")
                .Equals("old-price-text")).FirstOrDefault().InnerText;
                Console.WriteLine("");
                await Context.Channel.SendMessageAsync($"Mushrooms are currently priced at { ProductHtmlRabais } due to a discount, they are normaly at { ProductHtmlOld } ");
                
            }
            else
            {

                var ProductHtmlReg = htmlDocument.DocumentNode.DescendantsAndSelf("span")
                    .Where(node => node.GetAttributeValue("class", "")
                    .Equals("reg-price-text")).FirstOrDefault().InnerText;

                Console.WriteLine("Presentement le prix est de : " + ProductHtmlReg);
                await Context.Channel.SendMessageAsync($"Mushroom are priced at { ProductHtmlReg } ");
            }

        }

        [Command("Description"), Summary("Description of the bot")]
        public async Task Describe()
        {
            await Context.Channel.SendMessageAsync("ProvigoBot is a bot used to show price of mushrooms at Provigo (At first this was only a joke between my friend and I)" +
                " personally this bot is there for two reason :\n**To learn new programmings skills and have fun while doing it!**");
        }

        [Command("Disclaimers"), Summary("Disclamer")]
        public async Task Disclaim()
        {
            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithAuthor("Disclaimers :");
            Embed.WithColor(207, 70, 38);
            Embed.WithDescription("This Bot is not in any way an Official Provigo product: " +
                "\nAny thing said or done by this Bot are not linked to Provigo" +
                "\nAny prices or official information come from " +
                "\n ** [The Provigo website](https://www.provigo.ca/) ** " +
                "\n OR" +
                "\n[The official twitter account](https://twitter.com/provigoqc?lang=en) " +
                "\n \n \n" +
                "I don't get any monetary incentive from doing this **I'm doing this for fun** \n" +
                "\n \n" +
                "This bot is used in a education way under the [Fair use laws](https://fair-dealing.ca/what-is-fair-dealing/) of Canada");


            await Context.Channel.SendMessageAsync("", false, Embed.Build());
        }
    }
}
   
        
