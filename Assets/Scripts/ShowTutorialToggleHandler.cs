using UnityEngine;
using UnityEngine.UI;

public class ShowTutorialToggleHandler : MonoBehaviour
{
    public Toggle ShowTutorialToggle;

    void Start()
    {
        ShowTutorialToggle.onValueChanged.AddListener(OnToggleValueChanged);
    }

    void OnToggleValueChanged(bool isOn)
    {
        GlobalVariables.ShowTutorial = isOn;
    }
}