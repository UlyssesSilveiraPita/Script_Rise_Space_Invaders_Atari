using UnityEngine;

public class Shield : MonoBehaviour
{


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("shot") || col.CompareTag("shotEnemy"))
        {
            //Destroy()
            Destroy(gameObject);    
        }
    }

}
