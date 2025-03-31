using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NAudio.Wave;

namespace Cybersecurity_Awareness_Bot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            VoiceGreeting();
            //DisplayAsciiArt();
            //TextUserInteractionGreeting();
        }

        private static void VoiceGreeting()
        {
            // Attempt to play the GREETING.mp3 audio file
            try
            {
                // Your NAudio WASAPI initialization code here
            }
            catch (DllNotFoundException ex)
            {
                Console.WriteLine("WASAPI not available, switching to an alternative: " + ex.Message);
                // Fallback to DirectSound
            }

            /*
            try
            {
                 var audioFile = new AudioFileReader("GREETING.mp3");
                 var outputDevice = new WaveOutEvent();
                outputDevice.Init(audioFile);
                outputDevice.Play();

                // Perform FFT and update spectrum in real-time
                while (audioFile.Position < audioFile.Length)
                {
                    var fftData = PerformFFT(audioFile); // Perform FFT on audio data
                    UpdateSpectrum(fftData);            // Visualize the spectrum data
                    Thread.Sleep(16);                   // ~60 FPS
                }

                // Stop the audio after playback
                outputDevice.Stop();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            */
        }

        // Placeholder for the FFT processing method
        private static float[] PerformFFT(AudioFileReader audioFile)
        {
            // Implement FFT analysis logic here
            // For now, return a dummy array representing frequency bins
            return new float[256];
        }

        // Placeholder for the spectrum visualization method
        private static void UpdateSpectrum(float[] fftData)
        {
            // Render the spectrum based on FFT data
            // Example: Print the maximum frequency amplitude to console (just for demonstration)
            float maxAmplitude = 0f;
            foreach (var amplitude in fftData)
            {
                if (amplitude > maxAmplitude) maxAmplitude = amplitude;
            }
            Console.WriteLine($"Max Amplitude: {maxAmplitude}");
        }
           /* //throw new NotImplementedException();
            // Load the Hello sound
            var audioFile = new AudioFileReader("GREETING.mp3");

            // Analyze audio using FFT
            var outputDevice = new WaveOutEvent();
            outputDevice.Init(audioFile);
            outputDevice.Play();

            // Perform FFT and visualize (pseudo-code)
            while (audioFile.Position < audioFile.Length)
            {
                var fftData = PerformFFT(audioFile); // Your FFT processing here
                UpdateSpectrum(fftData);            // Render the spectrum visualization
                Thread.Sleep(16);                   // ~60 FPS
            }
           
        }*/

        static void TextUserInteractionGreeting()
        {
            //add welcome + interactive xp
            string welcomeMessage =   "\n================================" 
                                    + "\n|        Hello there!          |"
                                    + "\n================================\n";
                foreach (char c in welcomeMessage)
                {
                    Console.Write(c);
                    Thread.Sleep(50); // type effect
                }
            welcomeMessage =  "\nloading..." + "\n[    ]" + "\n[=   ]" + "\n[==  ]" + "\n[=== ]" + "\n[====]" + "\n[=====]" + "\n[  ===]" + "\n[   ==]\n" + "SUCCESSFULLY LOADED!\n";
                foreach (char c in welcomeMessage)
                {
                    Console.Write(c);
                    Thread.Sleep(50); // type effect
                }
            Thread.Sleep(100);
            Console.WriteLine();
            Console.WriteLine("\n================================");
            Console.WriteLine();
            welcomeMessage = "👾 Welcome to the Cybersecurity Awareness Bot! 🛡️\n";
                foreach (char c in welcomeMessage)
                {
                    Console.Write(c);
                    Thread.Sleep(50); // type effect
                }
                Console.WriteLine("\n================================");
        }
        static void DisplayAsciiArt()
        {
            string[] asciiArt = { @"
                              
                                  _____                    _____                    _____                    _____                   _______                           _____            _____          
                                 /\    \                  /\    \                  /\    \                  /\    \                 /::\    \                         /\    \          /\    \         
                                /::\____\                /::\    \                /::\    \                /::\____\               /::::\    \                       /::\____\        /::\____\        
                               /::::|   |                \:::\    \              /::::\    \              /::::|   |              /::::::\    \                     /:::/    /       /::::|   |        
                              /:::::|   |                 \:::\    \            /::::::\    \            /:::::|   |             /::::::::\    \                   /:::/    /       /:::::|   |        
                             /::::::|   |                  \:::\    \          /:::/\:::\    \          /::::::|   |            /:::/~~\:::\    \                 /:::/    /       /::::::|   |        
                            /:::/|::|   |                   \:::\    \        /:::/__\:::\    \        /:::/|::|   |           /:::/    \:::\    \               /:::/    /       /:::/|::|   |        
                           /:::/ |::|   |                    \:::\    \      /::::\   \:::\    \      /:::/ |::|   |          /:::/    / \:::\    \             /:::/    /       /:::/ |::|   |        
                          /:::/  |::|___|______               \:::\    \    /::::::\   \:::\    \    /:::/  |::|___|______   /:::/____/   \:::\____\           /:::/    /       /:::/  |::|___|______  
                         /:::/   |::::::::\    \               \:::\    \  /:::/\:::\   \:::\    \  /:::/   |::::::::\    \ |:::|    |     |:::|    |         /:::/    /       /:::/   |::::::::\    \ 
                        /:::/    |:::::::::\____\_______________\:::\____\/:::/  \:::\   \:::\____\/:::/    |:::::::::\____\|:::|____|     |:::|    |        /:::/____/       /:::/    |:::::::::\____\
                        \::/    / ~~~~~/:::/    /\::::::::::::::::::/    /\::/    \:::\  /:::/    /\::/    / ~~~~~/:::/    / \:::\    \   /:::/    /         \:::\    \       \::/    / ~~~~~/:::/    /
                         \/____/      /:::/    /  \::::::::::::::::/____/  \/____/ \:::\/:::/    /  \/____/      /:::/    /   \:::\    \ /:::/    /           \:::\    \       \/____/      /:::/    / 
                                     /:::/    /    \:::\~~~~\~~~~~~                 \::::::/    /               /:::/    /     \:::\    /:::/    /             \:::\    \                  /:::/    /  
                                    /:::/    /      \:::\    \                       \::::/    /               /:::/    /       \:::\__/:::/    /               \:::\    \                /:::/    /   
                                   /:::/    /        \:::\    \                      /:::/    /               /:::/    /         \::::::::/    /                 \:::\    \              /:::/    /    
                                  /:::/    /          \:::\    \                    /:::/    /               /:::/    /           \::::::/    /                   \:::\    \            /:::/    /     
                                 /:::/    /            \:::\    \                  /:::/    /               /:::/    /             \::::/    /                     \:::\    \          /:::/    /      
                                /:::/    /              \:::\____\                /:::/    /               /:::/    /               \::/____/                       \:::\____\        /:::/    /       
                                \::/    /                \::/    /                \::/    /                \::/    /                 ~~                              \::/    /        \::/    /        
                                 \/____/                  \/____/                  \/____/                  \/____/                                                   \/____/          \/____/         
                                                                                                                                                                               
                        © 2025 MZAMO LM. All Rights Reserved." 
            };
            //string Display = Array.AsReadOnly(asciiArt); ;

            /*int Count = 1; /* set at 0
                            * CHANGE THIS VALUE >> FASTER DEBUG TIME
                            */
            foreach (string line in asciiArt)
            {
                foreach (char c in line)
                {
                    Console.Write(c);
                    //Thread.Sleep(1); // quick type effect
                    //Count--;   
                }
                Console.WriteLine(); // Nextln
            }
            Console.Clear();

            
            int screenWidth = Console.WindowWidth;
            int iCount = 0; // show ascii 10 times
            bool bLoopBreak = false; //bflag

            /*CODE TO MOVE THE ASCII AROUND
             * 
            for (int round = 0; round < 2; round++) // 2 right
            {
                if (LoopBreak == true) break; // loopkill

                for (int i = 0; i < screenWidth - 10; i++) // Move right
                {
                    if (LoopBreak == true) break; // loopkill
                    Console.Clear();
                    for (int j = 0; j < asciiArt.Length; j++)
                    {
                        if (LoopBreak == true) break; // loopkill
                        iCount++;
                        Console.SetCursorPosition(i, j);
                        Console.Write(asciiArt[j]);
                        if (iCount == 10)
                        {
                            LoopBreak = true; //ser to true so all other loops may break
                            break; //exit loop
                        }


                    }
                    Thread.Sleep(50); // Control speed of movement
                }
            */
            // Loop through the ASCII Art for movement
            for (int iRround = 0; iRround < 2; iRround++) // Move across twice
            {
                if (bLoopBreak) break; // Exit loop if flag is set

                for (int i = 0; i < screenWidth - 10; i++) // Move right
                {
                    if (bLoopBreak) break; // Exit loop if flag is set
                    Console.Clear();

                    // Create Display string from asciiArt with random colors for each character
                    string display = "";
                    Random random = new Random();

                    for (int j = 0; j < asciiArt.Length; j++)
                    {
                        if (bLoopBreak) break; // Exit loop if flag is set

                        iCount++;

                        // Random color change for each character
                        Console.ForegroundColor = (ConsoleColor)random.Next(1, 16); // Random color
                        display += asciiArt[j]; // Add character to the display string

                        // Set cursor position and display the art at the given position
                        Console.SetCursorPosition(i, j);
                        Console.Write(asciiArt[j]);

                        if (iCount == 10)
                        {
                            bLoopBreak = true; // Set flag to true to break all loops
                            break; // Exit the loop
                        }
                    }

                    Thread.Sleep(50); // Control the speed of movement
                }
            }

            // Reset the console color back to default after the loop
            Console.ResetColor();
        }
    }
        
    
}
