// MvxSilverlightSetup.cs
// (c) Copyright Cirrious Ltd. http://www.cirrious.com
// MvvmCross is licensed using Microsoft Public License (Ms-PL)
// Contributions and inspirations noted in readme.md and license.txt
// 
// Project Lead - Stuart Lodge, @slodge, me@slodge.com

using System.Collections.Generic;
using System.Windows.Threading;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Platform;
using Cirrious.CrossCore.Plugins;
using Cirrious.MvvmCross.Platform;
using Cirrious.MvvmCross.Silverlight.Views;
using Cirrious.MvvmCross.Views;

namespace Cirrious.MvvmCross.Silverlight.Platform {
	public abstract class MvxSilverlightSetup : MvxSetup {
		private readonly Dispatcher _uiThreadDispatcher;
		private readonly IMvxSilverlightViewPresenter _presenter;

		protected MvxSilverlightSetup( Dispatcher uiThreadDispatcher, IMvxSilverlightViewPresenter presenter ) {
			_uiThreadDispatcher = uiThreadDispatcher;
			_presenter = presenter;
		}

		protected override IMvxTrace CreateDebugTrace() {
			return new MvxDebugTrace();
		}

		protected override MvxViewsContainer CreateViewsContainer() {
			return CreateAndRegisterSimpleSilverLightViewContainer();
		}

		private MvxViewsContainer CreateAndRegisterSimpleSilverLightViewContainer() {
			var toReturn = new MvxSilverlightViewsContainer();
			Mvx.RegisterSingleton<IMvxSimpleSilverlightViewLoader>( toReturn );
			return toReturn;
		}

		protected override IMvxViewDispatcher CreateViewDispatcher() {
			return new MvxSilverlightViewDispatcher( _uiThreadDispatcher, _presenter );
		}

		protected override IMvxPluginManager CreatePluginManager() {
			return new MvxFilePluginManager( ".SilverLight", string.Empty );
		}
	}
}