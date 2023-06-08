using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalController : MonoBehaviour
{

    public int multiplier;
    private bool isFinish;

    private void OnTriggerEnter(Collider other)
    {
        if (isFinish == false && other.gameObject.CompareTag("Knife"))
        {
            isFinish = true;
            GameManager.instance.FinishGame(multiplier);
        }
    }
}
