using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public List<GameObject> health_images;

    public void RefreshBar(int health)
    {
        for (int i = 0; i < health; i++)
        {
            if (health_images[i].activeSelf == false)
                health_images[i].SetActive(true);
        }
        for (int i = health; i < health_images.Count; i++)
        {
            if (health_images[i].activeSelf == true)
                health_images[i].SetActive(false);
        }
    }
}
