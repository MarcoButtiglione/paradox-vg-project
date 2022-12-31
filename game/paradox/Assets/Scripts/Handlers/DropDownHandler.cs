using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropDownHandler : MonoBehaviour
{
    public Dropdown m_Dropdown;
    public Text m_Text;
    public List<ResItem> resolution = new List<ResItem>();
    private int selectedResolution;
    public Toggle fullscreen;


    private void Start()
    {
        m_Dropdown = GetComponent<Dropdown>();
        m_Dropdown.onValueChanged.AddListener(delegate {
            DropdownValueChanged(m_Dropdown);
        });

        m_Text.text = "First Value : " + m_Dropdown.value;
    }
    public void DropdownValueChanged(Dropdown change)
    {
        selectedResolution = change.value;
        setResolution(change.value);
    }
    public void setResolution(int index)
    {
        if (index == 0)
        {
            fullscreen.isOn = true;
            Screen.SetResolution(resolution[index].horizontal, resolution[index].vertical, FullScreenMode.FullScreenWindow);
        }
        else
        {
            fullscreen.isOn = false;
            Screen.SetResolution(resolution[index].horizontal, resolution[index].vertical, FullScreenMode.Windowed);
        }
    }
}
[System.Serializable]public class ResItem
{
    public int horizontal, vertical;
}