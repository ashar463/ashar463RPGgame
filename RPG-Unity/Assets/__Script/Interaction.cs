using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Interaction : MonoBehaviour //class used to manage interactions
{
    private bool _hasKey;
    private bool _hasMap;
    private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>(); //gets values from player script
    }

    public void Interact(GameObject gameObject) //interact method that uses gameobject tags to decide what is being interacted with
    {
        if (gameObject.CompareTag("Chest"))
        {
            _hasKey = true;
            GameObject.Find("Text").GetComponent<Text>().text = "You found a key! Maybe it opens a door...";
            GameObject.Find("Text").GetComponent<Text>().color = new Color(1, 1, 1, 1);
            Invoke("RemoveText", 3f);

        }

        if (gameObject.CompareTag("BasementDoor"))
        {
            if (_hasKey)
            {
                _player.transform.position = new Vector3(-54, 1.5f, 0);
               
            }
            else
            {
                GameObject.Find("Text").GetComponent<Text>().text = "You need a key to open ths door";
                GameObject.Find("Text").GetComponent<Text>().color = new Color(1, 1, 1, 1);
                Invoke("RemoveText", 3f);
            }
        }

        if (gameObject.CompareTag("Map"))
        {
            _hasMap = true;
            GameObject.Find("Text").GetComponent<Text>().text = "You found the map! Its time to venture out into the forest";
            GameObject.Find("Text").GetComponent<Text>().color = new Color(1, 1, 1, 1);
            Invoke("RemoveText", 3f);
        }

        if (gameObject.CompareTag("Outside"))
        {
            if (_hasMap)
            {
                DontDestroyOnLoad(GameObject.FindWithTag("Player"));
                SceneManager.LoadScene("Level2");
            }
            else
            {
                GameObject.Find("Text").GetComponent<Text>().text = "You can't leave until you find the map. Legend says Grandpa hid it in the basement...";
                GameObject.Find("Text").GetComponent<Text>().color = new Color(1, 1, 1, 1);
                Invoke("RemoveText", 3f);    
            }
        }
    }

    public void RemoveText()    //method that removes text by making it invisible using colors
    {
        GameObject.Find("Text").GetComponent<Text>().color = new Color(0, 0, 0, 0);
    }
}