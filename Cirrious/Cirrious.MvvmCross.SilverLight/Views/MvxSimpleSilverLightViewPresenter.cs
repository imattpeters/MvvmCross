// MvxSimpleSilverLightViewPresenter.cs
// (c) Copyright Cirrious Ltd. http://www.cirrious.com
// MvvmCross is licensed using Microsoft Public License (Ms-PL)
// Contributions and inspirations noted in readme.md and license.txt
// 
// Project Lead - Stuart Lodge, @slodge, me@slodge.com

using System.Windows;
using System.Windows.Controls;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.ViewModels;

namespace Cirrious.MvvmCross.SilverLight.Views {
	public class MvxSimpleSilverLightViewPresenter : MvxSilverLightViewPresenter {
		private readonly ContentControl _contentControl;

		public MvxSimpleSilverLightViewPresenter( ContentControl contentControl ) {
			contentControl.VerticalContentAlignment = VerticalAlignment.Stretch;
			contentControl.HorizontalContentAlignment = HorizontalAlignment.Stretch;

			_contentControl = contentControl;
		}



		public override void Present( FrameworkElement frameworkElement ) {
			//System.Windows.Deployment.Current.Dispatcher.BeginInvoke( () => {

			//frameworkElement.VerticalAlignment = VerticalAlignment.Stretch;
			//frameworkElement.HorizontalAlignment = HorizontalAlignment.Stretch;

			_contentControl.Content = frameworkElement;

			//} );

		}


	}
}