using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CanvasNavigator : MonoBehaviour {

    public static bool inventar_isActive = false;
    private Animation inventar;
    public GameObject Inventar_Panel;
    public GameObject Invenape;

    public GameObject SceneItemButton;

    public ItemManager manager;
    private string slot;

    void Start () 
    {

        inventar = Inventar_Panel.GetComponent<Animation>();
        SceneItemButton.active = false;

        manager = TouchInputManager.getManager();
    }

    public void OpenDialog()
    {
      
        if (!inventar_isActive)
        {
            inventar.Play("Inventar_IN");
            inventar_isActive = true;
            ActiveGameMode.GAMEMODE = ActiveGameMode.GameModes.INVENTORY;
            
            Invenape.active = false;

            setAllItems();
        }
    }

    //NEU__________________________________________________
    public void deleteAllItems()
    {
        manager = TouchInputManager.manager;

        foreach (Item i in manager.getInventar())
        {
            Destroy(GameObject.Find(i.getName()));
        }
    }

    public void setAllItems()
    {
        //________________________ITEMS ANORDNEN_____________________________________________

        manager = TouchInputManager.manager;

       GameObject itemSlots = GameObject.Find("ItemSlots").GetComponent<GameObject>();

        for (int i = 0; i < manager.getAnzahlItems(); i++)
        {
            int o = i + 1;

            if (o < 10)
            {
                slot = "Slot_0" + o;
            }
            else
            {
                slot = "Slot_" + o;
            }

            GameObject obj = new GameObject(manager.getItem(i).getName());
            obj.AddComponent<Image>().sprite = manager.getItem(i).getSprite();
            obj.AddComponent<Item>();
            obj.GetComponent<Item>().name = manager.getItem(i).getName();
            obj.GetComponent<Item>().beschreibung = manager.getItem(i).getBeschreibung();
            obj.GetComponent<Item>().verwendungsCode = manager.getItem(i).getCode();
            obj.GetComponent<Item>().aussehen = manager.getItem(i).getSprite();
            obj.transform.parent = GameObject.Find(slot).transform;
            obj.transform.position = obj.transform.parent.position;
            //sprite größen gleich machen -> anpassen!!
            obj.transform.localScale = new Vector3(0.85f, 0.85f, 0);
        }
    }
}
