using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowMaker : MonoBehaviour
{

    public void PopoutNewWindow(GameObject window)
    {
        Instantiate(window, transform);
    }

}
