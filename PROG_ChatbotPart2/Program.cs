using System;
using System.Threading;
using System.Media;
using System.IO;
using System.Collections.Generic;
using System.Collections;

namespace PROG_ChatbotPart2
{
    class CyberAwarenessBot
    {
        delegate void ResponseHandler(string userInput);

        private static Dictionary<string, string> userMemory = new Dictionary<string, string>();

        private static Dictionary<string, List<string>> keywordResponses = new Dictionary<string, List<string>>()
        {
            {"password", new List<string>{
                "Make sure to use strong, unique passwords for each account. Avoid using personal details in your passwords. A strong password should be at least 12 characters long and include numbers, symbols, and both uppercase and lowercase letters.",
                "Consider using a passphrase instead of a password - something like 'PurpleTurtleEats42Apples!'",
                "Did you know the most common password is '123456'? Be sure to avoid simple sequences!"
            }},
            {"scam", new List<string>{
                "Scams often create a sense of urgency. Always take time to verify requests for money or information.",
                "If an offer seems too good to be true, it probably is! This is a common scam tactic.",
                "Never share personal information with unsolicited callers or emailers."
            }},
            {"privacy", new List<string>{
                "Review privacy settings on all your apps regularly - they often change with updates.",
                "Be careful what you share online. Even innocent-seeming information can be used maliciously.",
                "Consider using privacy-focused browsers like Firefox or Brave for better control over your data."
            }},
            {"phishing", new List<string>{
                "Check sender email addresses carefully - phishing emails often mimic legitimate ones with small typos.",
                "Hover over links before clicking to see the actual URL destination.",
                "Legitimate companies will never ask for sensitive information via email."
            }}
        };

        private static Dictionary<string, string> sentimentResponses = new Dictionary<string, string>()
        {
            {"worried", "It's completely understandable to feel that way. Let me help you feel more secure about this."},
            {"curious", "That's a great question! Here's what you should know about this topic."},
            {"frustrated", "I understand this can be frustrating. Cybersecurity can be complex, but I'll try to simplify it for you."},
            {"confused", "Let me break this down into simpler terms. The key things to remember are..."}
        };

        static void Main(string[] args)
        {
            DisplayAsciiLogo();

            Thread soundThread = new Thread(PlayVoiceGreeting);
            soundThread.Start();

            SimulateTyping("\nBot: Welcome to CyberX! I'm here to help you stay safe online. How can I assist you today?", 50);
            soundThread.Join();

            Console.Write("\nBot: Would you like my help? (yes/no): ");
            Console.Write("You: ");
            string helpResponse = Console.ReadLine().Trim().ToLower();

            if (helpResponse != "yes")
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                SimulateTyping("\nBot: Let me know if you'll need help with anything!", 50);
                Console.ResetColor();
                return;
            }

            if (!userMemory.ContainsKey("name"))
            {
                SimulateTyping("\nBot: What is your name?", 40);
                Console.Write("\nYou: ");
                string userName = Console.ReadLine().Trim();
                RememberUserInfo("name", userName);
            }

            if (!userMemory.ContainsKey("interest"))
            {
                SimulateTyping($"\nBot: Nice to meet you, {userMemory["name"]}! What is your favourite topic related to cybersecurity?", 40);
                Console.Write("\nYou: ");
                string favouriteTopic = Console.ReadLine().Trim().ToLower();
                RememberUserInfo("interest", favouriteTopic);
            }
            else
            {
                SimulateTyping($"\nBot: Welcome back, {userMemory["name"]}! Ready to continue our chat about {userMemory["interest"]}?", 40);
            }

            SimulateTyping("\nBot: You can:", 40);
            Console.WriteLine("1. Use the menu system");
            Console.WriteLine("2. Ask me anything about cybersecurity");
            Console.WriteLine("3. Exit");
            SimulateTyping("\nBot: Choose an option (1-3):", 40);

