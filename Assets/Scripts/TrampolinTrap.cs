using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;


namespace Assets.Scripts.RogozinGame
{
    public class TrampolinTrap : TrampolinParent
    {
        
        private void Start()
        {
            TrampolinType = "trap";
            PrepareArea();
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.GetComponent<Rigidbody2D>().velocity.y <= 0f)
            {
                gameObject.SetActive(false);
            }
        }
    }
}