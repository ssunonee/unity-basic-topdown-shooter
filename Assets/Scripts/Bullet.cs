using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Stats")]
    public float bullet_speed;

    private Rigidbody rigid;
    
    private void Awake()
    {
        rigid = transform.GetComponent<Rigidbody>();
        GameManager.OnResetGame += GameManager_OnResetGame;
    }

    public void Shoot()
    {
        rigid.velocity = transform.forward * bullet_speed;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == (int)LayersEnum.Actor)
        {
            collision.gameObject.GetComponent<Actor>().ReceiveHit();
            GameManager.Instance.StoreInactiveBullet(gameObject);
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.layer == (int)LayersEnum.Obstacles)
        {
            GameManager.Instance.StoreInactiveBullet(gameObject);
            gameObject.SetActive(false);
        }
    }

    private void GameManager_OnResetGame()
    {
        if (gameObject.activeSelf)
        {
            GameManager.Instance.StoreInactiveBullet(gameObject);
            gameObject.SetActive(false);
        }
    }
}
