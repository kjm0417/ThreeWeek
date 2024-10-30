using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterManager : MonoBehaviour
{
    private static CharacterManager instance;

    public static CharacterManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new GameObject("CharacterManager").AddComponent<CharacterManager>();
            }
            return instance;
        }
    }

    private Player player;

    public Player Player
    {
        get { return player; }
        set { player = value; }
    }

    void Awake()
    {
        if(instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance=this;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
