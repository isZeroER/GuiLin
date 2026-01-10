using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTrigger : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        RandomBallPos(); 
    }
    void RandomBallPos()
    {
        float tempPosX = Random.Range(GameCtrl.Instance.minPosX, GameCtrl.Instance.maxPosX);
        float tempPosZ = Random.Range(GameCtrl.Instance.minPosZ, GameCtrl.Instance.maxPosZ);
        Vector3 tempPosition = new Vector3(tempPosX, -0.2f, tempPosZ);
        transform.position = tempPosition;
    }
}
