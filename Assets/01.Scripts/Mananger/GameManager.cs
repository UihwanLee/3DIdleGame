using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;

    private void Awake()
    {
        if(_instance == null )
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static GameManager Instance
    {
        get
        {
            if (null == _instance)
            {
                return null;
            }
            return _instance;
        }
    }

    private GameManager() { } // 생성자 막기


    // 공유할 변수
    public Player Player { get; private set; }

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
}
