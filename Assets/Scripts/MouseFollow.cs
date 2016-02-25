using UnityEngine;
using System.Collections;

public class MouseFollow : MonoBehaviour
{
    void Update()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
