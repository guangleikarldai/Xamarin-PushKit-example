using System;
using Android.App;
using Android.Content;
using Android.Util;
using Android.Gms.Gcm.Iid;
using Android.Gms.Gcm;

namespace PushKitExample.Droid
{
  [Service (Exported = false)]
  class RegistrationIntentService : IntentService
  {
    private const string GOOGLE_PROJECT_ID = "541804503884";
    static object locker = new object ();

    public RegistrationIntentService () : base ("RegistrationIntentService")
    {
    }

    protected override void OnHandleIntent (Intent intent)
    {
      try {
        lock (locker) {
          var instanceID = InstanceID.GetInstance (this);
          var token = instanceID.GetToken (
            GOOGLE_PROJECT_ID, GoogleCloudMessaging.InstanceIdScope, null);
          SendRegistrationToAppServerAndPersist (token);
        }
      } catch (Exception e) {
        Log.Info ("RegistrationIntentService", "Failed to get a registration token ");
        return;
      }
    }

    async void SendRegistrationToAppServerAndPersist (string notificationToken)
    {
      Console.WriteLine("GCM Token is " + notificationToken);
    }
  }
}
