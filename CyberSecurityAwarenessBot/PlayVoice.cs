namespace CyberSecurityAwarenessBot
{
    using System.Media;
    using System;
    using System.IO;

    internal class PlayVoice
    {
        //constructor
        public PlayVoice()
        {
            /*
            //path
            string sProjectPath = AppDomain.CurrentDomain.BaseDirectory;
            //checker
            Console.WriteLine(sProjectPath);
            //
            string sUpdatePath = sProjectPath.Replace("bin\\Debug\\","");
            //combine
            string sFullPath = Path.Combine(sUpdatePath, "GREETING.wav");
            Play_wav(sFullPath);
            */

            PlayMusic("GREETING.wav");
            //BotColor();
            Console.WriteLine("Greeting Audio");

        }
        public static void PlayMusic(string filepath)
        {
            SoundPlayer musicPlayer = new SoundPlayer();
            musicPlayer.SoundLocation = filepath;
            musicPlayer.Play();
        }

        /*private void Play_wav(string sFullPath)
        {
            try
            {
                using (SoundPlayer player = new SoundPlayer("GREETING.wav"))
                {
                    player.PlaySync();
                }
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }
        }*/
    }
}