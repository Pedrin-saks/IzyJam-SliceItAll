using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceableObjectController : MonoBehaviour
{
    public int point = 1;

    private Rigidbody[] rbs;
    [SerializeField]
    private float forceSlice;
    private bool isSliced;
    private void Awake()
    {
        rbs = GetComponentsInChildren<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isSliced == false && other.gameObject.CompareTag("Knife"))
        {
            isSliced = true;
            GameManager.instance.OnSlicedObjectTrigger(point);
            foreach (var rb in rbs)
            {
                rb.isKinematic = false;
                //var dir = new Vector3(rb.transform.localPosition.x * forceSlice, 0, 0);
                rb.AddForce(Vector3.right * rb.transform.localPosition.x * forceSlice);
            }
        }
    }
}
