using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

[RequireComponent(typeof(Collider))]
public class LootBox : MonoBehaviour
{
    public LootSystem lootSystem;

    [Header("LootBox")]
    public GameObject lootBox;
    public GameObject lootReward;
    public Animator lootAnimation;
    public GameObject lootPosition;

    [Space]
    [Header("Camera")]
    public Camera lootBoxCamera;

    [Space]
    [Header("VFX Objects")]
    public GameObject shakeVFX;
    public GameObject crashVFX;
    public GameObject lootVFX;

    private Animator animator;
    private RaycastHit hit;
    private Ray ray;

    private bool isOpened;
    private bool startTimer;
    private int timer;

    void Start()
    {
        animator = GetComponent<Animator>();
        shakeVFX.SetActive(false);
        crashVFX.SetActive(false);
        lootVFX.SetActive(false);

        startTimer = false;
        timer = 0;

        isOpened = false;
    }

    void Update()
    {
        ray = lootBoxCamera.ScreenPointToRay(Input.mousePosition);

        if (startTimer)
        {
            timer += 1;
        }

        if (timer == 30)
        {
            lootAnimation.SetBool("DisplayReward", true);
            startTimer = false;
            timer = 0;
        }

        if(Physics.Raycast(ray, out hit))
        {
            if(hit.transform.name == gameObject.name)
            {
                animator.SetBool("Idle", false);
                animator.SetBool("Hover", true);

                if(!isOpened)
                {
                    shakeVFX.SetActive(true);
                }

                if(Input.GetMouseButtonDown(0))
                {
                    animator.SetBool("Open", true);
                    shakeVFX.SetActive(false);

                    isOpened = true;
                }
            }
            else
            {
                shakeVFX.SetActive(false);
                animator.SetBool("Idle", true);
                animator.SetBool("Hover", false);
            }
        }
    }

    //Called via animation
    public void LootReward()
    {
        lootSystem.Spawner();
    }

    public void LootVFX()
    {
        startTimer = true;
        crashVFX.SetActive(false);
        lootVFX.SetActive(true);
    }

    public void CrashVFX()
    {
        crashVFX.SetActive(true);
        lootVFX.SetActive(false);

        CameraShaker.Instance.ShakeOnce(4f, 10f, .1f, 1f);
    }
}
