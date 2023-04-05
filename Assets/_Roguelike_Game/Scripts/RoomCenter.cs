using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RoomCenter : MonoBehaviour
{
    public bool openedWhenEnemiesCleared;

    //public List<GameObject> enemies = new List<GameObject>();

    public List<EnemyController> enemies;

    public GameObject enemyGroup;

    public bool isEnemyCenter;

    public Room theRoom;

    public Transform checkPoint;
    public int maxX;
    public int minX;
    public int maxY;
    public int minY;


    // Start is called before the first frame update
    void Start()
    {
        if (isEnemyCenter)
        {
            EnemyController[] enemyGr = enemyGroup.GetComponentsInChildren<EnemyController>();

            foreach (EnemyController enemy in enemyGr)
            {
                enemies.Add(enemy);
            }
        }

        if (openedWhenEnemiesCleared)
        {
            theRoom.closeWhenEntered = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (enemies.Count > 0 && theRoom.roomActive && openedWhenEnemiesCleared)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i] == null)
                {
                    enemies.RemoveAt(i);
                    i--;
                }
            }

            if (enemies.Count == 0)
            {
                theRoom.OpenDoors();
            }
        }
    }
    public void GetTarget()
    {
        if (checkPoint != null)
        {
            checkPoint.localPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0f);
            Debug.Log(checkPoint.localPosition);

        }
    }

}
