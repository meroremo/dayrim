using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawLine : MonoBehaviour
{
    //private LineRenderer line;
    private bool isMousePressed, isTouchPressed;
    private List<Vector3> pointsList;
    private Vector3 mousePos;
    //private Sprite kiste;
    private int count = 0;
    private GameObject[] lines;
    private Touch touch;

    private SpriteRenderer zeichenfeld;
    // Structure for line points
    struct myLine
    {
        public Vector3 StartPoint;
        public Vector3 EndPoint;
    };
    //	-----------------------------------	
    void Start()
    {
        //Debug.Log(count);
        touch = new Touch();
        zeichenfeld = GetComponent<SpriteRenderer>();
        // Create line renderer component and set its property
        lines = new GameObject[1000];
        //line = gameObject.AddComponent<LineRenderer>();
        lines[count] = new GameObject();
        lines[count].AddComponent<LineRenderer>();
        lines[count].gameObject.GetComponent<LineRenderer>().material = new Material(Shader.Find("Particles/Additive"));
        //line.tag = "LineDraw";
        lines[count].gameObject.GetComponent<LineRenderer>().SetVertexCount(0);
        lines[count].gameObject.GetComponent<LineRenderer>().SetWidth(0.1f, 0.1f);
        lines[count].gameObject.GetComponent<LineRenderer>().SetColors(Color.magenta, Color.magenta);
        lines[count].gameObject.GetComponent<LineRenderer>().useWorldSpace = true;
        isMousePressed = false;
        isTouchPressed = false;

        pointsList = new List<Vector3>();

        //		renderer.material.SetTextureOffset(
    }
    //	-----------------------------------	
    void Update()
    {
        // If mouse button down, remove old line and set its color to green
        //Debug.Log(this.GetComponent<SpriteRenderer>().sprite.bounds.size.y);
        //Debug.Log(this.GetComponent<SpriteRenderer>().sprite.bounds.size.x);

        // Debug.Log(Input.mousePosition.ToString());
        //Debug.Log(count);
        // Debug.Log(lines.Length);

        if (Input.GetMouseButtonDown(0))
        {

            isMousePressed = true;
            //line.SetVertexCount(0);
            pointsList.RemoveRange(0, pointsList.Count); //wichtig damit neue line gezeichnet wird und nicht am punkt von der anderen weiter
                                                         //line = gameObject.AddComponent<LineRenderer>();
                                                         //lines[count].gameObject.GetComponent<LineRenderer>().SetColors(Color.magenta, Color.magenta);
        }
        else if (Input.GetMouseButtonUp(0) || Input.touchCount > 0)
        {

            count++;
            lines[count] = new GameObject();
            lines[count].AddComponent<LineRenderer>();
            lines[count].gameObject.GetComponent<LineRenderer>().material = new Material(Shader.Find("Particles/Additive"));
            lines[count].gameObject.GetComponent<LineRenderer>().SetVertexCount(0);
            lines[count].gameObject.GetComponent<LineRenderer>().SetWidth(0.1f, 0.1f);
            lines[count].gameObject.GetComponent<LineRenderer>().SetColors(Color.magenta, Color.magenta);
            lines[count].gameObject.GetComponent<LineRenderer>().useWorldSpace = true;
            isMousePressed = false;

        }
        // Drawing line when mouse is moving(presses)
        if (isMousePressed)
        {

            if ((Input.mousePosition.y > zeichenfeld.sprite.textureRect.y && Input.mousePosition.y < zeichenfeld.sprite.textureRect.height && Input.mousePosition.x > zeichenfeld.sprite.textureRect.x
                && Input.mousePosition.x < zeichenfeld.sprite.textureRect.width))

            {

                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0;
                if (!pointsList.Contains(mousePos))
                {
                    pointsList.Add(mousePos);
                    Debug.Log(lines[count].gameObject);

                    lines[count].gameObject.GetComponent<LineRenderer>().SetVertexCount(pointsList.Count);
                    lines[count].gameObject.GetComponent<LineRenderer>().SetPosition(pointsList.Count - 1, (Vector3)pointsList[pointsList.Count - 1]);

                }
            }
        }
        else if (isTouchPressed)
        {
            if (touch.position.y > 100 && touch.position.y < 500 && touch.position.x > 500
                && touch.position.x < 863)
            {

            }
        }

        if (Input.GetMouseButtonUp(0))
        {

        }
    }


    //	-----------------------------------	
    //	Following method checks whether given two points are same or not
    //	-----------------------------------	
    private bool checkPoints(Vector3 pointA, Vector3 pointB)
    {
        return (pointA.x == pointB.x && pointA.y == pointB.y);
    }



}