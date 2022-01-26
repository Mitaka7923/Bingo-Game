using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bingo_TheGame;

namespace Bingo_TheGame
{
    //Testing class
    class SetValues
    {
        public static void SaveFiles(string creditsPath, string houseCreditsPath, string isSecretPath, int credits, int houseCredits, bool isSecret)
        {
            //Save the credits
            File.WriteAllText(creditsPath, Convert.ToString(credits));
            //Save the house credits
            File.WriteAllText(houseCreditsPath, Convert.ToString(houseCredits));
            //Save secret value
            File.WriteAllText(isSecretPath, Convert.ToString(isSecret));
        }

        public static void SetInitialValuesToFiles(string creditsPath, string houseCreditsPath, string isSecretPath, int credits, int houseCredits)
        {
            
            //Set initial value for 'houseCredits'
            File.WriteAllText(houseCreditsPath, "50");
            houseCredits = Convert.ToInt32(File.ReadAllText(houseCreditsPath));
            //Set initial value for 'isSecret'
            File.WriteAllText(isSecretPath, Convert.ToString(false));
        }

        public static int SetInitialCredits(string creditsPath, string houseCreditsPath, string isSecretPath, int credits, int houseCredits)
        {
            //Set initial value for 'credits'
            File.WriteAllText(creditsPath, "20");
            credits = Convert.ToInt32(File.ReadAllText(creditsPath));

            return credits;
        }
    }
}
