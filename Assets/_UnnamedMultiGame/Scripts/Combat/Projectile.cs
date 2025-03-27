using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1f;

    public float Speed { get => _speed; set => _speed = value; }

    private void Update()
    {
        Advance();
    }

    private void Advance()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;
    }
}
