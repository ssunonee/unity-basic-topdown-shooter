public class EnemyController : IController
{
    public bool should_move;
    public bool should_shoot;
    public bool rotate_left, rotate_right;

    public bool GetMoveInput()
    {
        return should_move;
    }

    public bool GetRotateLeftInput()
    {
        return rotate_left;
    }

    public bool GetRotateRightInput()
    {
        return rotate_right;
    }

    public bool ShouldShoot()
    {
        return should_shoot;
    }
}
