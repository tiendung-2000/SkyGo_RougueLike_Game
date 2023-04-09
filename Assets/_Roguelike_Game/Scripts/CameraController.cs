using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public float moveSpeed;

    public Transform target;

    public Camera mainCamera, bigMapCamera;

    private bool bigMapActive;

    public bool isBossRoom;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(isBossRoom)
        {
            target = PlayerController.Ins.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, target.position.y, transform.position.z), moveSpeed * Time.deltaTime);

        }

        if (Input.GetKeyDown(KeyCode.M) && !isBossRoom)
        {
            if (!bigMapActive)
            {
                ActivateBigMap();
            }
            else
            {
                DeactiveBigMap();
            }
        }
    }

    public void ChangeTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void ActivateBigMap()
    {
        if (!LevelManager.instance.isPaused)
        {
            bigMapActive = true;

            bigMapCamera.enabled = true;
            mainCamera.enabled = false;

            PlayerController.Ins.canMove = false;
            Time.timeScale = 0f;

            UIController.Ins.mapDisplay.SetActive(false);
            UIController.Ins.bigMapText.SetActive(true);
        }
    }

    public void DeactiveBigMap()
    {
        if (!LevelManager.instance.isPaused)
        {
            bigMapActive = false;
            bigMapCamera.enabled = false;
            mainCamera.enabled = true;

            PlayerController.Ins.canMove = true;
            Time.timeScale = 1f;

            UIController.Ins.mapDisplay.SetActive(true);
            UIController.Ins.bigMapText.SetActive(false);
        }
    }
}
