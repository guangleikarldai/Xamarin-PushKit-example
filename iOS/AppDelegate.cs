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

      return base.FinishedLaunching (app, options);
    }

    public void DidUpdatePushCredentials (PKPushRegistry registry, PKPushCredentials credentials, string type)
    {
      if(credentials != null && credentials.Token != null) {
        token = credentials.Token.ToString();
      }
    }

    public void DidReceiveIncomingPush (PKPushRegistry registry, PKPushPayload payload, string type)
    {
      var ppp = payload;
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

