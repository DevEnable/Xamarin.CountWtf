using System;
using Android.App;
using Android.Runtime;
using CounterWtf.Common;
using TinyIoC;

namespace CounterWtf.Droid
{
    /// <summary>
    /// Because this application is all things WTF.
    /// </summary>
    /// <remarks>Using <see cref="Application"/> to ensure that the IoC state is not garbage collected.</remarks>
    [Application]
    public class WtfApplication : Application
    {
        /// <summary>
        /// Initializes an instance of the <see cref="WtfApplication"/> class
        /// </summary>
        /// <remarks>Even though Application has a default constructor, need to use this overload or the Android runtime will be unable to instantiate this class.</remarks>
        public WtfApplication(IntPtr handle, JniHandleOwnership transfer)
        : base(handle, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

            // Register IoC dependencies.
            TinyIoCContainer.Current.Register<IWtfClient, DroidClient>(); // Singleton
        }
    }
}