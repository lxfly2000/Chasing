using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    public GameObject senderObject = null;
    public BaseUnit usingUnit = new BaseUnit();
    private AudioClip hitSound = null;

    // Start is called before the first frame update
    void Start()
    {
        PlayerControl pc = senderObject.GetComponent<PlayerControl>();
        pc.bulletCount++;
        usingUnit.health = usingUnit.MaxHealth;
        GetComponent<Rigidbody2D>().velocity = new Vector2(usingUnit.maxMoveSpeed, 0);
        hitSound = Resources.Load<AudioClip>("hit");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        usingUnit.health--;
        if (usingUnit.health <= 0)
        {
            Destroy(gameObject);
            senderObject.GetComponent<PlayerControl>().bulletCount--;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Enemy"))
        {
            AudioSource.PlayClipAtPoint(hitSound, Camera.main.transform.position);
            NonPlayerControl npc = collision.gameObject.GetComponent<NonPlayerControl>();
            PlayerControl pc = senderObject.GetComponent<PlayerControl>();
            npc.unitData.health -= Mathf.Max(0, pc.usingUnit.Attack - npc.unitData.Defend);
            Destroy(gameObject);
            senderObject.GetComponent<PlayerControl>().bulletCount--;
        }
    }
}
