using UnityEngine;

public class SawDisc : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(Vector3.forward, Time.deltaTime * 35);
    }

    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == (int)LayersEnum.Actor)
        {
            collision.gameObject.GetComponent<Actor>().ReceiveHit();
        }
    }
}
