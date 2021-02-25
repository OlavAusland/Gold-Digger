using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{

    public void LoadLevel(int index)
    {
        Application.LoadLevel(index);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
