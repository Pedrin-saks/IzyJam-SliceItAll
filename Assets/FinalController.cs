using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalController : MonoBehaviour
{

    public int multiplier;
    private bool isFinish;

    private void OnCollisionEnter(Collision collision)
    {
        if (isFinish == false && collision.gameObject.CompareTag("Knife"))
        {
            isFinish = true;
            GameManager.instance.FinishGame(multiplier);
        }
    }
}
