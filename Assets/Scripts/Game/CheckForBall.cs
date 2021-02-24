using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForBall : MonoBehaviour
{
    public float sightRange;
    public bool ballInRange;

    public Animator anim;

    public bool raiseFlag, lowerFlag;

    public LayerMask whatIsBall;
    void Start()
    {
        raiseFlag = false;
        lowerFlag = true;
    }

    void Update()
    {
        //Check for ball in range
        ballInRange = Physics.CheckSphere(transform.position, sightRange, whatIsBall);

        if (!ballInRange)
        {
            LowerFlag();
        }

        if (ballInRange)
        {
            RaiseFlag();
        }
    }

    public void RaiseFlag()
    {
        anim.SetBool("RaiseFlag", true);
        anim.SetBool("LowerFlag", false);
    }

    public void LowerFlag()
    {
        anim.SetBool("RaiseFlag", false);
        anim.SetBool("LowerFlag", true);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
