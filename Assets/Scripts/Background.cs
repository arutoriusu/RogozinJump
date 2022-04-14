using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.RogozinGame
{
    public class Background : MonoBehaviour
    {
        [SerializeField] GameObject Rogozin;
        [SerializeField] GameObject backgroundPrefab;
        [SerializeField] GameObject gameManagerObj;
        [SerializeField] GameObject leftBorder;
        [SerializeField] GameObject rightBorder;

        private bool created = false;

        public GameObject RightBorder { get => rightBorder; set => rightBorder = value; }
        public GameObject LeftBorder { get => leftBorder; set => leftBorder = value; }
        public GameObject GameManagerObj { get => gameManagerObj; set => gameManagerObj = value; }
        public GameObject BackgroundPrefab { get => backgroundPrefab; set => backgroundPrefab = value; }
        public GameObject Rogozin1 { get => Rogozin; set => Rogozin = value; }

        // Update is called once per frame
        private void FixedUpdate()
        {
            if (Rogozin1.transform.position.y >= transform.GetChild(0).position.y)
            {
                if (!created && BackgroundPrefab != null)
                {
                    var b = Instantiate(BackgroundPrefab, new Vector3(0, transform.position.y + 10, 0), transform.rotation);
                    b.GetComponent<Background>().Rogozin1 = Rogozin1;
                    b.GetComponent<Background>().BackgroundPrefab = BackgroundPrefab;
                    b.GetComponent<Background>().GameManagerObj = GameManagerObj;
                    b.GetComponent<Background>().LeftBorder = LeftBorder;
                    b.GetComponent<Background>().RightBorder = RightBorder;
                    b.GetComponentInChildren<Trampolin>().GameManagerObj = GameManagerObj;
                    b.GetComponentInChildren<TrampolinTrap>().GameManagerObj = GameManagerObj;
                    b.GetComponentInChildren<Trampolin>().LeftBorder = LeftBorder;
                    b.GetComponentInChildren<Trampolin>().RightBorder = RightBorder;
                    b.transform.SetParent(gameObject.transform.parent.transform);
                    created = true;
                }
                else
                {
                    if (Rogozin1.transform.position.y >= transform.position.y + 20)
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}