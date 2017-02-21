using UnityEngine;
using System.Collections;

public class MoveCharacterController : MonoBehaviour
{
    public GameObject feli;
    public GameObject felix;
    public GameObject pathManager;

    public float movementSpeed = 4f;

    private GameObject inactiveCharacter;
    private PathManager pathManagerScript;

    void Start()
    {
        pathManagerScript = pathManager.GetComponent<PathManager>();

        ActiveCharacter.activeCharacter = feli;
        inactiveCharacter = felix;

        //feli.transform.position = pathManagerScript.GetNode(2).transform.position;

        Vector3 felixPosition = feli.transform.position;
        felixPosition.x--;
        felix.transform.position = felixPosition;

        if (ActiveCharacter.activeCharacter != null)
        {
            setFollowingCharacter();
        }
    }

    void Update()
    {
        setFollowingCharacter();

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            ActiveCharacter.activeCharacter.transform.position += Vector3.left * movementSpeed * Time.deltaTime;
            inactiveCharacter.transform.position += Vector3.left * movementSpeed * Time.deltaTime;

            if (ActiveCharacter.activeCharacter.transform.position.x > inactiveCharacter.transform.position.x)
                switchCharacterPosition();
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            ActiveCharacter.activeCharacter.transform.position += Vector3.right * movementSpeed * Time.deltaTime;
            inactiveCharacter.transform.position += Vector3.right * movementSpeed * Time.deltaTime;

            if (ActiveCharacter.activeCharacter.transform.position.x < inactiveCharacter.transform.position.x)
                switchCharacterPosition();
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            ActiveCharacter.activeCharacter.transform.position += Vector3.up * movementSpeed * Time.deltaTime;
            inactiveCharacter.transform.position += Vector3.up * movementSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            ActiveCharacter.activeCharacter.transform.position += Vector3.down * movementSpeed * Time.deltaTime;
            inactiveCharacter.transform.position += Vector3.down * movementSpeed * Time.deltaTime;
        }
    }

    private void setFollowingCharacter()
    {
        if (ActiveCharacter.activeCharacter.name == "Felix" && inactiveCharacter != feli)
        {
            inactiveCharacter = feli;
            switchCharacterPosition();
        }
        else if (ActiveCharacter.activeCharacter.name == "Feli" && inactiveCharacter != felix)
        {
            inactiveCharacter = felix;
            switchCharacterPosition();
        }
    }

    private void switchCharacterPosition()
    {
        float tmpActivePosition = ActiveCharacter.activeCharacter.transform.position.x;

        ActiveCharacter.activeCharacter.transform.position = new Vector3(inactiveCharacter.transform.position.x, ActiveCharacter.activeCharacter.transform.position.y, ActiveCharacter.activeCharacter.transform.position.z);
        inactiveCharacter.transform.position = new Vector3(tmpActivePosition, inactiveCharacter.transform.position.y, inactiveCharacter.transform.position.z);
    }

    public void moveCharacterTo(Vector3 position)
    {
      /*  pathManagerScript.setDestination(position);
        // pathManagerScript.calcPath();

        while (!pathManagerScript.reachedDestination)
        {
            ActiveCharacter.activeCharacter.transform.position = pathManagerScript.NextNode().transform.position;
        }*/
    }
}