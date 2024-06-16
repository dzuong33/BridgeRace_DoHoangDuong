using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class AIPlayer : G_Character
{
    [SerializeField] private List<GameObject> checkBoolean = new List<GameObject>();
    [SerializeField] private List<GameObject> targets = new List<GameObject>();
    private NavMeshAgent agent;
    [SerializeField] private Vector3 targetTransform;
    [SerializeField] private bool haveTarget;

    [SerializeField] private List<GameObject> checkbrickSpawned = new List<GameObject>();
    private int numOfBricks = 0;
    private GameObject brickSpawnContainer;
    private GameObject availableBridges;
    [SerializeField] private GameObject targetBridge;

    private void Start()
    {
        Debug.Log("restart");
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(GetTargets());
    }
    public void SetCurrentlyBridges()
    {
        availableBridges = currentlyStandingFloor.transform.GetChild(1).gameObject;
        targetBridge = availableBridges.transform.GetChild(Random.Range(0, availableBridges.transform.childCount)).transform.GetChild(0).gameObject;
    }
    public IEnumerator GetTargets()
    {
        if (targets.Count == 0)
        {
            brickSpawnContainer = currentlyStandingFloor.transform.GetChild(0).gameObject;
            for (int i = 0; i < brickSpawnContainer.transform.childCount; i++)
            {
                if (brickSpawnContainer.transform.GetChild(i).GetComponent<CollectableBrick>().color == playerColor)
                {
                    targets.Add(brickSpawnContainer.transform.GetChild(i).gameObject);
                }
            }
            haveTarget = false;
        }
        yield return new WaitForSeconds(0.5f);
    }

    public void RemoveTargetFromList(GameObject obj)
    {
        targets.Remove(obj);
        haveTarget = false;
    }

    void Update()
    {
        FindCurrentlyStandingFloor();
        HandleRotation();
        AIStates();
        //Debug.Log(haveTarget + " " + targets.Count + " " + gameObject.GetComponent<StackBrick>().isPopable());
    }

    void AIStates()
    {
        if (!haveTarget && targets.Count > 0)
        {
            targetTransform = targets[0].gameObject.transform.position;
            SetDestination(targetTransform);
            ChangeAnimation("run");
            haveTarget = true;
           
        }
        else if (!haveTarget && targets.Count == 0 && gameObject.GetComponent<StackBrick>().isPopable())
        {
            // place bricks to the bridge
            if (targetBridge == null)
            {
                return;
            }
            targetTransform = targetBridge.transform.position;
            SetDestination(targetTransform);
            ChangeAnimation("run");
            haveTarget = true;
        }
        else if (haveTarget && targets.Count == 0 && Vector3.Distance(gameObject.transform.position, targetTransform) < 0.3f)
        {
            // go to next bridge
            Debug.Log("ar");
            StartCoroutine(GetTargets());
            haveTarget = false;

        }
    }

    private void HandleRotation()
    {
        Vector3 direction = (targetTransform - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 9f);
    }

    public void ClearTarget()
    {
        targetTransform = new Vector3(0, 0, 0);
    }


    private Vector3 destination;

    //property tra ve ket qua xem la da toi diem muc tieu hay chua
    public bool IsDestionation => Vector3.Distance(transform.position, destination + (transform.position.y - destination.y) * Vector3.up) < 0.1f;

    //set diem den
    public void SetDestination(Vector3 destination)
    {
   
        this.destination = destination;
        agent.SetDestination(destination);
    }

}
