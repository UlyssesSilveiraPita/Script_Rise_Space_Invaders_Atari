using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] public Player player;
    [SerializeField] public GameManager gameManager;

    [Header("Componentes")]
    [SerializeField] private Rigidbody2D enemy_rb;
    [SerializeField] private Transform enemy_Transform;
    [SerializeField] private Transform enemy_ShotPosition;
    [SerializeField] private GameObject enemy_ShotPrefab;

    [Header("Variaveis")]
    [SerializeField] private float enemy_speed;
    [SerializeField] private Transform enemy_firstPosition;
    [SerializeField] private float shootCooldown;
    [SerializeField] private float descendAmount; // 1 pixel em unidades Unity
    [SerializeField] private float speedIncrease; // quanto aumenta a velocidade a cada ida

    [Header("Configuração de Tiro")]
    private float minShootDelay = 3f;  // tempo mínimo entre tiros
    private float maxShootDelay = 20f;    // tempo máximo entre tiros
    private float nextShootTime;

    [Header("Movimentacao")]
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    private bool movingToB = true; // direção inicial

    private Vector3 currentPointA;
    private Vector3 currentPointB;

    void Start()
    {
        // Define os pontos iniciais baseados nos Transforms originais
        currentPointA = pointA.position;
        currentPointB = pointB.position;
        enemy_Transform.position = enemy_firstPosition.position;
        SetNextShootTime();
    }

    void Update()
    {
        MoveBetweenPoints();
        HandleShooting();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("shot"))
        {
           gameObject.SetActive(false);
            gameManager.score_game += 100;
        }

        if (col.CompareTag("Player"))
        {
            player.transform.position = player.initial_Position;
            gameManager.score_game = 0;
        }
    }


    // ---------------- MOVIMENTO ENTRE A E B ----------------
    void MoveBetweenPoints()
    {
        if (movingToB)
        {
            enemy_Transform.position = Vector2.MoveTowards(enemy_Transform.position, currentPointB, enemy_speed * Time.deltaTime);

            // Chegou em B
            if (Vector2.Distance(enemy_Transform.position, currentPointB) < 0.01f)
            {
                movingToB = false;
                DescendAndSpeedUp(); // ⬇️ desce só quando chega no B
            }
        }
        else
        {
            enemy_Transform.position = Vector2.MoveTowards(enemy_Transform.position, currentPointA, enemy_speed * Time.deltaTime);

            // Chegou em A apenas muda direção, sem descer
            if (Vector2.Distance(enemy_Transform.position, currentPointA) < 0.01f)
            {
                movingToB = true;
            }
        }
    }


    // ---------------- DESCER E AUMENTAR VELOCIDADE ----------------
    void DescendAndSpeedUp()
    {
        // Desce os pontos e o inimigo juntos
        currentPointA.y -= descendAmount;
        currentPointB.y -= descendAmount;

        enemy_speed += speedIncrease;
    }

    // ---------------- SISTEMA DE TIRO ----------------
    void HandleShooting()
    {
        if (Time.time >= nextShootTime)
        {
            Shoot();
            SetNextShootTime();
        }
    }

    void Shoot()
    {
        Instantiate(enemy_ShotPrefab, enemy_ShotPosition.position, Quaternion.identity);
    }

    void SetNextShootTime()
    {
        float delay = UnityEngine.Random.Range(minShootDelay, maxShootDelay);
        nextShootTime = Time.time + delay;
    }

}
