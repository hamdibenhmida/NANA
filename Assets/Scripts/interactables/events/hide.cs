using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hide : MonoBehaviour
{
    [SerializeField] private Transform player;
   public void Hide (Transform Place)
    {
        player.position = Place.position;
    }
}
