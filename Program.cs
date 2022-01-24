using System;
using System.Media;
using System.Text;
using System.IO;

namespace BingoGame
{
    class Program
    {
        static void Main(string[] args)
        {
            //Set the "resolution" to match our design
            Console.BufferWidth = Console.WindowWidth;
            Console.BufferHeight = Console.WindowHeight;

            //Background music
            if (OperatingSystem.IsWindows())
            {
                SoundPlayer soundPlayer = new SoundPlayer("Devonshire_Waltz_(Allegretto).wav");
                soundPlayer.Load();
                soundPlayer.PlayLooping();
            }

            //Start the game
            Game myGame = new Game();
            myGame.Start();
        }
    }
}
