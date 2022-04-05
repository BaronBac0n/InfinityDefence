using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSelect : MonoBehaviour
{
    public Transform infoCanvas;
    
    public Transform canvas1;
    public Transform canvas2;
    public Transform canvas3;

    public void OpenCanvas1()
    {
        canvas1.gameObject.SetActive(true);
        infoCanvas.gameObject.SetActive(false);
    }

    public void OpenCanvas2()
    {
        canvas2.gameObject.SetActive(true);
        infoCanvas.gameObject.SetActive(false);
    }

    public void OpenCanvas3()
    {
        canvas3.gameObject.SetActive(true);
        infoCanvas.gameObject.SetActive(false);
    }
    public void BackButton()
    {
        infoCanvas.gameObject.SetActive(true);
        canvas2.gameObject.SetActive(false);
        canvas1.gameObject.SetActive(false);
        canvas3.gameObject.SetActive(false);
    }

    public void OpenButton()
    {
        infoCanvas.gameObject.SetActive(true);
    }
}
