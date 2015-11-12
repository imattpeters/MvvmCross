// MvxSilverLightView.cs
// (c) Copyright Cirrious Ltd. http://www.cirrious.com
// MvvmCross is licensed using Microsoft Public License (Ms-PL)
// Contributions and inspirations noted in readme.md and license.txt
// 
// Project Lead - Stuart Lodge, @slodge, me@slodge.com

using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Cirrious.MvvmCross.ViewModels;

namespace Cirrious.MvvmCross.SilverLight.Views {
	public class MvxSilverLightView : ContentControl, IMvxSilverLightView {
		private IMvxViewModel _viewModel;


		public event EventHandler ViewModelChanged;

		public virtual IMvxViewModel ViewModel {
			get { return _viewModel; }
			set {
				_viewModel = value;
				DataContext = value;

				if ( ViewModelChanged != null )
					ViewModelChanged.Invoke( this, EventArgs.Empty );
			}
		}

		/// <summary>
		/// This will only be called the very first time the control is shown.
		/// If you use Loaded then it will be called each time the control is loaded on to the screen
		/// </summary>
		public event RoutedEventHandler FirstShown;
		private bool hasBeenShown = false;


		public MvxSilverLightView()
			: base() {

			// HACK: need to do this because the binding stringformat uses EN-US if we don't.
			this.Language = XmlLanguage.GetLanguage( Thread.CurrentThread.CurrentCulture.Name );

			this.VerticalContentAlignment = VerticalAlignment.Stretch;
			this.HorizontalContentAlignment = HorizontalAlignment.Stretch;

			this.Loaded += ( s, e ) => {
				if ( !hasBeenShown ) {
					if ( FirstShown != null ) {
						hasBeenShown = true; // this is here so that we will run it once after its set. and fail to run it if its set after the first time we're shown.
						FirstShown.Invoke( e, e );
					}
				}
			};

		}
	}
}