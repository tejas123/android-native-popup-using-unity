using UnityEngine;
using System.Collections;

public class AndroidNative
{
    public static void CallStatic(string methodName, params object[] args)
    {
        #if UNITY_ANDROID && !UNITY_EDITOR
        try
        {
            string CLASS_NAME = "com.tag.nativepopup.PopupManager";
            AndroidJavaObject bridge = new AndroidJavaObject(CLASS_NAME);

            AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"); 
            AndroidJavaObject act = jc.GetStatic<AndroidJavaObject>("currentActivity"); 
            
            act.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                bridge.CallStatic(methodName, args);
            }));

        } catch (System.Exception ex)
        {
            Debug.LogWarning(ex.Message);
        }
        #endif
    }

	
    public static void showRateUsPopUP(string title, string message, string rate, string remind, string declined)
    {
        CallStatic("ShowRatePopup", title, message, rate, remind, declined);
    }

    public static void showDialog(string title, string message, string yes, string no)
    {
        CallStatic("ShowDialogPopup", title, message, yes, no);
    }

    public static void showMessage(string title, string message, string ok)
    {
        CallStatic("ShowMessagePopup", title, message, ok);
    }

    public static void RedirectToAppStoreRatingPage(string appLink)
    {
        CallStatic("OpenAppRatingPage", appLink);
    }

    public static void RedirectToWebPage(string urlString)
    {
        CallStatic("OpenWebPage", urlString);
    }
}