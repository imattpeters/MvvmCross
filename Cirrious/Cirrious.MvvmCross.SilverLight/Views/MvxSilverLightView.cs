// MvxSilverLightView.cs
// (c) Copyright Cirrious Ltd. http://www.cirrious.com
// MvvmCross is licensed using Microsoft Public License (Ms-PL)
// Contributions and inspirations noted in readme.md and license.txt
// 
// Project Lead - Stuart Lodge, @slodge, me@slodge.com

using System.Windows;
using System.Windows.Controls;
using Cirrious.MvvmCross.ViewModels;

namespace Cirrious.MvvmCross.SilverLight.Views {
	public class MvxSilverLightView : UserControl, IMvxSilverLightView {
		private IMvxViewModel _viewModel;

		public virtual IMvxViewModel ViewModel {
			get { return _viewModel; }
			set {
				_viewModel = value;
				DataContext = value;
			}
		}

		/// <summary>
		/// This will only be called the very first time the control is shown.
		/// If you use Loaded then it will be called each time the control is loaded on to the screen
		/// </summary>
		public event RoutedEventHandler Shown;
		private bool hasBeenShown = false;


		public MvxSilverLightView()
			: base() {

			this.Loaded += ( s, e ) => {
				if ( !hasBeenShown ) {
					if ( Shown != null ) {
						hasBeenShown = true; // this is here so that we will run it once after its set. and fail to run it if its set after the first time we're shown.
						Shown.Invoke( e, e );
					}
				}
			};

		}
	}

	public class MvxSilverLightView<T> : MvxSilverLightView
		where T : class, IMvxViewModel {

		public MvxSilverLightView() : base() { }

		public new T ViewModel {
			get { return base.ViewModel as T; }
			set { base.ViewModel = value; }
		}
	}

}