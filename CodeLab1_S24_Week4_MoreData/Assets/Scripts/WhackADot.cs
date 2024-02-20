using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhackADot : MonoBehaviour
{
  void OnMouseDown()
  {
    Debug.Log("you whacked a dot");
    transform.position = new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f));
    GameManager.instance.Score++;

  }
}
