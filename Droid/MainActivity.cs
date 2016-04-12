using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Gms.Common;

namespace PushKitExample.Droid
{
  [Activity (Label = "PushKitExample.Droid", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
  public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
  {
    protected override void OnCreate (Bundle bundle)
    {
      base.OnCreate (bundle);

      global::Xamarin.Forms.Forms.Init (this, bundle);

      var intent = new Intent (Android.App.Application.Context, typeof(RegistrationIntentService));
      Android.App.Application.Context.StartService (intent);

      LoadApplication (new App ());
    }
  }
}

