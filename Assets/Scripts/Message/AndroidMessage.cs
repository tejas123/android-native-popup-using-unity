using UnityEngine;
using System.Collections;

public class AndroidMessage : MonoBehaviour
{
    #region DELEGATE

    public delegate void OnMessagePopupComplete(MessageState state);

    public static event OnMessagePopupComplete onMessagePopupComplete;

    #endregion

    #region DELEGATE_CALLS

    private void RaiseOnMessagePopupComplete(MessageState state)
    {
        if (onMessagePopupComplete != null)
            onMessagePopupComplete(state);
    }

    #endregion

    #region PUBLIC_VARIABLES

    public string title;
    public string message;
    public string ok;

    #endregion

    #region PUBLIC_FUNCTIONS

    public static AndroidMessage Create(string title, string message)
    {
        return Create(title, message, "Ok");
    }

    public static AndroidMessage Create(string title, string message, string ok)
    {
        AndroidMessage dialog;
        dialog = new GameObject("AndroidMessagePopup").AddComponent<AndroidMessage>();
        dialog.title = title;
        dialog.message = message;
        dialog.ok = ok;
		
        dialog.init();
        return dialog;
    }

    public void init()
    {
        AndroidNative.showMessage(title, message, ok);
    }

    #endregion

    #region ANDROID_EVENT_LISTENER

    public void OnMessagePopUpCallBack(string buttonIndex)
    {
        RaiseOnMessagePopupComplete(MessageState.OK);
        Destroy(gameObject);
    }

    #endregion
}
