// MvxSimpleSilverlightViewPresenter.cs
// (c) Copyright Cirrious Ltd. http://www.cirrious.com
// MvvmCross is licensed using Microsoft Public License (Ms-PL)
// Contributions and inspirations noted in readme.md and license.txt
// 
// Project Lead - Stuart Lodge, @slodge, me@slodge.com

using System;
using System.Windows;
using System.Windows.Controls;
using Cirrious.CrossCore.Exceptions;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.ViewModels;

namespace Cirrious.MvvmCross.Silverlight.Views {
	public class MvxSimpleSilverlightViewPresenter : MvxSilverlightViewPresenter {
		protected ContentControl ContentControlInternal;

		public MvxSimpleSilverlightViewPresenter( ContentControl contentControl ) {
			contentControl.VerticalContentAlignment = VerticalAlignment.Stretch;
			contentControl.HorizontalContentAlignment = HorizontalAlignment.Stretch;

			ContentControlInternal = contentControl;
		}

		public override void Present( FrameworkElement frameworkElement ) {
			var mvxView = frameworkElement as IMvxSilverlightView;
			if ( mvxView == null ) throw new MvxException( "Passed in FrameworkElement is not a IMvxSilverLightView" );

			ContentControlInternal.Content = frameworkElement;
		}

	}
}