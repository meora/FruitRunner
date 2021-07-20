using UnityEngine;
using UnityEngine.UI;

public class DisplayedPoints : MonoBehaviour
{
    public static int ScorePoints;

    [SerializeField] private Text _text;

    private void Awake()
    {
        ScorePoints = 0;
    }

    private void FixedUpdate()
    {
        ScorePoints++;
        if (ScorePoints < 10)
        {
            _text.text = "000" + ScorePoints.ToString();
        }
        else if(ScorePoints >= 10 && ScorePoints <= 100)
        {
            _text.text = "00" + ScorePoints.ToString();
        }
        else if (ScorePoints >= 100 && ScorePoints <= 1000)
        {
            _text.text = "0" + ScorePoints.ToString();
        }
        else 
        {
            _text.text = ScorePoints.ToString();
        }
    }
}
