using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeConstructorHandle : MonoBehaviour
{
    [SerializeField] Material[] brickMaterials;

    private StackBrick stackManager;
    private GameObject player;
    private G_Character playerScript;
    [SerializeField] private int bridgeIndexBiggest = 0;

    private void Start()
    {
        stackManager = gameObject.transform.parent.GetComponent<StackBrick>();
        player = gameObject.transform.parent.gameObject;
        playerScript = player.GetComponent<G_Character>();
    }

    private void Update()
    {
        HandleBridgeConstruction();
    }

    void HandleBridgeConstruction()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 1000, Color.white);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
        {
            if (hit.transform.gameObject.tag == "Stair")
            {
                // for handling going backwards in the middle of the bridge
                if (bridgeIndexBiggest == 0 || bridgeIndexBiggest < int.Parse(hit.transform.gameObject.name))
                {
                    bridgeIndexBiggest = int.Parse(hit.transform.gameObject.name);
                    hit.transform.GetChild(0).GetComponentInChildren<BoxCollider>().enabled = false;
                }
                else if (bridgeIndexBiggest > int.Parse(hit.transform.gameObject.name))
                {
                    // going backwards can pass
                    hit.transform.GetChild(0).GetComponentInChildren<BoxCollider>().enabled = false;
                    return;
                }

                if (stackManager.isPopable())
                {
                    if (hit.transform.gameObject.GetComponent<MeshRenderer>().enabled == false || hit.transform.gameObject.GetComponent<BridgeSteps>().colorIndex != playerScript.playerColorIndex)
                    {
                        hit.transform.gameObject.GetComponent<BridgeSteps>().ColorBrick(playerScript.playerColorIndex);
                        stackManager.Pop();
                    }
                }
                else
                {
                    if (playerScript.isAI)
                    {
                        player.GetComponent<AIPlayer>().ClearTarget();
                        StartCoroutine(player.GetComponent<AIPlayer>().GetTargets());
                    }
                    hit.transform.GetChild(0).GetComponentInChildren<BoxCollider>().enabled = true;
                    
                }
            }
            else if (hit.transform.gameObject.tag == "Ground")
            {
                bridgeIndexBiggest = 0;
            }
        }
    }
}
