using System;
using Android.Gms.Gcm;
using Android.App;
using Android.Content;

namespace PushKitExample.Droid
{
  [Service (Exported = false), IntentFilter (new [] { "com.google.android.c2dm.intent.RECEIVE" })]
  public class NotificationGcmListenerService : GcmListenerService
  {
    public override void OnMessageReceived (string from, Android.OS.Bundle data)
    {
      var intent = new Intent (this, typeof(MessageAudioDownloadIntentService));
      StartService (intent);
    }
  }
}
