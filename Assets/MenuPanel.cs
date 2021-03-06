using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanel : MonoBehaviour
{
    public GameObject InstructionPanel;

    public void TogglePanel()
    {
        if(InstructionPanel != null)
        {
            bool isActive = InstructionPanel.activeSelf;
            InstructionPanel.SetActive(!isActive);
        }
    }

}
