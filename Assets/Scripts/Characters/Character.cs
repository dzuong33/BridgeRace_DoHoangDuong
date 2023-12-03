using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class G_Character : MonoBehaviour
{
    [SerializeField] Animator anim;
    private string currentAnim;

    public List<GameObject> stackBricks = new List<GameObject>();
    public GameObject currentlyStandingFloor;
    public Material playerColor;
    public int playerColorIndex;

    public bool isAI;
    private void Start()
    {
        OnInit();
    }
    public virtual void OnInit()
    {
    }
    public virtual void OnDespawn()
    {

    }
    protected void ChangeAnimation(string animName)
    {
        if (currentAnim != animName)
        {
            anim.ResetTrigger(animName);
            currentAnim = animName;
            anim.SetTrigger(currentAnim);
        }
    }
    protected void FindCurrentlyStandingFloor()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
        {
            if (hit.transform.gameObject != currentlyStandingFloor)
            {
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    currentlyStandingFloor = hit.transform.gameObject;
                }
            }
            if (isAI)
            {
                
                gameObject.GetComponent<AIPlayer>().SetCurrentlyBridges();
            }
        }
    }

    
    protected virtual void OnDeath()
    {

        ChangeAnimation("die");
    }
    
}
