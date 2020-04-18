using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePanelManager : MonoBehaviour
{
    public GameObject contents;

    public virtual void Show()
    {
        contents.SetActive(true);
    }

    public virtual void Hide()
    {
        contents.SetActive(false);
    }
}
