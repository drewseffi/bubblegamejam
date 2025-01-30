using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Attacking : MonoBehaviour
{
    public float cutlassRange = 3f;
    public float cutlassAttackSpeed = 10000f;
    public int cutlassDamage = 10;

    public int blunderbussDamage = 7;

    public Camera fpsCam;
    public LayerMask enemy;

    public Animator anim;

    private float nextTimeToFire = 0f;

    public AudioSource src;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire && StateManager.Instance.isPaused == false)
        {
            nextTimeToFire = Time.time + 1f/cutlassAttackSpeed;
            if (StateManager.Instance.weapon == 1)
            {
                Cutlass();
            }
            else
            {
                Blunderbuss();
            }
        }
    }

    void Cutlass()
    {
        RaycastHit hit;

        anim.Play("Base Layer.Swing");

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, cutlassRange, enemy))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(cutlassDamage);
            }
        }

        audioManager.Instance.PlaySFX("CutlassAuto", src);

    }

       void Blunderbuss()
    {
        RaycastHit hit;

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, enemy))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(blunderbussDamage);
            }
        }

        audioManager.Instance.PlaySFX("BlunderbussAuto", src);

    }
}
