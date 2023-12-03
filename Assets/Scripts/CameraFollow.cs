using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;
    [SerializeField] float speed;
    [SerializeField] Vector3 distance;

    void Start()
    {
    }
    void LateUpdate()
    {
        Vector3 dPos = target.gameObject.transform.position + distance;
        Vector3 sPos = Vector3.Lerp(transform.position, dPos, speed * Time.smoothDeltaTime);
        transform.position = sPos;
    }
}
