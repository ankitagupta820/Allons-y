using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instructions_opener : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Panel;

    public void OpenPanel()
    {
        if(Panel!=null)
        {
            Panel.SetActive(true);
        }
    }
}
