using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.RogozinGame
{
    public class GameManager : MonoBehaviour
    {
        private int difficulty = 1;
        private int counTPlatformsUsed = 0;
        private bool isGameOver = false;

        private Dictionary<int, string[]> difficultySettings = new Dictionary<int, string[]>()
        {
            { 1, new string[]{"0.5", "8", "3"} },
            { 2, new string[]{"0.4", "7", "4"} },
            { 3, new string[]{"0.3", "6", "5"} },
            { 4, new string[]{"0.2", "5", "6"} },
        };

        public int Difficulty { get => difficulty; set => difficulty = value; }
        public int countPlatformsUsed { get => counTPlatformsUsed; set => counTPlatformsUsed = value; }
        public Dictionary<int, string[]> DifficultySettings { get => difficultySettings; set => difficultySettings = value; }
        public bool IsGameOver { get => isGameOver; set => isGameOver = value; }

        [SerializeField] GameObject rogozin;
        [SerializeField] GameObject gameOverText;
        [SerializeField] GameObject returnButton;

        private void Start()
        {
            InvokeRepeating("IncreaseDifficulty", 1f, 1f);
        }

        public void IncreaseDifficulty()
        {
            switch (countPlatformsUsed)
            {
                case 10:
                    Difficulty = 1;
                    break;
                case 20:
                    Difficulty = 2;
                    break;
                case 30:
                    Difficulty = 3;
                    break;
                case 40:
                    Difficulty = 4;
                    break;
            }
        }

        private void Update()
        {
            if (IsGameOver == true)
            {
                rogozin.SetActive(false);
                gameOverText.SetActive(true);
                returnButton.SetActive(true);
            }
        }

    }
}