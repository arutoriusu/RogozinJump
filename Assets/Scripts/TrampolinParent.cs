using System.Collections;
using System.Globalization;
using UnityEngine;

namespace Assets.Scripts.RogozinGame
{
    public class TrampolinParent : MonoBehaviour
    {

        [SerializeField] GameObject gameManagerObj;
        [SerializeField] GameObject prefabThisObject;

        private string trampolinType = "parent";

        public string TrampolinType { get => trampolinType; set => trampolinType = value; }
        public GameObject PrefabThisObject { get => prefabThisObject; set => prefabThisObject = value; }
        public GameObject GameManagerObj { get => gameManagerObj; set => gameManagerObj = value; }

        protected ArrayList PrepareArea()
        {
            ArrayList tramplines = new ArrayList();
            transform.localScale = new Vector3(
                float.Parse(
                    GameManagerObj.GetComponent<GameManager>().DifficultySettings[
                        GameManagerObj.GetComponent<GameManager>().Difficulty
                    ][0], CultureInfo.InvariantCulture),
                transform.localScale.y,
                transform.localScale.z);
            if (PrefabThisObject != null)
            {
                int countTrampolins = 0;
                if (TrampolinType == "regular" || TrampolinType == "movable")
                {
                    countTrampolins = int.Parse(
                                        GameManagerObj.GetComponent<GameManager>().DifficultySettings[GameManagerObj.GetComponent<GameManager>().Difficulty][1]
                                        );
                    
                } else if (TrampolinType == "trap")
                {
                    countTrampolins = int.Parse(
                                        GameManagerObj.GetComponent<GameManager>().DifficultySettings[GameManagerObj.GetComponent<GameManager>().Difficulty][2]
                                        );
                }
                for (int i = 0; i < countTrampolins; i++)
                {
                    Vector3 center = gameObject.transform.parent.GetChild(0).transform.position;
                    var t = Instantiate(PrefabThisObject, new Vector3(
                        center.x + Random.Range(-3, 3), 
                        center.y + Random.Range(-6, 6), 0), 
                        transform.rotation);

                    t.transform.SetParent(transform.parent);

                    if (TrampolinType == "regular" || TrampolinType == "movable")
                    {
                        t.GetComponent<Trampolin>().GameManagerObj = GameManagerObj;
                        t.GetComponent<Trampolin>().PrefabThisObject = null;
                    }
                    else if(TrampolinType == "trap")
                    {
                        t.GetComponent<TrampolinTrap>().GameManagerObj = GameManagerObj;
                        t.GetComponent<TrampolinTrap>().PrefabThisObject = null;
                    }

                    tramplines.Add(t);
                }
            }

            SetPosition();

            return tramplines;
        }

        private void SetPosition()
        {
            float randomCoordinate = Random.Range(-3, 3);
            if (randomCoordinate > 0)
            {
                randomCoordinate += 0.1f * Random.Range(0, 10);
            }
            else
            {
                randomCoordinate -= 0.1f * Random.Range(0, 10);
            }
            transform.position = new Vector3(randomCoordinate, transform.position.y, 0);
        }
    }
}