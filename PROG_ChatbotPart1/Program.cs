using System;
using System.Threading;
using System.Media;
using System.IO;

namespace PROG_ChatbotPart1
{
    class CyberAwarenessBot
    {
        static void Main(string[] args)
        {
           
            DisplayAsciiLogo();

          
            Thread soundThread = new Thread(PlayVoiceGreeting);
            soundThread.Start(); 

            SimulateTyping("\nHello, welcome to CyberX! I'm here to help you stay safe online.", 50);
            soundThread.Join(); 

            // Ask if the user would like help
            Console.Write("\nWould you like me to help you? (yes/no): ");
            string helpResponse = Console.ReadLine().Trim().ToLower();

            if (helpResponse != "yes")
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                SimulateTyping("\nAlright! If you ever need assistance, feel free to return. Stay safe online!", 50);
                Console.ResetColor();
                return;
            }

        
            Console.Write("\nPlease enter your name: ");
            string userName = Console.ReadLine();

           
            SimulateTyping($"\nHello, {userName}! I'm here to help you stay safe online. Let's get started!", 50);
            Console.ResetColor();

            // Main conversation loop
            bool continueChat = true;
            while (continueChat)
            {
                Console.Clear(); 

             
                Console.WriteLine("\nWhat would you like to learn about today?");
                Console.WriteLine("1. Password Safety");
                Console.WriteLine("2. Phishing Attacks");
                Console.WriteLine("3. Safe Browsing Tips");
                Console.WriteLine("4. Social Media Security");
                Console.WriteLine("5. Public Wi-Fi Risks");
                Console.WriteLine("6. Identifying Scams");
                Console.WriteLine("7. Exit");
                Console.Write("Choose an option (1-7): ");
                string userChoice = Console.ReadLine();

                
                switch (userChoice)
                {
                    case "1":
                        DisplayPasswordSafetyTips();
                        break;
                    case "2":
                        DisplayPhishingTips();
                        break;
                    case "3":
                        DisplaySafeBrowsingTips();
                        break;
                    case "4":
                        DisplaySocialMediaSecurityTips();
                        break;
                    case "5":
                        DisplayPublicWiFiRisks();
                        break;
                    case "6":
                        DisplayIdentifyingScamsTips();
                        break;
                    case "7":
                        continueChat = false;
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        SimulateTyping("\nThank you for chatting with CyberX! Stay safe online.", 50);
                        Console.ResetColor();
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        SimulateTyping("Oops! I didn't quite understand that. Please choose a number between 1 and 7.", 50);
                        Console.ResetColor();
                        break;
                }

                // Pause before continuing
                if (continueChat)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
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
                    Console.WriteLine("Welcome message audio file not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while trying to play the sound: {ex.Message}");
                Console.WriteLine("Please ensure the 'welcome.wav' file exists and is accessible.");
            }
        }

        static void SimulateTyping(string message, int delay)
        {
            foreach (char c in message)
            {
                Console.Write(c);
                Thread.Sleep(delay); // Small delay for typing effect
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
            Thread.Sleep(1000); // Shortened the delay for better UX
        }

        static void DisplayPasswordSafetyTips()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            SimulateTyping("\nPassword Safety Tips:", 50);
            Console.WriteLine("- Use at least 12 characters (longer passwords are better).");
            Console.WriteLine("- Include numbers, symbols, and both uppercase and lowercase letters.");
            Console.WriteLine("- Don't reuse passwords across multiple sites.");
            Console.WriteLine("- Use a password manager to store your passwords safely.");
            Console.ResetColor();
        }

        static void DisplayPhishingTips()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            SimulateTyping("\nPhishing Attack Tips:", 50);
            Console.WriteLine("- Be cautious of unsolicited emails and messages asking for personal information.");
            Console.WriteLine("- Verify the sender's email address carefully.");
            Console.WriteLine("- Avoid clicking on links in suspicious emails, especially if they seem urgent.");
            Console.WriteLine("- Look for spelling mistakes and grammatical errors in suspicious messages.");
            Console.ResetColor();
        }

        static void DisplaySafeBrowsingTips()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            SimulateTyping("\nSafe Browsing Tips:", 50);
            Console.WriteLine("- Use HTTPS websites for secure browsing (look for the padlock symbol).");
            Console.WriteLine("- Avoid downloading files or clicking on pop-up ads from unknown sources.");
            Console.WriteLine("- Keep your browser and antivirus software up-to-date.");
            Console.WriteLine("- Always log out of your accounts when you’re done, especially on public computers.");
            Console.ResetColor();
        }

        static void DisplaySocialMediaSecurityTips()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            SimulateTyping("\nSocial Media Security Tips:", 50);
            Console.WriteLine("- Keep your accounts private and limit who can see your posts.");
            Console.WriteLine("- Avoid sharing sensitive personal information online.");
            Console.WriteLine("- Use strong, unique passwords for each social media platform.");
            Console.WriteLine("- Be cautious when accepting friend requests from strangers.");
            Console.WriteLine("- Enable two-factor authentication (2FA) for added security.");
            Console.ResetColor();
        }

        static void DisplayPublicWiFiRisks()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            SimulateTyping("\nPublic Wi-Fi Risks:", 50);
            Console.WriteLine("- Avoid accessing sensitive accounts (like banking) on public Wi-Fi.");
            Console.WriteLine("- Use a VPN (Virtual Private Network) for secure browsing.");
            Console.WriteLine("- Turn off automatic Wi-Fi connections to prevent connecting to unsafe networks.");
            Console.WriteLine("- Be cautious of Wi-Fi networks that do not require a password.");
            Console.WriteLine("- Always log out of any accounts you access on public networks.");
            Console.ResetColor();

        }

        static void DisplayIdentifyingScamsTips()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            SimulateTyping("\nTips for Identifying Scams:", 50);
            Console.WriteLine("- Be skeptical of deals that seem too good to be true.");
            Console.WriteLine("- Never share personal or financial information with unknown contacts.");
            Console.WriteLine("- Look for poor grammar and spelling mistakes in scam messages.");
            Console.WriteLine("- Verify the authenticity of websites before making purchases.");
            Console.WriteLine("- Do not click on unknown links sent via email or social media.");
            Console.ResetColor();
        }
    }
}
