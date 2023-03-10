using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public bool canRoll, mouseEnabled;
    public string tileColour;
    public GameObject sceneObjects, battleObjects, diceObject, menuCanvas;
    PlayerMovement playerM;
    void Update()
    {
        if (Input.GetKeyDown("m") && !menuCanvas.activeSelf)
        {//Open menu panel
            menuCanvas.SetActive(true);
            Time.timeScale = 0;
        }
        else if (Input.GetKeyDown("m") && menuCanvas.activeSelf)
        {
            menuCanvas.SetActive(false);
            Time.timeScale = 1;
        }
        if (Input.GetMouseButtonUp(1))
        {
            if(canRoll)
            {
                canRoll = false;
            }
        }
        if(PlayerPrefs.HasKey("NextScene") && PlayerPrefs.GetInt("NextScene") == 0)
        {
            activateAll();
            canRoll = true;
            PlayerPrefs.DeleteKey("NextScene");
        }
        if(tileColour == "Green")
        {
            BattleScene();
            tileColour = "";
        }
        else if (tileColour == "Red")
        {
            BattleScene();
            tileColour = "";
        }
        else if(tileColour == "Blue" || tileColour == "Orange" || tileColour == "Yellow")
        {
            OtherScene();
            tileColour = "";
        }
    }
    public void UnloadScene()
    {
        //diceObject = GameObject.Find("AllObjects");
        //activateAll();
        PlayerPrefs.SetInt("NextScene", 0);
        SceneManager.UnloadSceneAsync(1);
    }
    public void BattleScene()
    {
        deactivateAll();
        SceneManager.LoadScene("BattleScene", LoadSceneMode.Additive);
    }
    public void OtherScene()
    {
        //canRoll = true;
        diceObject = GameObject.Find("d6(Clone)");
        GameObject.Destroy(diceObject);
    }
    public void deactivateAll()
    {
        PlayerPrefs.SetInt("NextScene", 1);
        sceneObjects.SetActive(false);
        battleObjects.SetActive(true);
        diceObject = GameObject.Find("d6(Clone)");
        GameObject.Destroy(diceObject);
    }
    public void activateAll()
    {
        sceneObjects.SetActive(true);
        battleObjects.SetActive(false);
    }
    public void deleteAllKeys()
    {
        PlayerPrefs.DeleteAll();
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
            //Debug.Log("The key " + KeyName + " exists");
            return true;
        }
        else
        {
            //Debug.Log("The key " + KeyName + " does not exist");
            return false;
        }
    }
    public void DeleteKey(string KeyName)
    {
        PlayerPrefs.DeleteKey(KeyName);
    }
}
