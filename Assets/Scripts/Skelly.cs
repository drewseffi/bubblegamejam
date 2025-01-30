using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Skelly : MonoBehaviour
{
    public Transform player;
    public Transform enemyCollider;
    public float speed = 3f;

    public float attackRange = 5f;

    public Transform empty;
    public bool isAttacking = false;

    public LayerMask playerLayer;

    public Animator anim;

    public GameObject hitbox;

    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 offset = player.transform.position - enemyCollider.transform.position;
        float distance = offset.magnitude;  

        empty.LookAt(player.position);

        if (distance <= attackRange && isAttacking == false)
        {
            StartCoroutine(Attack());
        }
        else
        {
            Follow();
        }
    }

    void Follow ()
    {
        var p = player.position;
        p.y = transform.position.y;
        transform.LookAt(p);

        transform.Rotate(0f, 0f, 0f, Space.World);

        Vector3 flatPos = player.position;
        flatPos.y = 0;

        transform.position = Vector3.MoveTowards(transform.position, flatPos, speed * Time.deltaTime);

        
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        //anim.SetInteger("Attacking", 1);
        speed = 0f;

        yield return new WaitForSeconds(0.14f);

        hitbox.SetActive(true);

        yield return new WaitForSeconds(1f);

        hitbox.SetActive(false);
        //anim.SetInteger("Attacking", 0);
        speed = 6;
        isAttacking = false;
    }
}