            bool continueChat = true;
            while (continueChat)
            {
                Console.Write("\nYou: ");
                string userInput = Console.ReadLine().Trim().ToLower();

                if (userInput == "exit" || userInput == "quit" || userInput == "3")
                {
                    SimulateTyping("\nBot: Thank you for chatting with CyberX! Stay safe online.", 50);
                    continueChat = false;
                    break;
                }

                if (userInput == "menu" || userInput == "1")
                {
                    ShowMenuWithDialogue();
                    SimulateTyping("\nBot: You can:", 40);
                    Console.WriteLine("1. Use the menu system");
                    Console.WriteLine("2. Ask me anything about cybersecurity");
                    Console.WriteLine("3. Exit");
                    SimulateTyping("\nBot: Choose an option (1-3):", 40);
                    continue;
                }

                if (userInput == "2")
                {
                    SimulateTyping("\nBot: Sure! What would you like to know about cybersecurity? (Type 'back' to return)", 40);

                    while (true)
                    {
                        Console.Write("\nYou: ");
                        string question = Console.ReadLine().Trim().ToLower();

                        if (question == "back")
                        {
                            SimulateTyping("\nBot: Returning to main options.", 40);
                            SimulateTyping("\nBot: You can:", 40);
                            Console.WriteLine("1. Use the menu system");
                            Console.WriteLine("2. Ask me anything about cybersecurity");
                            Console.WriteLine("3. Exit");
                            SimulateTyping("\nBot: Choose an option (1-3):", 40);
                            break;
                        }

                        if (question == "forget" || question == "clear memory" || question == "forget my data")
                        {
                            userMemory.Clear();
                            SimulateTyping("\nBot: I've forgotten your information for privacy. If you'd like, you can tell me again.", 40);
                            SimulateTyping("\nBot: What is your name?", 40);
                            Console.Write("You: ");
                            string newName = Console.ReadLine().Trim();
                            RememberUserInfo("name", newName);

                            SimulateTyping($"\nBot: Nice to meet you, {userMemory["name"]}! What is your favourite topic related to cybersecurity?", 40);
                            Console.Write("You: ");
                            string newInterest = Console.ReadLine().Trim().ToLower();
                            RememberUserInfo("interest", newInterest);

                            SimulateTyping("\nBot: Let's continue.", 40);
                            continue;
                        }

                        if (question.Contains("name"))
                        {
                            if (userMemory.ContainsKey("name"))
                            {
                                SimulateTyping($"\nBot: Your name is {userMemory["name"]}.", 40);
                            }
                            else
                            {
                                SimulateTyping("\nBot: I don't know your name yet. What should I call you?", 40);
                                Console.Write("You: ");
                                string newName = Console.ReadLine().Trim();
                                RememberUserInfo("name", newName);
                                continue;
                            }
                            continue;
                        }
                        if (question.Contains("favourite topic") || question.Contains("favorite topic") || question.Contains("interest") || question.Contains("topic"))
                        {
                            if (userMemory.ContainsKey("interest"))
                            {
                                SimulateTyping($"\nBot: Your favourite topic is {userMemory["interest"]}.", 40);
                            }
                            else
                            {
                                SimulateTyping("\nBot: I don't know your favourite topic yet. What do you like to talk about?", 40);
                                Console.Write("You: ");
                                string newInterest = Console.ReadLine().Trim().ToLower();
                                RememberUserInfo("interest", newInterest);
                                continue;
                            }
                            continue;
                        }
                        if (question.Contains("history"))
                        {
                            string name = userMemory.ContainsKey("name") ? userMemory["name"] : "user";
                            string interest = userMemory.ContainsKey("interest") ? userMemory["interest"] : "cybersecurity";
                            SimulateTyping($"\nBot: {name}, you mentioned earlier that your favourite topic is {interest}. Feel free to ask me more about it!", 40);
                            continue;
                        }

                        // Memory recall for interest expressed
                        if (question.Contains("interested in") || question.Contains("i like") || question.Contains("i love") || question.Contains("my favourite topic is"))
                        {
                            foreach (var topic in keywordResponses.Keys)
                            {
                                if (question.Contains(topic))
                                {
                                    RememberUserInfo("interest", topic);
                                    SimulateTyping($"\nBot: Great! I'll remember that you're interested in {topic}. It's a crucial part of staying safe online.", 40);
                                    break;
                                }
                            }
                        }

                        // Sentiment handling with contextual info for worries
                        bool sentimentHandled = false;
                        foreach (var sentiment in sentimentResponses.Keys)
                        {
                            if (question.Contains(sentiment))
                            {
                                sentimentHandled = true;
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                SimulateTyping($"Bot: {sentimentResponses[sentiment]}", 30);
                                Console.ResetColor();
                                if (sentiment == "worried")
                                {
                                    foreach (var key in keywordResponses.Keys)
                                    {
                                        if (question.Contains(key))
                                        {
                                            SimulateTyping($"Bot: If you're worried about {key}, consider reviewing your {key} protection strategies carefully.", 40);
                                            break;
                                        }
                                    }
                                }
                                break;
                            }
                        }

                        bool responseGiven = false;
                        foreach (var keyword in keywordResponses.Keys)
                        {
                            if (question.Contains(keyword))
                            {
                                var responses = keywordResponses[keyword];
                                string response = responses[new Random().Next(responses.Count)];

                                if (userMemory.ContainsKey("name"))
                                {
                                    response = $"{userMemory["name"]}, {response.ToLower()}";
                                }

                                if (userMemory.ContainsKey("interest") && userMemory["interest"] == keyword)
                                {
                                    response += $"\nSince you're interested in this, you might want to review the security settings on your accounts.";
                                }

                                Console.ForegroundColor = ConsoleColor.Cyan;
                                SimulateTyping($"Bot: {response}", 30);
                                Console.ResetColor();

                                responseGiven = true;

                                SimulateTyping("\nBot: Would you like me to explain more about this topic? (yes/no)", 40);
                                Console.Write("You: ");
                                string moreInfoResponse = Console.ReadLine().Trim().ToLower();
                                if (moreInfoResponse == "yes")
                                {
                                    string extraInfo = GetAdditionalInfo(responses, response);
                                    SimulateTyping($"Bot: {extraInfo}", 40);
                                }

                                SimulateTyping("\nBot: Feel free to ask me about another topic!", 40);
                                break;
                            }
                        }

                        if (!responseGiven && !sentimentHandled)
                        {
                            SimulateTyping("Bot: Sorry, I didn't understand that. Please ask about passwords, scams, privacy, or phishing.", 40);
                        }
                    }
                    continue;
                }

                SimulateTyping("Bot: Sorry, I didn't recognize that option. Please choose 1, 2, or 3, or type 'menu'.", 40);
            }

