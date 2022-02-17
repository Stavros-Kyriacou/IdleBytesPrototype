using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    public Camera MainCam;
    public UnityEvent OnClick;
    private Vector3 CameraPosition;
    private void OnMouseDown()
    {
        this.CameraPosition = this.MainCam.transform.position;
    }
    private void OnMouseUp()
    {
        if (MenuController.Instance.IsMenuOpen) return;

        if (this.CameraPosition != MainCam.transform.position) return;

        this.OnClick.Invoke();
    }
}