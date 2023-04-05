using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class BossBulletBouce : MonoBehaviour
{
    public float speed = 7.5f;
    public Rigidbody2D theRB;

    public int bounceCount;
    public int currentBounceCount;

    Vector3 lastVelocity;

    private void Awake()
    {
        theRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        theRB.velocity = transform.right * speed;
    }

    private void FixedUpdate()
    {
        lastVelocity = theRB.velocity;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (currentBounceCount == bounceCount)
        {
            Destroy(gameObject);
        }
        else
        {
            if (other.gameObject.CompareTag("Block"))
            {
                var directionValue = Vector2.Reflect(lastVelocity.normalized, other.contacts[0].normal);
                transform.right = directionValue;
                lastVelocity = directionValue * Mathf.Max(speed, 0f);
                theRB.AddForce(new Vector2(Random.Range(-5, 5), Random.Range(-5, 5)));

                currentBounceCount++;
            }else if (other.gameObject.CompareTag("Player"))
            {
                PlayerHealthController.Ins.DamagePlayer();
                Destroy(gameObject);
            }
        }
    }
}
