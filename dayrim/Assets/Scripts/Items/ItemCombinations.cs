using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ItemCombinations : MonoBehaviour {

    //Test zum schließen des Inventars für SceneItemKombi
    private Animation inventar;
    public GameObject Inventar_Panel;
    public GameObject Invenape;
    public GameObject watchMode;

    private Item FixedItem;
    private Item TestItem;
    private GameObject slot;

    public GameObject SceneItemButton;

    //zum interagieren mit der szene, bezug zu detecttouches
    public static bool isItemSceneCombiActive = false;
    public static bool isWatchModeActive = false;
    public static Item selectedItem;

    //ItemSprites
    public Image test;
    public ItemManager manager;
    public static bool isRemoved = false;


    // Use this for initialization
    void Start () {
        inventar = Inventar_Panel.GetComponent<Animation>();

        manager = TouchInputManager.getManager();
    }
	
	// Update is called once per frame
	void Update () {

        if (isRemoved)
        {
            FixedItem.transform.parent.GetComponent<Button>().GetComponent<Image>().color = Color.white;
            FixedItem = null;
            isRemoved = false;
        }
    }


    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void watchItemInfo(Button watchButton)
    {
        if (!isWatchModeActive)
        {
            isWatchModeActive = true;
            watchButton.GetComponent<Image>().color = Color.red;
        }
        else
        {
            isWatchModeActive = false;
            watchButton.GetComponent<Image>().color = Color.white;
        }
    }

    public void getItemInformation(GameObject ItemSlot)
    {
        watchMode.active = true;
        GameObject.Find("ItemImage").GetComponent<Image>().sprite = ItemSlot.transform.GetChild(0).GetComponentInChildren<Item>().getSprite();

        GameObject.Find("ItemInfoText").GetComponent<Text>().text = ItemSlot.transform.GetChild(0).GetComponentInChildren<Item>().getBeschreibung();
    }

    public void closeItemInfo()
    {
        watchMode.active = false;
    }
        

    public void CombineItem(GameObject ItemSlot)
    {
        slot = ItemSlot;

        if (isWatchModeActive)
        {
            if (slot != null)
            {
                getItemInformation(slot);
            }
        }

        else{ //nicht info bekommen zu item
            if (FixedItem == null)
            {
                ItemSlot.GetComponent<Button>().GetComponent<Image>().color = Color.gray;
                FixedItem = ItemSlot.transform.GetChild(0).GetComponent<Item>();

                // GameObject.Find("TestText").GetComponent<Text>().text = ItemSlot.transform.GetChild(0).GetComponent<Item>().ToString();
                //FixedItem = ItemSlot.transform.parent.GetChild(0).GetChild(0).GetComponent<GameObject>().gameObject;
            }
            else
            {
                if (ItemSlot.transform.childCount != 0)
                {

                    if (FixedItem == ItemSlot.transform.GetChild(0).GetComponent<Item>())
                    {
                        ItemSlot.GetComponent<Button>().GetComponent<Image>().color = Color.white;

                        if (TestItem != null)
                        {
                            //TestItem.GetComponent<Button>().GetComponent<Image>().color = Color.white;
                            TestItem = null;
                        }

                        if (test.sprite != null)
                        {
                            test.sprite = null;
                            GameObject.Find("SceneItem").active = false;
                        }
                        FixedItem = null;
                    }
                    else
                    {
                        TestItem = ItemSlot.transform.GetChild(0).GetComponent<Item>();
                        
                        manager.possibleItemCombinations(FixedItem, TestItem);

                    }
                }
            }
        }
    }

    public void SceneCombinations()
    {
        if(FixedItem != null)
        {
            inventar.Play("Inventar_OUT");
            Invenape.active = true;
            ActiveGameMode.GAMEMODE = ActiveGameMode.GameModes.ITEMCOMBI;

            CanvasNavigator.inventar_isActive = false; //muss drin bleiben, da Kamera und Affe sonst nicht mehr reagieren

            selectedItem = FixedItem;
            isItemSceneCombiActive = true;

            //GameObject.Find("TestText").GetComponent<Text>().text = selectedItem.name.ToString();

            SceneItemButton.active = true;


            Sprite chosenItem = FixedItem.getSprite();
            
            GameObject.Find("SceneItemImage").GetComponent<Image>().sprite = chosenItem;
            GameObject.Find("SceneItemImage").transform.localScale = new Vector3(2.5f, 2.5f, 0f);
            //GameObject.Find("ItemImage").GetComponent<SpriteRenderer>().sprite = gummipflanze; 


        }
    }

    public void SceneItem()
    {
        GameObject.Find("SceneItem").active = false;

        isItemSceneCombiActive = false;
        FixedItem = null;
        slot.GetComponent<Button>().GetComponent<Image>().color = Color.white;

        ActiveGameMode.GAMEMODE = ActiveGameMode.GameModes.INGAME;
    }

    public void closeInventar()
    {
        inventar.Play("Inventar_OUT");
        Invenape.active = true;
        ActiveGameMode.GAMEMODE = ActiveGameMode.GameModes.INGAME;
        CanvasNavigator.inventar_isActive = false;

        if (FixedItem != null)
        {
            FixedItem.transform.parent.GetComponent<Button>().GetComponent<Image>().color = Color.white;
            FixedItem = null;
        }
        //slot.GetComponent<Button>().GetComponent<Image>().color = Color.white;
        CanvasNavigator navi = new CanvasNavigator();
        navi.deleteAllItems();
    }
}
