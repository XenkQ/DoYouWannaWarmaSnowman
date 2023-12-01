using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyperLinksButtons : MonoBehaviour
{
    public void OpenYoutubeLink()
    {
        Application.OpenURL("https://www.youtube.com/channel/UCUFNXkMq76o_OFMZgH45Kog");
    }

    public void OpenTwitterLink()
    {
        Application.OpenURL("https://twitter.com/StudioVisla");
    }
}
