using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class DetectTouches : MonoBehaviour {

    private GameObject selectedObject;
    private Ray ray;
    private RaycastHit rayHit;
    private bool changeScene = false;

    public GameObject pfeil;
    public GameObject x;

    public static ItemManager manager;

    // Use this for initialization
    void Start () {
        manager = new ItemManager();
    }

     public static ItemManager getManager()
    {
        ItemManager mama = manager;
        return mama;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
           // GameObject.Find("TestText").GetComponent<Text>().text = "HOT HOT HOT";
            ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            Touch touch = Input.touches[0];

            // touchPosition = Input.GetTouch(0).position;

            //zusätzliche if, falls man NICHT Items mit Elementen der Szene kombinieren will
            if (!ItemCombinations.isItemSceneCombiActive)
            {

                if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    //    GameObject.Find("TestText").GetComponent<Text>().text = "CHICK CHICK CHICK";

                    if (Physics.Raycast(ray, out rayHit, Mathf.Infinity))
                    {
                        // GameObject.Find("TestText").GetComponent<Text>().text = "HIT HIT HIT";

                        GameObject hitObject = rayHit.transform.gameObject;
                        selectedObject = hitObject;

                       
                        if (hitObject.tag == "PickableItem")
                        {
                            manager.addItem(hitObject.GetComponent<Item>());

                            //Destroy(hitObject);

                            //GameObject.Find("TestText").GetComponent<Text>().text = manager.getItem(0).ToString();
                        }

                        if (hitObject.tag == "OpenDialog")
                        {
                            //GameObject.Find("TestText").GetComponent<Text>().text = "LUISE LUISE LUISE";
                            SceneManager.LoadScene("DialogScene");
                        }



                        /*        // GameObject.Find("TestText").GetComponent<Text>().text = "HIT HIT HIT";
                                hit = Physics.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position), Vector2.zero);
                        //GameObject.Find("TestText").GetComponent<Text>().text = Input.GetTouch(0).position.ToString();
                       if (hit.collider != null && hit.transform.gameObject.tag == "Karton_Luise") 
                        { 
                            GameObject.Find("TestText").GetComponent<Text>().text = "Invenape getouched doooown..";
                        }*/

                        if (hitObject.name == "Door")
                        {

                            pfeil.active = true;
                            x.active = true;

                            changeScene = true;                            
                        }

                        if (changeScene)
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
                }
            }
            //man hat ein Item ausgewählt un möchte es Kombinieren mit Elementen der Szene
            else
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


                        if(ItemCombinations.selectedItem.name == "Slot_02")
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
            } //ende der else
        }
    }
}
