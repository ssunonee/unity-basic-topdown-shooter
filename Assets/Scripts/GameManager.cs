using System;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static event Action OnResetGame = delegate { };

    public static Queue<GameObject> inactive_bullets;

    [Header("Resources")]
    public GameObject actor_prefab;
    public Material player_material;
    public Material enemy_material;
    public GameObject bullet_prefab;
    public GameObject sawDisc_prefab;

    [Header("Initialization")]
    public int spawn_enemies;
    public GameObject spawn_area;
    public Transform saw;
    public Transform players_holder;
    public Transform bullets_holder;

    public static Vector3 area_extents;

    public static GameObject player;
    private int enemies_alive;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
        inactive_bullets = new Queue<GameObject>();
        area_extents = spawn_area.GetComponent<MeshRenderer>().bounds.extents;
    }

    private void Start()
    {
        InitGame();
    }

    public void InitGame()
    {
        InitSaw();
        enemies_alive = 0;
        InstantiateActor(ActorType.Player);
        for (int i = 0; i < spawn_enemies-1; i++)
        {
            InstantiateActor(ActorType.Enemy);
        }
    }

    public void InitSaw()
    {
        for (int i = 0; i < 90; i++)
        {
            var sawDisc = Instantiate(sawDisc_prefab);
            var sd_transform = sawDisc.transform;
            sd_transform.SetParent(saw);
            sd_transform.localPosition = new Vector3(0, 0, i * sd_transform.localScale.z * -2);
            sd_transform.localEulerAngles = new Vector3(0, 0, -5f * i);
        }
    }

    public void RestartGame()
    {
        OnResetGame();
        for (int i = players_holder.childCount-1; i >= 0; i--)
        {
            Destroy(players_holder.GetChild(i).gameObject);
        }
        player = null;

        enemies_alive = 0;
        InstantiateActor(ActorType.Player);
        for (int i = 0; i < spawn_enemies-1; i++)
        {
            InstantiateActor(ActorType.Enemy);
        }
    }

    public void OnPlayerDamaged()
    {
        UIManager.Instance.RefreshUI();
    }

    public void OnPlayerDied()
    {
        UIManager.Instance.RefreshUI();
        UIManager.Instance.ShowLosePanel();
    }

    public void OnEnemyDied()
    {
        enemies_alive--;
        if (enemies_alive == 0)
            UIManager.Instance.ShowWinPanel();
    }

    public GameObject InstantiateActor(ActorType type)
    {
        var actor = Instantiate(actor_prefab);
        actor.transform.SetParent(players_holder);

        if (type == ActorType.Player)
        {
            actor.GetComponent<Actor>().SetController(new PlayerController());
            actor.GetComponent<MeshRenderer>().sharedMaterial = player_material;
            actor.GetComponent<Actor>().OnDamaged += OnPlayerDamaged;
            actor.GetComponent<Actor>().OnDied += OnPlayerDied;
            player = actor;
            UIManager.Instance.RefreshUI();
        }
        else if (type == ActorType.Enemy)
        {
            actor.GetComponent<Actor>().SetController(new EnemyController());
            actor.GetComponent<MeshRenderer>().sharedMaterial = enemy_material;
            actor.AddComponent<AI>();
            actor.GetComponent<Actor>().OnDied += OnEnemyDied;
            enemies_alive++;
        }

        var posX = Random.Range(-area_extents.x, area_extents.x);
        var posZ = Random.Range(-area_extents.z, area_extents.z);
        actor.transform.localPosition = new Vector3(posX, 0, posZ);

        return actor;
    }

    public GameObject GetBullet()
    {
        if (inactive_bullets.Count > 0)
        {
            return inactive_bullets.Dequeue();
        }
        else
        {
            var bullet = Instantiate(bullet_prefab);
            bullet.transform.SetParent(bullets_holder);
            return bullet;
        }
    }

    public void StoreInactiveBullet(GameObject bullet)
    {
        inactive_bullets.Enqueue(bullet);
    }
}

public enum ActorType
{
    Player, Enemy
}
