using UnityEngine;
using System.Collections;

public class SwitchCharacterController : MonoBehaviour 
{
    public GameObject felixGameObject;
    public GameObject feliGameObject;

    private PlayerCharacterController felix;
    private PlayerCharacterController feli;

    void Start () 
    {
        felix = felixGameObject.GetComponent<PlayerCharacterController>();
        feli = feliGameObject.GetComponent<PlayerCharacterController>();

        setActiveCharacter();
	}

    public void switchActiveCharacter()
    {
        felix.activeCharacter = !felix.activeCharacter;
        feli.activeCharacter = !feli.activeCharacter;

        setActiveCharacter();

        Debug.Log(ActiveCharacter.activeCharacter.name);
    }

    private void setActiveCharacter()
    {
        if (feli.activeCharacter)
        {
            felix.activeCharacter = false;
            ActiveCharacter.activeCharacter = feliGameObject;

            felixGameObject.tag = "NPC";
            feliGameObject.tag = "ActiveCharacter";
        }
        else if (felix.activeCharacter)
        {
            feli.activeCharacter = false;
            ActiveCharacter.activeCharacter = felixGameObject;

            feliGameObject.tag = "NPC";
            felixGameObject.tag = "ActiveCharacter";
        }
    }
}
