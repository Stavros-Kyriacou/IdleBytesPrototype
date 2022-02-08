using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectionLine : MonoBehaviour
{
    public List<RectTransform> ConnectionLines;
    public ConnectionType ConnectionType;
    public int ConnectionTypeIndex
    {
        get
        {
            return (int)ConnectionType;
        }
    }
    private void Start()
    {
        foreach (var line in ConnectionLines)
        {
            foreach (RectTransform child in line)
            {
                var img = child.GetComponent<Image>();
                img.color = Color.grey;
            }
            line.gameObject.SetActive(false);
        }

        int index = (int)ConnectionType;

        ConnectionLines[index].gameObject.SetActive(true);
    }
}

public enum ConnectionType
{
    ThreeToOne,
    TwoToOne,
    OneToTwo,
    OneToOne,
    OneToThree,
    ThreeToTwo
}