using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestClick : MonoBehaviour
{
    public TextMeshProUGUI debugText;
    private void OnMouseDown() {
        debugText.text = gameObject.name + " has been clicked";
    }
}