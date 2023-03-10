using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int numTiles, counter = 0, moveAmount, currentTileNumber = 0;
    public GameObject player, gameManager;
    public GameObject[] tilePositions;
    public string currentTileColour;
    public int[] destination;
    Vector3 startPos, nextPos;
    public bool movingPlayer, finishedMoving, endReached, defeated;
    public float moveTimer;
    private const string PlayerDiceRoll = "PlayerDiceRoll", FinishedRolling = "FinishedRolling";

    void Start()
    {
        PlayerPrefs.DeleteKey(PlayerDiceRoll);
        startPos = transform.position;
        tilePositions = new GameObject[numTiles];
        for (int i = 0; i < numTiles; i++)
        {
            tilePositions[i] = GameObject.Find("Tile (" + i + ")");
        }
    }


    void Update()
    {
        if(PlayerPrefs.HasKey(PlayerDiceRoll) && !movingPlayer)
        {
            moveAmount = PlayerPrefs.GetInt(PlayerDiceRoll);
            FindNextPosition();
            counter += 1;
        }
        if(endReached && defeated)
        {
            player.transform.position = Vector3.Lerp(player.transform.position, startPos, 1f);
            currentTileNumber = 0;
            defeated = false;
            endReached = false;
        }
        if (player.transform.position == nextPos)
        {
            
            movingPlayer = false;
            if (counter == moveAmount)
            {
                finishedMoving = true;
                PlayerPrefs.DeleteKey(PlayerDiceRoll);
                //PlayerPrefs.SetInt(FinishedRolling, 2);
                counter = 0;
                gameManager.GetComponent<GameManager>().canRoll = true;
                gameManager.GetComponent<GameManager>().tileColour = currentTileColour;
            }
        }
        else if (movingPlayer)
        {
            player.transform.position = Vector3.Lerp(player.transform.position, nextPos, 0.2f);           
        }
        if(currentTileNumber >= (numTiles - 1))
        {
            gameManager.GetComponent<GameManager>().canRoll = false;
        }
    }
    void FindNextPosition()
    {
        movingPlayer = true;
        finishedMoving = false;
        
        if (currentTileNumber >= (numTiles - 1))
        {
            nextPos = new Vector3(tilePositions[numTiles - 1].transform.position.x, 0.25f, tilePositions[numTiles - 1].transform.position.z);
            endReached = true;
            gameManager.GetComponent<GameManager>().canRoll = false;
        }
        else
        {
            currentTileNumber += 1;
            nextPos = new Vector3(tilePositions[currentTileNumber].transform.position.x, 0.25f, tilePositions[currentTileNumber].transform.position.z);
            endReached = false;
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Green")
        {
            currentTileColour = "Green";
        }
        else if (collision.gameObject.tag == "Red")
        {
            currentTileColour = "Red";
        }
        else if (collision.gameObject.tag == "Blue")
        {
            currentTileColour = "Blue";
        }
        else if (collision.gameObject.tag == "Yellow")
        {
            currentTileColour = "Yellow";
        }
        else if (collision.gameObject.tag == "Orange")
        {
            currentTileColour = "Orange";
        }
        else if (collision.gameObject.tag == "Black")
        {
            currentTileColour = "Black";
        }
        else if (collision.gameObject.tag == "Start")
        {
            currentTileColour = "";
        }
    }

    public void SetInt(string KeyName, int Value)
    {
        PlayerPrefs.SetInt(KeyName, Value);
    }
    public int GetInt(string KeyName)
    {
        return PlayerPrefs.GetInt(KeyName);
    }
    public bool HasKey(string KeyName)
    {
        if (PlayerPrefs.HasKey(KeyName))
        {
            Debug.Log("The key " + KeyName + " exists");
            return true;
        }
        else
        {
            Debug.Log("The key " + KeyName + " does not exist");
            return false;
        }
    }
    public void DeleteKey(string KeyName)
    {
        PlayerPrefs.DeleteKey(KeyName);
    }
}
