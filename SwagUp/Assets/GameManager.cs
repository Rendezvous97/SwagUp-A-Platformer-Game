using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Player player;

    private void Awake() {
        if(instance == null) //To have only one game manager in teh game at all times
            instance = this;
        else
            Destroy(gameObject);
    }
}
