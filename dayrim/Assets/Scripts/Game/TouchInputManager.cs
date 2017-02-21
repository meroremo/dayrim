using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TouchInputManager : MonoBehaviour
{
    public GameObject CharacterController;

    public GameObject LookAtButton;
    public GameObject PickUpButton;
    public GameObject TalkToButton;

    public Text infoText;

    private SwitchCharacterController switchCharacterController;
    private MoveCharacterController moveCharacterController;
    private DialogSceneManager dialogSceneManager;
    private LookAtDialogManager lookAtDialogManager;
    private ItemSingleInteraction itemSingleInteraction;

    private GameObject selectedObject;
    private Ray ray;
    private RaycastHit rayHit;

    private Vector3 touchPosition;
    private Vector3 lookPosition;
    private Vector3 talkPosition;
    private Vector3 pickPosition;

    //vanessas quatsch
    private bool changeScene = false;
    public GameObject pfeil;
    public GameObject x;
    public static ItemManager manager;
    public GameObject tipp;
    private int tippCount = 0;
   // private Animator animator;


    void Start()
    {
        SceneNameManager.lastIngameScene = SceneManager.GetActiveScene().name;
        ActiveGameMode.GAMEMODE = ActiveGameMode.GameModes.INGAME;

        switchCharacterController = CharacterController.GetComponent<SwitchCharacterController>();
        moveCharacterController = CharacterController.GetComponent<MoveCharacterController>();

        dialogSceneManager = this.GetComponent<DialogSceneManager>();
        infoText.text = "ICH BIN DA";

        SetInteraction(false, false, false);

        //___________
        manager = new ItemManager();
        //manager = TouchInputManager.manager;

        //animator = this.GetComponent<Animator>();

    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                Touch touch = Input.touches[0];
                touchPosition = Input.GetTouch(0).position;




               // Vector3 lala = new Vector3(touchPosition.x, touchPosition.y, -1);

                tippCount++;

                
                if (tippCount > 0)
                {
                    Debug.Log(touchPosition);
                    //Vector3 lala = new Vector3(touchPosition.x, touchPosition.y, -1);
                    Destroy(GameObject.FindGameObjectWithTag("Touch"));
                    tippCount = 0;
                }

                Instantiate(tipp, touchPosition, Quaternion.identity);
                SetPositionsToTouchPosition();

                // Animator.Play("Touch");
                // animator.Play("isTouched", 1);
                // Vector3 alala = Camera.main.ScreenToWorldPoint(touchPosition);


                switch (ActiveGameMode.GAMEMODE) // NOCH ZU ERWEITERN
                {
                    case ActiveGameMode.GameModes.INGAME:
                        HandleTouchIngameScene(touch);
                        break;
                    case ActiveGameMode.GameModes.ITEMCOMBI:
                        HandleTouchCombiScene(touch);
                        break;
                    default:
                        Debug.Log("NOT INGAME GAMEMODE");
                        break;
                }
            }
        }

    }

    public static ItemManager getManager()
    {
        ItemManager mama = manager;
        return mama;
    }

    private void HandleTouchIngameScene(Touch touch)
    {
        
        if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId))
        {
            if (Physics.Raycast(ray, out rayHit, Mathf.Infinity))
            {
                GameObject hitObject = rayHit.transform.gameObject;
                selectedObject = hitObject;

                if (hitObject != null)
                {
                    moveCharacterController.moveCharacterTo(hitObject.transform.position);
                }

                Debug.Log(hitObject.tag);
                Debug.Log(hitObject.name);

                if (hitObject.name == "Felix")
                {
                    if (hitObject.tag != "NPC" && hitObject.tag != "ActiveCharacter")
                    {
                        if (ActiveCharacter.activeCharacter.name == "Feli")
                            hitObject.tag = "NPC";
                    }
                }

                if (hitObject.tag == "ActiveCharacter" || hitObject.tag == "NPC")
                {
                    Debug.Log("FELIX ODER FELI");
                    lookPosition.x -= 30;
                    lookPosition.y += 30;

                    talkPosition.x += 30;
                    talkPosition.y += 30;

                    SetButtonPositions();

                    SetInteraction(true, true, false);
                }
                else if (hitObject.tag == "PickableItem")
                {
                    itemSingleInteraction = hitObject.GetComponent<ItemSingleInteraction>();
                    if (itemSingleInteraction.singleInteraction)
                        infoText.text = "Ich interagiere mit diesem Objekt";
                    else
                        infoText.text = "Mit diesem Objekt ist keine Interaktion möglich";

                    lookPosition.x -= 30;
                    lookPosition.y += 30;

                    pickPosition.x += 30;
                    pickPosition.y += 30;

                    SetButtonPositions();

                    SetInteraction(true, false, true);


                }
                else if (hitObject.tag == "UnpickableItem")
                {
                    itemSingleInteraction = hitObject.GetComponent<ItemSingleInteraction>();
                    if (itemSingleInteraction.singleInteraction)
                        infoText.text = "Ich interagiere mit diesem Objekt"; // ENTSPRECHEND ETWAS TUN, BETRETEN ODER SO, KA
                    else
                        infoText.text = "Mit diesem Objekt ist keine Interaktion möglich";

                    lookPosition.y += 30;

                    SetButtonPositions();

                    SetInteraction(true, false, false);
                }

                //DUMMY__________________________________________wenn man auf luises kiste drückt
                else if (hitObject.tag == "OpenDialog")
                {
                    SceneManager.LoadScene("DialogScene");
                    
                }

                if (hitObject.name == "Door")
                {

                    pfeil.active = true;
                    x.active = true;

                    changeScene = true;
                }

                else if (changeScene)
                {
                    if (hitObject.tag == "OpenJunkyard01")
                    {
                        SceneManager.LoadScene("Junkyard_Scene_01");
                        changeScene = false;
                        pfeil.active = false;
                        x.active = false;
                    }

                    else if (hitObject.tag == "OpenJunkyardIndoor")
                    {
                        SceneManager.LoadScene("Junkyard_Indoor_Scene_03");
                        changeScene = false;
                        pfeil.active = false;
                        x.active = false;
                    }

                    else if (hitObject.name == "x")
                    {
                        changeScene = false;
                        pfeil.active = false;
                        x.active = false;
                    }
                }
            }
            else
            {
                SetInteraction(false, false, false);
            }
        }
    }

    private void HandleTouchCombiScene(Touch touch)
    {
        if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId))
        {
            //    GameObject.Find("TestText").GetComponent<Text>().text = "CHICK CHICK CHICK";

            if (Physics.Raycast(ray, out rayHit, Mathf.Infinity))
            {
                // GameObject.Find("TestText").GetComponent<Text>().text = "HIT HIT HIT";

                GameObject hitObject = rayHit.transform.gameObject;
                selectedObject = hitObject;

                //für den Fall der Sprühdose als Beispiel


                if (ItemCombinations.selectedItem.name == "Slot_02")
                {
                    //GameObject.Find("TestText").GetComponent<Text>().text = hitObject.name;


                    if (hitObject.tag == "OpenDialog" || hitObject.name == "plueschmoehre")
                    {
                        //GameObject.Find("TestText").GetComponent<Text>().text = "LUISE LUISE LUISE";
                        hitObject.GetComponent<SpriteRenderer>().color = Color.red;
                    }

                    /*if (hitObject.tag == "OpenDialog")
                    {
                        //GameObject.Find("TestText").GetComponent<Text>().text = "LUISE LUISE LUISE";
                        SceneManager.LoadScene("DialogScene");
                    }



                            // GameObject.Find("TestText").GetComponent<Text>().text = "HIT HIT HIT";
                            hit = Physics.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position), Vector2.zero);
                    //GameObject.Find("TestText").GetComponent<Text>().text = Input.GetTouch(0).position.ToString();
                   if (hit.collider != null && hit.transform.gameObject.tag == "Karton_Luise") 
                    { 
                        GameObject.Find("TestText").GetComponent<Text>().text = "Invenape getouched doooown..";
                    }*/

                    /*if (hitObject.tag == "OpenJunkyard01")
                    {
                        SceneManager.LoadScene("Junkyard_Scene_01");
                    }

                    if (hitObject.tag == "OpenJunkyardIndoor")
                    {
                        SceneManager.LoadScene("Junkyard_Indoor_Scene_03");
                    }*/
                }
            }
        }
    }

    private void SetInteraction(bool stateA, bool stateB, bool stateC)
    {
        LookAtButton.SetActive(stateA);
        TalkToButton.SetActive(stateB);
        PickUpButton.SetActive(stateC);
    }

    private void SetPositionsToTouchPosition()
    {
        lookPosition = touchPosition;
        pickPosition = touchPosition;
        talkPosition = touchPosition;
    }

    private void SetButtonPositions()
    {
        LookAtButton.transform.position = lookPosition;
        PickUpButton.transform.position = pickPosition;
        TalkToButton.transform.position = talkPosition;
    }

    public void switchCharacterOnButton()
    {
        switchCharacterController.switchActiveCharacter();
    }

    public void LookAt() // Steuerung über das Element
    {
        lookAtDialogManager = selectedObject.GetComponent<LookAtDialogManager>();

        SetInteraction(false, false, false);
        infoText.text = lookAtDialogManager.getLookAtDialog();
    }

    public void TalkTo() // Steuerung über das Element
    {
        SetInteraction(false, false, false);
        lookAtDialogManager = selectedObject.GetComponent<LookAtDialogManager>();

        if (selectedObject.name == ActiveCharacter.activeCharacter.name)
        {
            infoText.text = lookAtDialogManager.getLookAtDialog();
            // Anschauen Dialog öffnen, da Spieler sonst mit sich selbst redet
        }
        else
        {
            dialogSceneManager.enterDialogScene();
        }
    }

    public void PickUp() // Steuerung über das Element
    {
        SetInteraction(false, false, false);
        lookAtDialogManager = selectedObject.GetComponent<LookAtDialogManager>();

        lookAtDialogManager.getLookAtDialog();

        selectedObject.SetActive(false);
        // Aufheben-Funktion der Itemverwaltung aufrufen
        manager.addItem(selectedObject.GetComponent<Item>());

    }

    public void ClearInfoText()
    {
        infoText.text = "";
    }

    /*  public void OpenInventory()
      {
          ActiveGameMode.GAMEMODE = ActiveGameMode.GameModes.INVENTORY;
          Debug.Log(ActiveGameMode.GAMEMODE);

          // Inventar einblenden (Mit Vanessa zusammen erarbeiten)
              // INVENTAR SCHLIEßEN, UM DANN DEN GAMEMODE WIEDER ZU ÄNDERN
      }

      public void OpenMenu()
      {
          ActiveGameMode.GAMEMODE = ActiveGameMode.GameModes.MENU;
          Debug.Log(ActiveGameMode.GAMEMODE);

          // Menu öffnen
              // Beim Schließen des Menüs den GameMode wieder ändern!
      }*/

    public void OpenDrawScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}