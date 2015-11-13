using System;
using System.Windows;
using System.Windows.Controls;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Platform;
using Cirrious.MvvmCross.Silverlight.Views;
using Cirrious.MvvmCross.ViewModels;

namespace Cirrious.MvvmCross.Silverlight.Platform {

	/// <summary>
	/// The non-generic has the default presenter and set up
	/// </summary>
	public class MvxSimpleSilverlightApplication : MvxSimpleSilverlightApplication<MvxSimpleSilverlightViewPresenter, MvxSilverlightSetup> {
	}

	/// <summary>
	/// this allows you to override what Presenter or Setup we use.
	/// </summary>
	/// <typeparam name="TPresenter">The type of IMvxSilverlightViewPresenter to use</typeparam>
	/// <typeparam name="TSetup">The implementation of MvxSetup to use</typeparam>
	public class MvxSimpleSilverlightApplication<TPresenter, TSetup> : Application
		where TPresenter : IMvxSilverlightViewPresenter
		where TSetup : MvxSetup {
		private bool _hasRun;

		public MvxSimpleSilverlightApplication() {
			this.Startup += SilverlightApplication_Startup;
		}

		void SilverlightApplication_Startup( object sender, StartupEventArgs e ) {
			if ( _hasRun ) return;
			_hasRun = true;

			var _rootVisualControl = new ContentControl();

			Application.Current.RootVisual = _rootVisualControl;

			var presenter = (TPresenter)Activator.CreateInstance( typeof( TPresenter ), _rootVisualControl );

			var appSetup = (TSetup)Activator.CreateInstance( typeof( TSetup ), Deployment.Current.Dispatcher, presenter );

			appSetup.Initialize();

			Mvx.Resolve<IMvxAppStart>().Start();
		}


		/// <summary>
		/// To used instead of RootVisual 
		/// </summary>
		public static FrameworkElement RootElement {
			get {
				var RootVisual = Application.Current.RootVisual as ContentControl;
				if ( RootVisual == null ) return null;

				var RootControl = RootVisual.Content as FrameworkElement;
				if ( RootControl == null ) return null;

				return RootControl;
			}
		}

	}
}