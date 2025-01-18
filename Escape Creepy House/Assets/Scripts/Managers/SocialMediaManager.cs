using UnityEngine;

public class SocialMediaManager : MonoBehaviour
{
    public void ConnectToInstagram()
    {
        Application.OpenURL("https://www.instagram.com/legendkgamesstudio/");
    }

    public void ConnectToTwtter()
    {
        Application.OpenURL("https://x.com/Stelios_Krom");
    }

    public void ConnectToYoutube()
    {
        Application.OpenURL("https://www.youtube.com/@SteliosKrom");
    }
}
