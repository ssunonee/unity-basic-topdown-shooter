using System;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public event Action OnDamaged = delegate { };
    public event Action OnDied = delegate { };

    [Header("Stats")]
    public int health;
    public float fire_rate;
    public float move_speed;
    public float rotation_speed;

    [Header("Other")]
    public Transform bullet_spawner;

    private IController controller;
    private Rigidbody rigid;
    private float next_shoot_time;

    private bool speeded_up;
    private float speed_up_timer;

    private void Start()
    {
        rigid = transform.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        HandleShooting();
        HandleRotation();
        HandleMovement();
        HandleSpeedingUp();
    }

    private void OnDestroy()
    {
        OnDamaged = delegate { };
        OnDied = delegate { };
    }

    public IController GetController()
    {
        return controller;
    }

    public void SetController(IController controller)
    {
        this.controller = controller;
    }

    private void HandleRotation()
    {
        if (controller.GetRotateLeftInput() == true)
        {
            rigid.angularVelocity = Vector3.down * rotation_speed * Time.deltaTime;
        }
        else if (controller.GetRotateRightInput() == true)
        {
            rigid.angularVelocity = Vector3.up * rotation_speed * Time.deltaTime;
        }
        else if (rigid.angularVelocity != Vector3.zero)
        {
            rigid.angularVelocity = Vector3.zero;
        }
    }

    private void HandleMovement()
    {
        if (controller.GetMoveInput() == true)
        {
            //horizontal
            var newVelocity = new Vector3(transform.forward.x, 0, transform.forward.z)
                * (speeded_up ? move_speed * 2 : move_speed)
                * Time.fixedDeltaTime;
            //vertical
            newVelocity.y = rigid.velocity.y;
            rigid.velocity = newVelocity;
        }
        else
        {
            rigid.velocity = new Vector3(0, rigid.velocity.y, 0);
        }
    }
    
    private void HandleShooting()
    {
        if (controller.ShouldShoot() == true)
        {
            if (Time.time >= next_shoot_time)
            {
                var bullet = GameManager.Instance.GetBullet();
                bullet.transform.position = bullet_spawner.position;
                bullet.transform.localRotation = transform.localRotation;
                bullet.gameObject.SetActive(true);
                bullet.GetComponent<Bullet>().Shoot();

                next_shoot_time = Time.time + (speeded_up ? fire_rate / 2 : fire_rate);
            }
        }
    }

    public void ReceiveHit()
    {
        if (health < 1)
            return; 

        if (health > 1)
        {
            health--;
            OnDamaged();
        }
        else
        {
            health--;
            Die();
        }
    }

    public void Die()
    {
        OnDied();
        Destroy(gameObject);
    }

    public void ReceiveSpeedUp()
    {
        speeded_up = true;
        speed_up_timer = 4f;
    }

    private void HandleSpeedingUp()
    {
        if (speeded_up == false)
            return;

        speed_up_timer -= Time.deltaTime;

        if (speed_up_timer <= 0)
            speeded_up = false;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == (int)LayersEnum.Void)
        {
            health = 0;
            Die();
        }
    }
}
