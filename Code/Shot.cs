using UnityEngine;

public class Shot : MonoBehaviour
{
    [Header("Componentes")]
    [SerializeField] public GameObject shot;
    [SerializeField] private Rigidbody2D shot_rb;

    [Header("Variaveis")]
    [SerializeField] private float shot_Speed;

    void Start()
    {
        shot_rb.linearVelocityY = shot_Speed;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") || col.gameObject.CompareTag("Lim") || col.gameObject.CompareTag("Shield"))
        {
            Destroy(shot);
        }

    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Enemy"))
        {
            Destroy(shot);
        }
        if(col.CompareTag("Shield"))
        {
            Destroy(shot);
        }
    }


}
