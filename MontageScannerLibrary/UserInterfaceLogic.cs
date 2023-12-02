using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MontageScanLib
{
    public static class UserInterfaceLogic
    {
        /// <summary>
        /// Hardcoded Prüfung der eingabe
        /// </summary>
        /// <param name="input">Inputstring</param>
        /// <returns>true wenn eingabe Buchstabe und 6 Ziffern sind</returns>
        public static bool InputCheck(this string input)
        {
            bool output = false;

            if (input != null && input.Length == 7)
            {
                if (char.IsLetter(input[0]))
                {
                    for (int i = 1; i < input.Length; i++)
                    {
                        if (char.IsDigit(input[i]))
                        {
                            output = true;
                        }
                        else
                        {
                            output = false;
                        }
                    }

                }
                else
                {
                    output = false;
                }
            }
            else
            {
                output = false;
            }
            return output;
        }



    }
}
