using UnityEngine;
using System.Collections;

public class Path : MonoBehaviour 
{
    public GameObject[] Nodes;
    public GameObject character;
    public int crossPoint;

    public float speed;
    public bool isPatroling;
    public bool isPatrolingQueue;
    public bool hasCrossPoint;

    private int currentNode;
    private int nextNode;
    private int lastNode;
    private int destNode;
    private float thisDistance;

    private Vector3 destination;
    private GameObject[] tmpPath;

    private bool onPath;
    private bool adding;
    private bool foundClosestNode;
    private bool moving;

    void Start()
    {
        if (isPatroling && isPatrolingQueue) // Falls beide Patroullienarten ausgewählt werden, Pfadpatroullie als Standart nehmen
        {
            isPatrolingQueue = false;
        }
    }
	
	void Update () 
    {
        if (isPatroling) // Pfadpatroullie
            patrolPath();
        else if (isPatrolingQueue) // Kreispatroullie
            patrolQueue();
	}

    // Muss aufgerufen werden, wenn kein Charakter im Inspektor angegeben wird, bevor jegliches Movement ausgeführt wird
    public void setCharacter(GameObject characterToMove)
    {
        character = characterToMove;
    }

    public GameObject getCharacter()
    {
        return character;
    }

    private void findClosestNodeIndex(Vector3 position)
    {
        float distance = Vector3.Distance(Nodes[0].transform.position, position);

        for (int i = 0; i < Nodes.Length; ++i)
        {
            Vector3 nodePosition = Nodes[i].transform.position;
            float nodeDistance = Vector3.Distance(nodePosition, position);

            if (nodeDistance <= distance)
            {
                distance = nodeDistance;
                thisDistance = distance;
                destNode = i;
            }
        }

        foundClosestNode = true;
    }

    public GameObject findClosestNode(Vector3 position)
    {
        GameObject node = Nodes[0];
        float distance = Vector3.Distance(Nodes[0].transform.position, position);

        for (int i = 1; i < Nodes.Length; ++i) // nächsten Knotenpunkt über geringsten Abstand ermitteln
        {
            Vector3 nodePosition = Nodes[i].transform.position;
            float nodeDistance = Vector3.Distance(nodePosition, position);

            if (nodeDistance < distance)
            {
                distance = nodeDistance;
                thisDistance = distance;
                node = Nodes[i];
            }
            else
                continue;
        }

        return node;
    }

    public void leavePath() // diesen Pfad verlassen
    {
        onPath = false;
        currentNode = 0;
    }

    public void enterPath() // diesen Pfad betreten
    {
        onPath = true;
    }

    public void setCurrentNode(GameObject node) // aktuellen Knotenpunkt setzen
    {
        for (int i = 0; i < Nodes.Length; ++i)
        {
            if (Nodes[i] == node)
            {
                currentNode = i;
                break;
            }
        }
    }

    public void setDestination(Vector3 newDest) // Ziel setzen
    {
        destination = newDest;
    }

    public Vector3 getDestination()
    {
        return destination;
    }

    public void walkPath() // Pfad ablaufen
    {
        moving = true;

        getPath(destination); // pfad berechnen und richtung bestimmen
        checkDirection();

        if (adding)
            nextNode = currentNode + 1; // "vorwärts" laufen
        else
            nextNode = currentNode - 1; // "rückwärts" laufen

        if (nextNode < 0)
            nextNode = 0;

        character.transform.position = Vector3.MoveTowards(character.transform.position, Nodes[nextNode].transform.position, speed * Time.deltaTime);

        if (character.transform.position == Nodes[nextNode].transform.position) // wenn nächsten Knoten erreicht, aktueller Knoten = nächster Knoten
            currentNode = nextNode;

        if (currentNode == destNode) // wenn Ziel erreicht, Bewegung beenden
        {
            foundClosestNode = false;
            moving = false;
        }
    }

    private void checkDirection()
    {
        if (destNode > currentNode)
            adding = true;
        else if (destNode < currentNode)
            adding = false;
    }

    private void getPath(Vector3 destPosition) // Pfad berechnen
    {
        if(!foundClosestNode)
            findClosestNodeIndex(destPosition);

        if (destNode > currentNode)
        {
            adding = true;
            tmpPath = new GameObject[destNode - currentNode + 1]; // pfad anlegen

            int j = 0;
            for (int i = currentNode; i <= destNode; ++i) //  benötigte punkte in neuen pfad einfügen
            {
                tmpPath[j] = Nodes[i];
                ++j;
            }
        }
        else if (destNode < currentNode)
        {
            adding = false;
            tmpPath = new GameObject[currentNode - destNode + 1];
            int j = 0;

            for (int i = currentNode; i >= destNode; --i) //  benötigte punkte in neuen pfad einfügen
            {
                tmpPath[j] = Nodes[i];
                ++j;
            }
        }
    }

    public void patrolQueue() // kreis ablaufen
    {
        nextNode = (currentNode + 1) % (Nodes.Length);

        character.transform.position = Vector3.MoveTowards(character.transform.position, Nodes[nextNode].transform.position, speed * Time.deltaTime);

        if (character.transform.position == Nodes[nextNode].transform.position)
            currentNode = nextNode;
    }

    public void patrolPath() // pfad hin und her laufen
    {
        if (currentNode != Nodes.Length - 1)
            nextNode = (currentNode + 1);
        else
            lastNode = Nodes.Length - 1;

        if (lastNode != -1)
        {
            if (lastNode < currentNode)
                nextNode = (currentNode + 1);
            else
            {
                if (currentNode != 0)
                    nextNode = (currentNode - 1);
                else
                    nextNode = (currentNode + 1);
            }
        }

        character.transform.position = Vector3.MoveTowards(character.transform.position, Nodes[nextNode].transform.position, speed * Time.deltaTime);

        if (character.transform.position == Nodes[nextNode].transform.position)
        {
            lastNode = currentNode;
            currentNode = nextNode;
        }
    }

    public bool isMoving()
    {
        return moving;
    }

    public void setMoving(bool isNowMoving)
    {
        moving = isNowMoving;
    }

    public float getPathDistance()
    {
        return thisDistance;
    }

    public bool atCrossPoint()
    {
        bool atCrossPoint = false;

        if(currentNode == crossPoint)
            atCrossPoint = true;

        return atCrossPoint;
    }

    public bool reachedGoal()
    {
        bool reachedGoal = !moving;
        return reachedGoal;
    }
}
