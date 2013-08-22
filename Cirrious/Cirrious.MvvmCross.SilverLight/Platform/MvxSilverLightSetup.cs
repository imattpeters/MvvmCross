// MvxSilverLightetup.cs
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
using Cirrious.MvvmCross.SilverLight.Views;
using Cirrious.MvvmCross.Views;

namespace Cirrious.MvvmCross.SilverLight.Platform {
	public abstract class MvxSilverLightSetup : MvxSetup {
		private readonly Dispatcher _uiThreadDispatcher;
		private readonly IMvxSilverLightViewPresenter _presenter;

		protected MvxSilverLightSetup( Dispatcher uiThreadDispatcher, IMvxSilverLightViewPresenter presenter ) {
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
			var toReturn = new MvxSilverLightViewsContainer();
			Mvx.RegisterSingleton<IMvxSimpleSilverLightViewLoader>( toReturn );
			return toReturn;
		}

		protected override IMvxViewDispatcher CreateViewDispatcher() {
			return new MvxSilverLightViewDispatcher( _uiThreadDispatcher, _presenter );
		}

		protected override IMvxPluginManager CreatePluginManager() {
			return new MvxFilePluginManager( ".SilverLight", string.Empty );
		}
	}
}