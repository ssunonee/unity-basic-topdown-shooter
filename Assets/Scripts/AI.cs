using UnityEngine;

public class AI : MonoBehaviour
{
    private EnemyController controller;

    private void Awake()
    {
        controller = (EnemyController) transform.GetComponent<Actor>().GetController();
        controller.should_shoot = true;
    }

    private void OnEnable()
    {
        InvokeRepeating("RandomizeAction", 0f, 1f);
    }

    private void OnDisable()
    {
        CancelInvoke("RandomizeAction");
    }

    private void RandomizeAction()
    {
        if (controller == null)
            return;

        controller.should_move = Random.Range(0, 2) == 0 ? true : false;
        controller.rotate_left = Random.Range(0, 2) == 0 ? true : false;
        if (controller.rotate_left == false)
            controller.rotate_right = Random.Range(0, 2) == 0 ? true : false;
    }
}
