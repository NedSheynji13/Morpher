using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public Color buttonidle;
    public Color buttonhovered;

    private Color wantedColour;
    private Image Icon;

    private void Start()
    {
        Icon = GetComponent<Image>();
        Icon.color = wantedColour = buttonidle;
    }
    private void Update()
    {
        Icon.color = Color.Lerp(Icon.color, wantedColour, 10 * Time.deltaTime);
    }
    public void HoverColour()
    {
        wantedColour = buttonhovered;
    }
    public void ExitColour()
    {
        wantedColour = buttonidle;
    }
}
