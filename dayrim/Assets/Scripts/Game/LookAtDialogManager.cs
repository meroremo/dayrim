using UnityEngine;
using System.Collections;

public class LookAtDialogManager : MonoBehaviour 
{
    public string felixGerman;
    public string feliGerman;
    public string felixEnglish;
    public string feliEnglish;

    public string getLookAtDialog()
    {
        string lookAtDialog = "";

        if (ActiveCharacter.activeCharacter.name == "Felix")
        {
            if (false) // Deutsch
                lookAtDialog = felixGerman;
            else // English
                lookAtDialog = felixEnglish;
        }
        else if (ActiveCharacter.activeCharacter.name == "Feli")
        {
            if (false) // Deutsch
                lookAtDialog = feliGerman;
            else // English
                lookAtDialog = feliEnglish;
        }

        return lookAtDialog;
    }
}
