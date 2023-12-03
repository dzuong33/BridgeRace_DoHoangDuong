using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBrick : MonoBehaviour
{
    public Material color;
    public bool isOnStack = false;
    [SerializeField] private GameObject brickClone;
    
    private void OnTriggerEnter(Collider other)
    {
        Renderer source = gameObject.GetComponent<Renderer>();
        if (other.transform.GetComponent<G_Character>().playerColor == color)
        {      
            GameObject myBrick = Instantiate(brickClone);
            Renderer target = myBrick.GetComponent<Renderer>();
            target.material = source.material;
            other.transform.GetComponent<StackBrick>().Push(myBrick);    
            gameObject.SetActive(false);
        }
        if (other.transform.GetComponent<AIPlayer>() != null)
        {
            other.transform.GetComponent<AIPlayer>().RemoveTargetFromList(gameObject);
        }
        isOnStack = true;
    }
}
