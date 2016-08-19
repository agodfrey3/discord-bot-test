using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Windows;


namespace DiscordBot_Test
{
    class Program
    {
        public class DiscoBot
        {
            private DiscordClient bot;

            public DiscoBot()
            {
                bot = new DiscordClient();

                bot.ExecuteAndWait(async () =>
                {
                    await bot.Connect("ParadigmShiftBot", "Cutter35");
                });

            }
            //runs python file to scrape clan size from RS page
            public static void run_cmd(string cmd, string args)
            {
                ProcessStartInfo start = new ProcessStartInfo();
                start.FileName = cmd;//cmd is full path to python.exe
                start.Arguments = args;//args is path to .py file 
                start.UseShellExecute = false;//forces to run py in py shell
                start.RedirectStandardOutput = true;//redirects to py shell
                using (Process process = Process.Start(start))
                {
                    using (StreamReader reader = process.StandardOutput)
                    {
                        string result = reader.ReadToEnd();
                        Console.Write(result);
                    }
                }
            }
            //runs python file to scrape clan size from RS page
            static void itemToFile(string a)
            {
                System.IO.File.WriteAllText(@"item.txt", a);
            }

            static void Main(string[] args)
            {

                //creates a new bot client
                var client = new DiscordClient();

                client.UsingCommands(x =>
                {
                    x.PrefixChar = '$';
                    x.HelpMode = HelpMode.Public;
                });


                //COMMANDS

                //Greeting
                client.GetService<CommandService>().CreateCommand("greet") //create command greet
                    .Alias(new string[] { "greet", "welcome" }) //add 2 aliases, so it can be run with greet or welcome
                    .Description("Greets a person.") //add description, it will be shown when ~help is used
                    .Parameter("GreetedPerson", ParameterType.Required) //as an argument, we have a person we want to greet
                    .Do(async e =>
                    {
                        await e.Channel.SendMessage($"{e.User.Name} welcomes {e.GetArg("GreetedPerson")}");
                    //sends a message to channel with the given text
                });

                //Fetch Clan Size
                client.GetService<CommandService>().CreateCommand("size") //create command size
                    .Alias(new string[] { "size", "clanSize" }) //add 2 aliases, so it can be run with size or clanSize
                    .Description("Outputs size of clan.") //adds description, it will be shown when ~help is used
                    .Parameter("clan", ParameterType.Optional) //person we wish to greet
                    .Do(async e =>
                    {
                        Console.WriteLine("Fetching Clan Size");
                        string one = "C:\\Users\\God\\AppData\\Local\\Enthought\\Canopy\\User\\Scripts\\python.exe";
                        string two = "getClanSize.py";
                        run_cmd(one, two);
                        string text = System.IO.File.ReadAllText("clanSize.txt");
                        await e.Channel.SendMessage($"Clan Size: {text}");//sends a message to channel with the given text
                    Console.WriteLine("Finished finding Clan Size");
                    });
                //Fetches GE Price
                client.GetService<CommandService>().CreateCommand("price") //create command price
                    .Alias(new string[] { "price", "value" }) //add 2 aliases, so it can be run with price or value
                    .Description("Outputs price of item. Please use an underscore to indicate a space in an item name.") //adds description, it will be shown when ~help is used
                    .Parameter("item", ParameterType.Required) //takes in the item we wish to price check
                    .Do(async e =>
                    {
                        Console.WriteLine($"Finding Item Price for {e.GetArg("item")}");
                        itemToFile(e.GetArg("item"));
                        string one = "C:\\Users\\God\\AppData\\Local\\Enthought\\Canopy\\User\\Scripts\\python.exe";
                        string two = "getPrice.py";
                        run_cmd(one, two);
                        string price = System.IO.File.ReadAllText("itemPrice.txt");

                        await e.Channel.SendMessage($"price of {e.GetArg("item")}: {price}");//sends a message to channel with the given text
                    Console.WriteLine("Finished finding price");
                    });
                //logs all messages in the console
                client.Log.Message += (s, e) => Console.WriteLine($"[{e.Severity}] {e.Source}: {e.Message}");


                //Connects to server with specified account 
                client.ExecuteAndWait(async () =>
                {
                    await client.Connect("ParadigmShiftBot@gmail.com", "Cutter35");
                });
            }

        }
    }
}

