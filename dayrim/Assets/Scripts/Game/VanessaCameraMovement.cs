using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VanessaCameraMovement : MonoBehaviour
{

    public float speed;
    public float zoom;

    public bool isSceneIndoor;
    

    // Use this for initialization
    void Start()
    {
        this.GetComponent<Camera>().orthographicSize = zoom;

        //GameObject.Find("TestText").GetComponent<Text>().text = SceneManager.GetActiveScene().ToString() ;
        //this.GetComponent<Camera>().transform.position = new Vector3(0.0f, 0.0f, -zoom); 
    }

    // Update is called once per frame
    void Update()
    {
        // GameObject.Find("Text").GetComponent<Text>().text = (this.GetComponent<Camera>().transform.position).ToString();

        if (!CanvasNavigator.inventar_isActive)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                if (!isSceneIndoor)
                {
                    //in bestimmten radius 
                    if ((this.GetComponent<Camera>().transform.position.x >= -10.0f && this.GetComponent<Camera>().transform.position.x <= 10.0f)
                        && (this.GetComponent<Camera>().transform.position.y >= -6.0f && this.GetComponent<Camera>().transform.position.y <= 6.0f))
                    {

                        Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
                        transform.Translate(-touchDeltaPosition.x * speed * Time.deltaTime, -touchDeltaPosition.y * speed * Time.deltaTime, 0);
                    }
                    else
                    {
                        if (this.GetComponent<Camera>().transform.position.x < -10.0f)
                        {
                            transform.position = new Vector3(-9.9f, this.transform.position.y, -zoom);
                        }

                        else if (this.GetComponent<Camera>().transform.position.x > 10.0f)
                        {
                            transform.position = new Vector3(9.9f, this.transform.position.y, -zoom);
                        }

                        else if (this.GetComponent<Camera>().transform.position.y < -6.0f)
                        {
                            transform.position = new Vector3(this.transform.position.x, -5.9f, -zoom);
                        }

                        else if (this.GetComponent<Camera>().transform.position.y > 6.0f)
                        {
                            transform.position = new Vector3(this.transform.position.x, 5.9f, -zoom);
                        }
                    }
                }//bis hier hin Outdoor Szenen vom Schrottplatz

                else
                {
                    if (this.GetComponent<Camera>().transform.position.x >= -15.0f && this.GetComponent<Camera>().transform.position.x <= 15.0f)
                    {

                        Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
                        transform.Translate(-touchDeltaPosition.x * speed * Time.deltaTime, this.transform.position.y, 0);
                    }
                    else {
                        if (this.GetComponent<Camera>().transform.position.x < -15.0f)
                        {
                            transform.position = new Vector3(-14.9f, this.transform.position.y, -zoom);
                        }

                        else if (this.GetComponent<Camera>().transform.position.x > 15.0f)
                        {
                            transform.position = new Vector3(14.9f, this.transform.position.y, -zoom);
                        }
                    }
                }
            }
        }

    }
}
