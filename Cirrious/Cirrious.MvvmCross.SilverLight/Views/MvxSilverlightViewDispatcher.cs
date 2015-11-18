// MvxSilverlightViewDispatcher.cs
// (c) Copyright Cirrious Ltd. http://www.cirrious.com
// MvvmCross is licensed using Microsoft Public License (Ms-PL)
// Contributions and inspirations noted in readme.md and license.txt
// 
// Project Lead - Stuart Lodge, @slodge, me@slodge.com

using System;
using System.Windows.Threading;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.Views;

namespace Cirrious.MvvmCross.Silverlight.Views {
	public class MvxSilverlightViewDispatcher : MvxSilverlightUIThreadDispatcher , IMvxViewDispatcher {
		private readonly IMvxSilverlightViewPresenter _presenter;

		public MvxSilverlightViewDispatcher( Dispatcher dispatcher, IMvxSilverlightViewPresenter presenter )
			: base( dispatcher ) {
			_presenter = presenter;
		}

		public bool ShowViewModel( MvxViewModelRequest request ) {
			return RequestMainThreadAction( () => _presenter.Show( request ) );
		}

		public bool ChangePresentation( MvxPresentationHint hint ) {
			return RequestMainThreadAction( () => _presenter.ChangePresentation( hint ) );
		}
	}
}