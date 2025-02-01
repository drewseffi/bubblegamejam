using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static StateManager Instance;
    public bool isPaused = false;

    public int wave = 0;

    public TMP_Text dieText;

    public GameObject chef;

    public GameObject skelly;

    public Transform player;

    public int enemyCount;

    public Transform bubble;

    private Vector3 maxBubbleScale;

    public float duration = 30f;

    private float t = 0;

    public float bubbleSpeed;

    public int weapon = 1;

    public TMP_Text waveText;

    public GameObject cutlass;
    public GameObject blunderbuss;

    public Canvas pauseMenu;

    public GameObject boon;
    public GameObject stall;
    private bool boonSpawn;

    public TMP_Text fpsText;

    public bool dead;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        dead = false;
    }

    void Start()
    {
        maxBubbleScale = new Vector3(5000, 5000, 5000);
        bubble.localScale = maxBubbleScale;
        wave = wave + 1;
        Spawn();
        //StartCoroutine(ShrinkBubble());
        cutlass.SetActive(true);
        blunderbuss.SetActive(false);
        weapon = 1;
        boonSpawn = false;
        //AudioSource src = audioManager.Instance.GetComponent<AudioSource>();
        audioManager.Instance.PlayMusic("T1");
        dead = false;
    }

    public void Expand()
    {
        Vector3 expand = new Vector3(500, 500, 500);
        if ((bubble.localScale.x + 500) <= 5000)
        {
            bubble.localScale += expand;
        }
    }

    public void Heal()
    {
        PlayerHealth p = player.gameObject.GetComponent<PlayerHealth>();
        p.currentHealth = 100;
    }

    public void Die()
    {
        dieText.gameObject.SetActive(true);
    
        //StartCoroutine("Wait", 1f);
        Time.timeScale = 0f;
        dead = true;
    }

    IEnumerable Wait(float duration)
    {
        yield return new WaitForSeconds(duration);
    }

    void Pause()
    {
        if(isPaused)
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            pauseMenu.gameObject.SetActive(true);
        }
        else 
        {
            Time.timeScale = 1f;
            isPaused = false;
            Cursor.lockState = CursorLockMode.Locked;
            pauseMenu.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        t =+ Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Escape) && isPaused == false)
        {
            isPaused = true;
            Pause();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused == true)
        {
            isPaused = false;
            Pause();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weapon = 1;
            cutlass.SetActive(true);
            blunderbuss.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weapon = 2;
            cutlass.SetActive(false);
            blunderbuss.SetActive(true);
        }

        float fps = 1.0f / Time.deltaTime;

        fpsText.text = "FPS:" + Convert.ToString(fps);

        //Debug.Log("FPS: " + fps);


        if (enemyCount == 0)
        {
            wave++;
            Spawn();
        }

        ShrinkBubble();
    }

    void Spawn()
    {
        if ((wave % 2) == 0)
        {
            boon.SetActive(true);
            stall.SetActive(true);
            boonSpawn = true;
        }
        else
        {
            boon.SetActive(false);
            stall.SetActive(false);
            boonSpawn = false;
        }


        waveText.text = "Wave: " + wave;

        for (int i = 0; i <= (wave + 4); i++)
        {
            float x = UnityEngine.Random.Range(-49f, 49f);
            float z = UnityEngine.Random.Range(-49f, 49f);

            Vector3 randomPos = new Vector3(x, 0.51f, z);

            int ranSel = UnityEngine.Random.Range(1,3);
            
            GameObject newEnemy = new GameObject();

            if (ranSel == 1)
            {
                newEnemy = Instantiate(chef, randomPos, Quaternion.identity);
                newEnemy.GetComponent<MeleeEnemy>().player = player;
            }
            else if (ranSel == 2)
            {
                newEnemy = Instantiate(skelly, randomPos, Quaternion.identity);
                newEnemy.GetComponent<Skelly>().player = player;
            }


            enemyCount = wave + 5;
        }
    }

    void ShrinkBubble()
    {
        Vector3 change = new Vector3(-50,-50,-50);



        bubble.localScale += change * Time.deltaTime;


    }
}
