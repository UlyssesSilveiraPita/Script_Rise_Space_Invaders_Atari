using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI score_txt; 

    [Header("Variaveis")]
    [SerializeField] public int score_game;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Score();
    }

    void Score()
    {
        score_game = score_game;
        score_txt.text = score_game.ToString();

    }
}
