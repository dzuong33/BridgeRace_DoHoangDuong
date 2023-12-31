using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackBrick : MonoBehaviour
{
    [SerializeField] private GameObject stackPoint;
    [SerializeField] private float stackYIncreaseRate = 0.32f;
    [SerializeField] private float stackXposition = -0.18f;

    [SerializeField] List<GameObject> bricks = new List<GameObject>();

    public void Push(GameObject collectedBrick)
    {
        bricks.Add(collectedBrick);
        MoveToStackAnim(collectedBrick);
    }

    public void Pop()
    {
        var objref = bricks[bricks.Count - 1].gameObject;
        bricks[bricks.Count - 1].gameObject.SetActive(false);
        bricks.RemoveAt(bricks.Count - 1);
        Destroy(objref);

        // spawn 1 more brick
    }

    public bool isPopable()
    {
        if (bricks.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    void MoveToStackAnim(GameObject brick)
    {
        Vector3 targetPosition;

        if (bricks.Count == 1)
        {
            targetPosition = new Vector3(stackXposition, stackPoint.transform.localPosition.y, 0);
        }
        else
        {
            targetPosition = new Vector3(stackXposition, (bricks.Count - 1) * stackYIncreaseRate, 0);
        }

        brick.transform.parent = stackPoint.transform;
        brick.transform.localPosition = targetPosition;
        brick.transform.rotation = stackPoint.transform.rotation;
    }
   
}
