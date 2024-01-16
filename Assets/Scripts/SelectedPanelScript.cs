using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedPanelScript : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (ScenarioController.Instance.IsGameOver)
        {
            gameObject.SetActive(false);
        }
    }
}
