using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerControl : MonoBehaviour
{
    public BaseUnit unitData = null;
    public bool isEnemy = false;
    public AudioClip collisionSound = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (unitData.health <= 0)
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isEnemy && unitData != null && collision.gameObject.tag.Equals("Player"))
        {
            PlayerControl pc = collision.gameObject.GetComponent<PlayerControl>();
            pc.usingUnit.health -= unitData.Attack - pc.usingUnit.Defend;
            AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("damage"), Camera.main.transform.position);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, pc.jumpForce * 20));
            collision.gameObject.GetComponent<Animator>().SetTrigger("Damage");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //TODO：播放被攻击动画，音效
        if (collision.gameObject.tag.Equals("Player"))
        {
            if (isEnemy)
            {
                if (unitData != null)
                {
                    PlayerControl pc = collision.gameObject.GetComponent<PlayerControl>();
                    pc.usingUnit.health -= unitData.Attack - pc.usingUnit.Defend;
                    AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("damage"), Camera.main.transform.position);
                    collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, pc.jumpForce * 20));
                    collision.gameObject.GetComponent<Animator>().SetTrigger("Damage");
                }
            }
            else//金币
            {
                PlayerControl pc = collision.gameObject.GetComponent<PlayerControl>();
                pc.usingUnit.health += unitData.coinLevel;
                Destroy(gameObject);
                if (collisionSound != null)
                    AudioSource.PlayClipAtPoint(collisionSound, Camera.main.transform.position);
            }
        }
    }
}
