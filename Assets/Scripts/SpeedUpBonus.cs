using UnityEngine;

public class SpeedUpBonus : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(Vector3.up, Time.deltaTime * 50);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == (int)LayersEnum.Actor)
        {
            collision.gameObject.GetComponent<Actor>().ReceiveSpeedUp();
            SpeedUpManager.speedUps_active--;
            Destroy(gameObject);
        }
        else if (collision.gameObject.layer == (int)LayersEnum.Obstacles)
        {
            var pos = transform.position;
            Destroy(transform.GetComponent<Rigidbody>());
            transform.position = new Vector3(pos.x, pos.y + 0.5f, pos.z);
        }
    }
}
