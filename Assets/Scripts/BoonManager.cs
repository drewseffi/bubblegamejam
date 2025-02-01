using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoonManager : MonoBehaviour
{
    public GameObject me;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        me.transform.Rotate(0, 25 * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StateManager.Instance.Heal();
            me.gameObject.SetActive(false);
            AudioSource src = player.GetComponent<AudioSource>();
            audioManager.Instance.PlaySFX("BoonOnClick", src);
        }
    }
}
