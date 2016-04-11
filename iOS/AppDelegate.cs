using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using CoreFoundation;
using PushKit;

namespace PushKitExample.iOS
{
  [Register ("AppDelegate")]
  public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate, IPKPushRegistryDelegate
  {
    string token;

    public override bool FinishedLaunching (UIApplication app, NSDictionary options)
    {
      global::Xamarin.Forms.Forms.Init ();

      LoadApplication (new App ());

      RegisterVoip();

      if (UIDevice.CurrentDevice.CheckSystemVersion (8, 0)) {
        var notificationSettings = UIUserNotificationSettings.GetSettingsForTypes (
          UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound, null
        );

        app.RegisterUserNotificationSettings (notificationSettings);
      } 

      return base.FinishedLaunching (app, options);
    }

    public void DidUpdatePushCredentials (PKPushRegistry registry, PKPushCredentials credentials, string type)
    {
      if(credentials != null && credentials.Token != null) {
        var fullToken = credentials.Token.ToString();
        token = fullToken.Trim ('<').Trim ('>').Replace(" ", string.Empty);
        Console.WriteLine("Token is " + token);
      }
    }

    public void DidReceiveIncomingPush (PKPushRegistry registry, PKPushPayload payload, string type)
    {
      Console.WriteLine("My push is coming!");
      var aps = payload.DictionaryPayload.ObjectForKey(new NSString("aps")) as NSDictionary;
      NSString alertKey = new NSString("alert");

      if(aps.ContainsKey(alertKey)) {
        UILocalNotification notification = new UILocalNotification();
        notification.FireDate = NSDate.Now;
        notification.AlertBody = aps.ObjectForKey(alertKey) as NSString;
        notification.TimeZone = NSTimeZone.DefaultTimeZone;
        notification.SoundName = UILocalNotification.DefaultSoundName;
        notification.ApplicationIconBadgeNumber = 1;
        UIApplication.SharedApplication.ScheduleLocalNotification(notification);
      }
    }

    public override void ReceivedLocalNotification(UIApplication application, UILocalNotification notification)
    {
      // show an alert
      UIAlertController okayAlertController = UIAlertController.Create (notification.AlertAction, notification.AlertBody, UIAlertControllerStyle.Alert);
      okayAlertController.AddAction (UIAlertAction.Create ("OK", UIAlertActionStyle.Default, null));

      // reset our badge
      UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
    }

    void RegisterVoip ()
    {
      var mainQueue = DispatchQueue.MainQueue;
      PKPushRegistry voipRegistry = new PKPushRegistry(mainQueue);
      voipRegistry.Delegate = this;
      voipRegistry.DesiredPushTypes = new NSSet(new string[] {PushKit.PKPushType.Voip});
    }
  }
}

