using MontageScannerLibrary;
using R24MontageScannerSqlAccess;
using R24MontageScannerSqlAccess.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace MontageScanLib;

public static class UserInterfaceLogic
{
    /// <summary>
    /// Hardcoded Prüfung der eingabe
    /// </summary>
    /// <param name="input">Inputstring</param>
    /// <returns>true wenn eingabe Buchstabe und 6 Ziffern sind</returns>
    public static bool inputCheckLieferschein(this string input)
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


    /// <summary>
    /// Checks if the Input could be a valid ChipId
    /// </summary>
    /// <param name="input"> input chipid</param>
    /// <returns></returns>
    public static bool inputCheckChipId(this string input)
    {
        bool output = Regex.IsMatch(input, @"^\d{10}$");
        return output;
    }

    /// <summary>
    /// Checks if MitarbeiterModel has values in vorname and nachname
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static bool validMitarbeiterInput(this MitarbeiterModel input)
    {
        bool output = false;

        if (input.Vorname.Length > 0 && input.Vorname.Length < 20 && input.Vorname != null &&
            input.Nachname.Length > 0 && input.Nachname.Length < 20 && input.Nachname != null
            )
        {
            output = true;
        }
        return output;
    }


    public static DisplayedModel addToDisplay(DisplayedModel liste, MitarbeiterModel user, MontageLieferscheinModel lieferschein)
    {
        liste.Lieferschein = lieferschein.Lieferschein;
        liste.TimeStamp = lieferschein.MontageTS;
        liste.Nachname = user.Nachname;

        return liste;
    }
}

