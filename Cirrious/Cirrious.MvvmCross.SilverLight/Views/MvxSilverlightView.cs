// MvxSilverlightView.cs
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
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.ViewModels;

namespace Cirrious.MvvmCross.Silverlight.Views {
	public class MvxSilverlightView : ContentControl, IMvxSilverlightView {

		public event EventHandler ViewModelChanged;
		public virtual IMvxViewModel ViewModel {
			get { return DataContext as IMvxViewModel; }
			set { DataContext = value; }
		}

		public new object DataContext {
			get { return base.DataContext; }
			set {
				if ( base.DataContext is IDisposable )
					( base.DataContext as IDisposable ).Dispose();

				if ( value is IMvxViewModel || value == null ) {
					base.DataContext = value;

					if ( ViewModelChanged != null )
						ViewModelChanged.Invoke( this, EventArgs.Empty );

					return;
				}
				MvxTrace.Trace( "MvxSilverlightView: " + this.GetType().ToString() + ".DataContext is not IMvxViewModel so ignoring set" );
			}
		}

		/// <summary>
		/// This will only be called the very first time the control is shown.
		/// If you use Loaded then it will be called each time the control is loaded onto the screen
		/// </summary>
		public event RoutedEventHandler FirstShown;
		private bool _hasRunFirstShown = false;


		public MvxSilverlightView() : base() {

			// HACK: need to do this because the binding string.format uses EN-US if we don't.
			this.Language = XmlLanguage.GetLanguage( Thread.CurrentThread.CurrentCulture.Name );

			this.VerticalContentAlignment = VerticalAlignment.Stretch;
			this.HorizontalContentAlignment = HorizontalAlignment.Stretch;

			this.Loaded += ( s, e ) => {
				if ( !_hasRunFirstShown ) {
					_hasRunFirstShown = true; // this is here so that we will run it once after its set. and fail to run it if its set after the first time we're shown.
					if ( FirstShown != null ) {
						FirstShown.Invoke( e, e );
					}
				}
			};

		}
	}
}