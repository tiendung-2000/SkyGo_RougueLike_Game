using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletSpawn : MonoBehaviour
{
    public Transform[] point;
    public GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Spawn()
    {
        foreach (Transform t in point)
        {
            Instantiate(bullet, t.position, t.rotation);
        }
        Destroy(this.gameObject);
    }
}
