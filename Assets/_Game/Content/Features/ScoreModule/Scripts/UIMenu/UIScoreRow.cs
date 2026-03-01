using TMPro;
using UnityEngine;

namespace _Game.Content.Features.ScoreModule.Scripts.UIMenu
{
    public class UIScoreRow : MonoBehaviour
    {
        [SerializeField] private TMP_Text dateText;
        [SerializeField] private TMP_Text scoreText;

        public void Setup(string date, int score)
        {
            dateText.text = date;
            scoreText.text = score.ToString();
        }
    }
}