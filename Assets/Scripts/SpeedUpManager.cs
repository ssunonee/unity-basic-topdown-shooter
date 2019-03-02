using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpManager : MonoBehaviour
{
    public static int speedUps_active;

    public GameObject speedUp_prefab;
    public Transform speedUps_holder;

    void Start()
    {
        InvokeRepeating("SpawnSpeedUp", 0f, 1f);
    }

    private void SpawnSpeedUp()
    {
        if (speedUps_active < 10)
        {
            var speedUp = Instantiate(speedUp_prefab);
            var s_transform = speedUp.transform;
            s_transform.SetParent(speedUps_holder);

            var posX = Random.Range(-GameManager.area_extents.x, GameManager.area_extents.x);
            var posZ = Random.Range(-GameManager.area_extents.z, GameManager.area_extents.z);
            s_transform.localPosition = new Vector3(posX, s_transform.localPosition.y, posZ);
            speedUps_active++;
        }
    }
}
