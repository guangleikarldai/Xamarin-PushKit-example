using System;
using Android.App;
using Android.Support.V4.App;
using Android.Content;
using System.Threading.Tasks;
using System.Linq;
using Android.Util;

namespace PushKitExample.Droid
{
  [Service (Exported = false)]
  public class MessageAudioDownloadIntentService : IntentService
  {
    public MessageAudioDownloadIntentService () : base ("MessageAudioDownloadIntentService")
    {
    }

    protected override async void OnHandleIntent (Android.Content.Intent intent)
    {
      await DownloadMessageContent ();
    }

    Task DownloadMessageContent ()
    {
      var intent = new Intent (this, typeof(MainActivity));
      intent.AddFlags (ActivityFlags.ClearTop);
      var pendingIntent = PendingIntent.GetActivity (this, 0, intent, PendingIntentFlags.OneShot);

      NotificationCompat.Builder notificationBuilder = new NotificationCompat.Builder (this)
        .SetSmallIcon (Resource.Drawable.icon)
        .SetContentTitle ("NVM New Message Downloaded")
        .SetContentText (String.Format ("Your have {0} new messages Downloaded", 1))
        .SetAutoCancel (true)
        .SetContentIntent (pendingIntent);

      Notification notification = notificationBuilder.Build ();
      NotificationManager notificationManager = GetSystemService (Context.NotificationService) as NotificationManager;
      notificationManager.Notify (0, notification);
    }
  }
}
