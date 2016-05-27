using UnityEngine;
using System.Collections;

public class AndroidRateUsPopUp : MonoBehaviour
{
    #region DELEGATE

    public delegate void OnRateUSPopupComplete(MessageState state);

    public static event OnRateUSPopupComplete onRateUSPopupComplete;

    #endregion

    #region DELEGATE_CALLS

    private void RaiseOnOnRateUSPopupComplete(MessageState state)
    {
        if (onRateUSPopupComplete != null)
            onRateUSPopupComplete(state);
    }

    #endregion

    #region PUBLIC_VARIABLES

    public string title;
    public string message;
    public string rate;
    public string remind;
    public string declined;
    public string appLink;

    #endregion

    #region PUBLIC_FUNCTIONS

    public static AndroidRateUsPopUp Create()
    {
        return Create("Like the Game?", "Rate US");
    }

    public static AndroidRateUsPopUp Create(string title, string message)
    {
        return Create(title, message, "Rate Now", "Ask me later", "No, thanks");
    }

    public static AndroidRateUsPopUp Create(string title, string message, string rate, string remind, string declined)
    {
        AndroidRateUsPopUp popup = new GameObject("AndroidRateUsPopUp").AddComponent<AndroidRateUsPopUp>();
        popup.title = title;
        popup.message = message;
        popup.rate = rate;
        popup.remind = remind;
        popup.declined = declined;
		
        popup.init();
        return popup;
    }

    public void init()
    {
        AndroidNative.showRateUsPopUP(title, message, rate, remind, declined);
    }

    #endregion

    #region ANDROID_EVENT_LISTENER

    public void OnRatePopUpCallBack(string buttonIndex)
    {
        int index = System.Convert.ToInt16(buttonIndex);
        switch (index)
        {
            case 0: 
                AndroidNative.RedirectToAppStoreRatingPage(appLink);
                RaiseOnOnRateUSPopupComplete(MessageState.RATED);
                break;
            case 1:
                RaiseOnOnRateUSPopupComplete(MessageState.REMIND);
                break;
            case 2:
                RaiseOnOnRateUSPopupComplete(MessageState.DECLINED);
                break;
        }
        Destroy(gameObject);
    }

    #endregion
}
