// MvxSilverLightSimpleViewPresenter.cs
// (c) Copyright Cirrious Ltd. http://www.cirrious.com
// MvvmCross is licensed using Microsoft Public License (Ms-PL)
// Contributions and inspirations noted in readme.md and license.txt
// 
// Project Lead - Stuart Lodge, @slodge, me@slodge.com

using System.Windows;

namespace Cirrious.MvvmCross.SilverLight.Views {
	public class MvxSilverLightSimpleViewPresenter
		: MvxSilverLightViewPresenter {
		private readonly Window _mainWindow;

		public MvxSilverLightSimpleViewPresenter( Window mainWindow ) {
			_mainWindow = mainWindow;
		}

		public override void Present( FrameworkElement frameworkElement ) {
			_mainWindow.Content = frameworkElement;
		}
	}
}