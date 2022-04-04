using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObject : MonoBehaviour
{
    public GameObject BattleShip;
    public float speed;
    private bool RotatingLeft = false;
    private bool RotatingRight = false;

    public void RotateLeft()
    {
        RotatingRight = false;

        if (RotatingLeft == false)
        {
            RotatingLeft = true;
        }
        else
        {
            RotatingLeft = false;
        }
    }

    public void RotateRight()
    {
        RotatingLeft = false;

        if (RotatingRight == false)
        {
            RotatingRight = true;
        }
        else
        {
            RotatingRight = false;
        }
    }

    public void StopRotate()
    {
        RotatingLeft = false;
        RotatingRight = false;
    }

    private void Update()
    {
        if (RotatingLeft == true)
        {
            BattleShip.transform.Rotate(Vector3.up * speed * Time.deltaTime);
        }

        if (RotatingRight == true)
        {
            BattleShip.transform.Rotate(Vector3.down * speed * Time.deltaTime);
        }
    }
}
