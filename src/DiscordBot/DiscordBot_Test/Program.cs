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
        //runs python file to scrape clan size from RS page
        //--------------------------------------------------------------------------------------------------
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
        //--------------------------------------------------------------------------------------------------

        //runs python file to scrape clan size from RS page
        //-------------------------------------------------------------------------------------------------
        static void itemToFile(string a)
        {
            System.IO.File.WriteAllText(@"item.txt", a);
        }
        //-------------------------------------------------------------------------------------------------

        static void Main(string[] args)
        {

            //creates a new bot client
            //--------------------------------------------------------------------------------------------------
            var client = new DiscordClient();

            client.UsingCommands(x => {
                x.PrefixChar = '$';
                x.HelpMode = HelpMode.Public;
            });
            //--------------------------------------------------------------------------------------------------


            //COMMANDS
            //--------------------------------------------------------------------------------------------------

            //Greeting
            client.GetService<CommandService>().CreateCommand("greet") //create command greet
                .Alias(new string[] { "greet", "welcome" }) //add 2 aliases, so it can be run with greet or welcome
                .Description("Greets a person.") //add description, it will be shown when ~help is used
                .Parameter("GreetedPerson", ParameterType.Unparsed) //as an argument, we have a person we want to greet
                .Do(async e =>
             {
                 await e.Channel.SendMessage($"{e.User.Name} welcomes {e.GetArg("GreetedPerson")}");
                 //sends a message to channel with the given text
             });
            //--------------------------------------------------------------------------------------------------

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
            //--------------------------------------------------------------------------------------------------

            //Fetches GE Price
            client.GetService<CommandService>().CreateCommand("price") //create command price
                .Alias(new string[] { "price", "value" }) //add 2 aliases, so it can be run with price or value
                .Description("Outputs price of item. Please use an underscore to indicate a space in an item name.") //adds description, it will be shown when ~help is used
                .Parameter("item", ParameterType.Unparsed) //takes in the item we wish to price check
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

            //Fetches Araxxor Rotation
            client.GetService<CommandService>().CreateCommand("araxxor") //create command price
                .Alias(new string[] { "araxxor", "araxxi" }) //add 2 aliases
                .Description("Finds the paths open.") //adds description, it will be shown when ~help is used
                .Parameter("None", ParameterType.Optional) //takes in the item we wish to price check
                .Do(async e =>
                {
                    Console.WriteLine($"Finding Paths for Araxxor");
                    string one = "C:\\Users\\God\\AppData\\Local\\Enthought\\Canopy\\User\\Scripts\\python.exe";
                    string two = "getPaths.py";
                    run_cmd(one, two);
                    string path1 = System.IO.File.ReadAllText("path1.txt");
                    string path2 = System.IO.File.ReadAllText("path2.txt");
                    string path3 = System.IO.File.ReadAllText("path3.txt");

                    await e.Channel.SendMessage($"|Path 1: {path1}|  |Path 2: {path2}|  |Path 3: {path3}|");//sends a message to channel with the given text
                    Console.WriteLine("Finished finding paths");
                });

            //HELLO
            client.GetService<CommandService>().CreateCommand("hello") //create command hello
                .Alias(new string[] { "hello", "noobs" }) //add 2 aliases
                .Description("Hello....") //adds description, it will be shown when ~help is used
                .Parameter("None", ParameterType.Optional) //takes in the item we wish to price check
                .Do(async e =>
                {
                    Console.WriteLine($"Saying Hello");
                    await e.Channel.SendFile("hello.mp3");//sends a file
                    Console.WriteLine("Finished finding paths");
                });
            
            //TTS
            client.GetService<CommandService>().CreateCommand("TextToSpeech") //create command hello
                .Alias(new string[] { "tts", "say" }) //add 2 aliases
                .Description("Reads out a message") //adds description, it will be shown when ~help is used
                .Parameter("Message", ParameterType.Unparsed) //takes in the item we wish to price check
                .Do(async e =>
                {
                    Console.WriteLine($"Performing text to speech with line: {e.GetArg("Message")}");
                    await e.Message.Channel.SendTTSMessage($"{e.GetArg("Message")}");//Text to speech line
                    Console.WriteLine("Finished performing text to speech.");
                });



            //logs all messages in the console
            client.Log.Message += (s, e) => Console.WriteLine($"[{e.Severity}] {e.Source}: {e.Message}");
            //--------------------------------------------------------------------------------------------------


            //Connects to server with specified account 
            //--------------------------------------------------------------------------------------------------
            client.ExecuteAndWait(async () =>
            {
                await client.Connect("ParadigmShiftBot@gmail.com", "Cutter35");
            });
            //--------------------------------------------------------------------------------------------------
        }
    }
}

