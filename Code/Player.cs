using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private Shot shot { get; set; }
    [SerializeField] public GameManager gameManager;

    [Header("Componentes")]
    [SerializeField] private Rigidbody2D player_rb;
    [SerializeField] private Transform playerPosition_Shot;
    [SerializeField] private GameObject shot_Prefab;

    [Header("Variaveis")]
    [SerializeField] private float player_speed;
    [SerializeField] private float shot_TimeDelay;
    [SerializeField] public Vector3 initial_Position;

    


    void Start()
    {
        initial_Position = transform.position;
        shot_Prefab = shot_Prefab;
    }

    void Update()
    {
        InputManager();

    }

    private void LateUpdate()
    {
        /// DelayDisparo ///
        shot_TimeDelay -= Time.deltaTime;

        if (shot_TimeDelay <= 0  ) { shot_TimeDelay = 0; }
      
    }

    private void OnCollisionEnter2D (Collision2D col)
    {
        if( col.gameObject.CompareTag("shotEnemy"))
        {
            PlayerDead();
        }
    }

    void InputManager()
    {
        //--------------- Movimentacao do Personagem ----------------//

        float horizontal = Input.GetAxis("Horizontal") * player_speed;  
        player_rb.linearVelocityX = horizontal; 

        if(Input.GetButtonDown("Fire1"))
        {
            Shot();

        }


    }

    void Shot()
    {

        if (shot_TimeDelay > 0)
        {
            return;
        }
        else
        {
            GameObject shotPrefab = Instantiate(shot_Prefab);
            shotPrefab.transform.position = playerPosition_Shot.transform.position;
        }

        shot_TimeDelay = 1;

    }

    void PlayerDead()
    {
        player_rb.transform.position = initial_Position;
        gameManager.score_game = 0;

    }


}
