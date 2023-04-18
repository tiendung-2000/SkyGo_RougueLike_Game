using API.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Ins;

    public float moveSpeed;
    private Vector2 moveInput;

    public Rigidbody2D theRB;
    public Transform playerHand;
    //private Camera theCam;
    public Animator anim;

    public SpriteRenderer bodySR;

    private float activeMoveSpeed;
    public float dashSpeed = 8f, dashLength = .5f, dashCooldown = 1f, dashInvincibisity = .5f;
    [HideInInspector]
    public float dashCounter;
    private float dashCoolCounter;
    [HideInInspector]
    public bool canMove = true;

    public List<Weapon> availableWeapons = new List<Weapon>();

    [HideInInspector]
    public int currentWeapon;

    private void Awake()
    {
        Ins = this;

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        activeMoveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove && !LevelManager.instance.isPaused)
        {
            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");

            //moveInput.Normalize();


            theRB.velocity = moveInput * activeMoveSpeed;

            Vector3 mousePos = Input.mousePosition;
            Vector3 screenPoint = CameraController.instance.mainCamera.WorldToScreenPoint(transform.localPosition);

            if (mousePos.x < screenPoint.x)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
                playerHand.localScale = new Vector3(-1f, -1f, 1f);
            }
            else
            {
                transform.localScale = Vector3.one;
                playerHand.localScale = Vector3.one;
            }

            //rotate gun arm
            Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
            float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
            playerHand.rotation = Quaternion.Euler(0, 0, angle);

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if(availableWeapons.Count > 0)
                {
                    currentWeapon++;
                    if(currentWeapon >= availableWeapons.Count)
                    {
                        currentWeapon = 0;
                    }

                    SwitchWeapon();
                }
                else
                {
                    Debug.LogError("Player Has No Gun!");
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (dashCoolCounter <= 0 && dashCounter <= 0)
                {
                    activeMoveSpeed = dashSpeed;
                    dashCounter = dashLength;

                    anim.SetTrigger("dash");
                    PlayerHealthController.Ins.MakeInvincible(dashInvincibisity);

                    AudioController.instance.PlaySFX(8);
                }
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                CanvasManager.Ins.OpenUI(UIName.WinUI, null);
            }

            if (dashCounter > 0)
            {
                dashCounter -= Time.deltaTime;
                if (dashCounter <= 0)
                {
                    activeMoveSpeed = moveSpeed;
                    dashCoolCounter = dashCooldown;
                }
            }

            if (dashCoolCounter > 0)
            {
                dashCoolCounter -= Time.deltaTime;
            }


            if (moveInput != Vector2.zero)
            {
                anim.SetBool("isMoving", true);
            }
            else
            {
                anim.SetBool("isMoving", false);
            }
        }
        else
        {
            theRB.velocity = Vector2.zero;
            anim.SetBool("isMoving", false);
        }
    }

    public void SwitchWeapon()
    {
        foreach(Weapon theGun in availableWeapons)
        {
            theGun.gameObject.SetActive(false);
        }

        availableWeapons[currentWeapon].gameObject.SetActive(true);
    }
}
