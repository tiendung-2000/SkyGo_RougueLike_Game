using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RoomCenter : MonoBehaviour
{
    public bool openedWhenEnemiesCleared;

    public List<EnemyController> enemies;

    public GameObject enemyGroup;

    public bool isEnemyCenter;

    public Room theRoom;

    public Transform checkPoint;
    public int maxX;
    public int minX;
    public int maxY;
    public int minY;

    void Start()
    {
        if (isEnemyCenter)
        {
            EnemyController[] enemyGr = enemyGroup.GetComponentsInChildren<EnemyController>();

            foreach (EnemyController enemy in enemyGr)
            {
                enemies.Add(enemy);
                enemy.gameObject.SetActive(false);
            }
        }

        if (openedWhenEnemiesCleared)
        {
            theRoom.closeWhenEntered = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // new GameObject ten la "RoomActivator" trigger voi GameObject co tag la "Player"
        if (other.CompareTag("Player"))
        {
            //Check dieu kien xem day co phai phong co Enemy hay khong
            if (isEnemyCenter)
            {
                //Duyet vong for tu mang cua enemies[] de bat Enemy len bang ham SetActive(true)
                for (int i = 0; i < enemies.Count; i++)
                {
                    enemies[i].gameObject.SetActive(true);
                }

                UIController.Ins.map.SetActive(false);
            }
        }
    }

    void Update()
    {
        //Kiem tra lien tuc xem so Enemy trong phong co > 0 hay khong
        if (enemies.Count > 0 && theRoom.roomActive && openedWhenEnemiesCleared)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                // moi lan tieu diet 1 Enemy se Destroy Enemy do, vi vay vi tri cua Enemy do trong mang se = null
                //kiem tra lien tuc xem co vi tri nao trong mang co Enemy = null hay khong
                if (enemies[i] == null)
                {
                    //neu tim thay vi tri nao = null thi su dung ham RemoveAt(vitri) de xoa phan tu do trong mang
                    enemies.RemoveAt(i);
                    //sau khi xoa thi giam phan tu trong mang di 1 don vi
                    i--;
                }
            }

            //kiem tra enemy = 0
            if (enemies.Count == 0)
            {
                //new Enemy = 0 thi goi den ham OpenDoor trong class Room
                theRoom.OpenDoors();
                UIController.Ins.map.SetActive(true);
            }
        }
    }
    public void GetTarget()
    {
        //if (checkPoint != null)
        //{
        //    checkPoint.localPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0f);
        //    Debug.Log(checkPoint.localPosition);

        //}
    }

}