            SimulateTyping("\nBot: Thank you for chatting with CyberX! Stay safe online.", 50);
        }

        static string GetAdditionalInfo(List<string> responses, string lastResponse)
        {
            foreach (var resp in responses)
            {
                if (resp != lastResponse)
                {
                    return resp;
                }
            }
            return lastResponse;
        }

        static void ShowMenuWithDialogue()
        {
            SimulateTyping("\nBot: Here are your options:", 40);
            Console.WriteLine("1. Password Safety Tips");
            Console.WriteLine("2. Phishing Tips");
            Console.WriteLine("3. Scam Awareness Tips");
            Console.WriteLine("4. Online Privacy Tips");
            Console.WriteLine("5. Back to Chat");

            while (true)
            {
                Console.Write("You: ");
                string choice = Console.ReadLine().Trim();
                switch (choice)
                {
                    case "1":
                        DisplayPasswordSafetyTips();
                        break;
                    case "2":
                        DisplayPhishingTips();
                        break;
                    case "3":
                        DisplayScamTips();
                        break;
                    case "4":
                        DisplayPrivacyTips();
                        break;
                    case "5":
                        SimulateTyping("\nBot: Returning to chat. Ask me anything!", 40);
                        return;
                    default:
                        SimulateTyping("Bot: Sorry, I didn't understand that. Please choose 1 to 5.", 40);
                        break;
                }
            }
        }

        static void RememberUserInfo(string key, string value)
        {
            if (userMemory.ContainsKey(key))
            {
                userMemory[key] = value;
            }
            else
            {
                userMemory.Add(key, value);
            }

            if (key == "interest")
            {
                Console.ForegroundColor = ConsoleColor.Green;
                SimulateTyping($"Bot: I'll remember that you're interested in {value}.", 30);
                Console.ResetColor();
            }
        }

        static void PlayVoiceGreeting()
        {
            try
            {
                string audioFilePath = "welcome.wav";
                if (File.Exists(audioFilePath))
                {
                    SoundPlayer soundPlayer = new SoundPlayer(audioFilePath);
                    soundPlayer.PlaySync();
                }
                else
                {
                    Console.WriteLine("Bot: Welcome message audio file not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Bot: Error playing sound: {ex.Message}");
            }
        }

        static void SimulateTyping(string message, int delay)
        {
            foreach (char c in message)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
            Console.WriteLine();
        }

        static void DisplayAsciiLogo()
        {
            string asciiLogo = @"
CCCCC    Y   Y   BBBBB     EEEEE    RRRRR     X     X
C          Y Y    B    B    E        R   R      X   X
C           Y     BBBBB     EEEE     RRRRR        X
C           Y     B    B    E        R   R       X X
CCCCC      Y     BBBBB     EEEEE    R    R    X     X
";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(asciiLogo);
            Console.ResetColor();
            Console.WriteLine("Loading CyberX...");
            Thread.Sleep(1000);
        }

        static void DisplayPasswordSafetyTips()
        {
            SimulateTyping("\nBot: Password Safety Tips:", 50);
            Console.WriteLine("- Use at least 12 characters (longer passwords are better).");
            Console.WriteLine("- Include numbers, symbols, and both uppercase and lowercase letters.");
            Console.WriteLine("- Don't reuse passwords across multiple sites.");
            Console.WriteLine("- Use a password manager to store your passwords safely.");
        }

        static void DisplayPhishingTips()
        {
            SimulateTyping("\nBot: Phishing Attack Tips:", 50);
            Console.WriteLine("- Be cautious of unsolicited emails and messages asking for personal information.");
            Console.WriteLine("- Verify the sender's email address carefully.");
            Console.WriteLine("- Avoid clicking on links in suspicious emails, especially if they seem urgent.");
        }

        static void DisplayScamTips()
        {
            SimulateTyping("\nBot: Scam Awareness Tips:", 50);
            Console.WriteLine("- Be wary of messages or calls that create urgency or fear.");
            Console.WriteLine("- Do not give out personal or banking information over the phone or email.");
            Console.WriteLine("- Research unfamiliar companies before making purchases or donations.");
        }

        static void DisplayPrivacyTips()
        {
            SimulateTyping("\nBot: Online Privacy Tips:", 50);
            Console.WriteLine("- Regularly review app and website privacy settings.");
            Console.WriteLine("- Limit the amount of personal information you share online.");
            Console.WriteLine("- Use privacy-focused browsers and search engines when possible.");
        }
    }
}