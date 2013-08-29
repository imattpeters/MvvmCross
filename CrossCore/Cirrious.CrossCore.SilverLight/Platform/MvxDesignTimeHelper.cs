// MvxDesignTimeHelper.cs
// (c) Copyright Cirrious Ltd. http://www.cirrious.com
// MvvmCross is licensed using Microsoft Public License (Ms-PL)
// Contributions and inspirations noted in readme.md and license.txt
// 
// Project Lead - Stuart Lodge, @slodge, me@slodge.com

using System.ComponentModel;
using Cirrious.CrossCore.Core;
using Cirrious.CrossCore.IoC;
using System;
using System.Windows;

namespace Cirrious.CrossCore.SilverLight.Platform {
	public abstract class MvxDesignTimeHelper {
		protected MvxDesignTimeHelper() {
			if ( !IsInDesignTime )
				return;

			if ( MvxSingleton<IMvxIoCProvider>.Instance == null ) {
				var iocProvider = MvxSimpleIoCContainer.Initialise();
				Mvx.RegisterSingleton( iocProvider );
			}
		}

		private static bool? _isInDesignTime;
		protected static bool IsInDesignTime {
			get {
				if ( _isInDesignTime.HasValue )
					return _isInDesignTime.Value;

				// there are a few reasons why none of these are 100% all the time
				// i think that using all of them makes sure that you always know if your in design time.

				if ( Application.Current.RootVisual != null ) {
					if ( DesignerProperties.GetIsInDesignMode( Application.Current.RootVisual ) ) {
						_isInDesignTime = true;
						return true;
					}
				}


				if ( (bool)DesignerProperties.IsInDesignModeProperty.GetMetadata( typeof( System.Windows.DependencyObject ) ).DefaultValue ) {
					_isInDesignTime = true;
					return true;
				}

				try {
					var host = Application.Current.Host.Source;
					_isInDesignTime = false;
					return false;
				} catch {
					_isInDesignTime = true;
					return true;
				}

			}
		}
	}
}