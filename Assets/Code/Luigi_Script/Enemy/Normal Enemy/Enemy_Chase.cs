using UnityEngine;

[System.Serializable]
public class Enemy_Chase : EnemyState
{
    [SerializeField] private float chaseRadius = 20f;

    public override void OnEnter(Enemy_Controller _controller)
    {
        _controller.Agent.SetDestination(_controller.PlayerTransform.position);
    }

    public override void OnUpdate(Enemy_Controller _controller)
    {
        float distanceToPlayer = Vector3.Distance(_controller.transform.position, _controller.PlayerTransform.position);

        _controller.Agent.SetDestination(_controller.PlayerTransform.position);

        if (distanceToPlayer > chaseRadius)
        {
            _controller.SetState(_controller.PatrolState);
        }
    }

    public override void OnExit(Enemy_Controller _controller)
    {
    }

    public override void OnCollision(Enemy_Controller _controller, Collider _collision)
    {
        Enemy_Controller enemyController = _collision.GetComponent<Enemy_Controller>();
    }

    public override void DrawGizmo(Enemy_Controller _controller)
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(_controller.transform.position, chaseRadius);

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(_controller.transform.position, _controller.PlayerTransform.position);
    }
}
