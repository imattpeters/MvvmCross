// MvxSilverLightView.cs
// (c) Copyright Cirrious Ltd. http://www.cirrious.com
// MvvmCross is licensed using Microsoft Public License (Ms-PL)
// Contributions and inspirations noted in readme.md and license.txt
// 
// Project Lead - Stuart Lodge, @slodge, me@slodge.com

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

		public MvxSilverLightView() : base() { }
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