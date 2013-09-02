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
			_contentControl = contentControl;
		}

		/// <summary>
		/// this allows for an easy constructor in silverlight
		/// new MvxSimpleSilverLightViewPresenter( Application.Current.RootVisual )
		/// </summary>
		/// <param name="rootVisual">suggested: Application.Current.RootVisual</param>
		public MvxSimpleSilverLightViewPresenter( UIElement rootVisual ) {
			_contentControl = new ContentControl();
			rootVisual = _contentControl;
		}

		
		public override void Present( FrameworkElement frameworkElement ) {
			_contentControl.Content = frameworkElement;
		}


	}
}