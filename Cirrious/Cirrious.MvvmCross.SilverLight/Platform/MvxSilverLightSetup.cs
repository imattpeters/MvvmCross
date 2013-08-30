// MvxSilverLightetup.cs
// (c) Copyright Cirrious Ltd. http://www.cirrious.com
// MvvmCross is licensed using Microsoft Public License (Ms-PL)
// Contributions and inspirations noted in readme.md and license.txt
// 
// Project Lead - Stuart Lodge, @slodge, me@slodge.com

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Threading;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Converters;
using Cirrious.CrossCore.Platform;
using Cirrious.CrossCore.Plugins;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Binding.Bindings.Target.Construction;
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

		//protected override IMvxPluginManager CreatePluginManager() {
			//return new MvxFilePluginManager( ".SilverLight", string.Empty );
		//}
		protected override IMvxPluginManager CreatePluginManager() {
			var toReturn = new MvxLoaderPluginManager();
			var registry = new MvxLoaderPluginRegistry( ".SilverLight", toReturn.Finders );
			AddPluginsLoaders( registry );
			return toReturn;
		}

		protected virtual void AddPluginsLoaders( MvxLoaderPluginRegistry loaders ) {
			// none added by default
		}

		


		protected override void InitializePlatformServices() {
			RegisterPlatformProperties();
			// for now we continue to register the old style platform properties
			RegisterOldStylePlatformProperties();
			RegisterPresenter();
			RegisterLifetime();
		}

		protected virtual void RegisterPlatformProperties() {
			Mvx.RegisterSingleton<IMvxTouchSystem>( CreateTouchSystemProperties() );
		}

		protected virtual MvxTouchSystem CreateTouchSystemProperties() {
			return new MvxTouchSystem();
		}


		protected override void InitializeLastChance() {
			InitialiseBindingBuilder();
			base.InitializeLastChance();
		}

		protected virtual void InitialiseBindingBuilder() {
			RegisterBindingBuilderCallbacks();
			var bindingBuilder = CreateBindingBuilder();
			bindingBuilder.DoRegistration();
		}

		protected virtual void RegisterBindingBuilderCallbacks() {
			Mvx.CallbackWhenRegistered<IMvxValueConverterRegistry>( FillValueConverters );
			Mvx.CallbackWhenRegistered<IMvxTargetBindingFactoryRegistry>( FillTargetFactories );
			Mvx.CallbackWhenRegistered<IMvxBindingNameRegistry>( FillBindingNames );
		}

		protected virtual MvxBindingBuilder CreateBindingBuilder() {
			var bindingBuilder = new MvxTouchBindingBuilder();
			return bindingBuilder;
		}

		protected virtual void FillBindingNames( IMvxBindingNameRegistry obj ) {
			// this base class does nothing
		}

		protected virtual void FillValueConverters( IMvxValueConverterRegistry registry ) {
			registry.Fill( ValueConverterAssemblies );
			registry.Fill( ValueConverterHolders );
		}

		protected virtual List<Type> ValueConverterHolders {
			get { return new List<Type>(); }
		}

		protected virtual List<Assembly> ValueConverterAssemblies {
			get {
				var toReturn = new List<Assembly>();
				toReturn.AddRange( GetViewModelAssemblies() );
				toReturn.AddRange( GetViewAssemblies() );
				return toReturn;
			}
		}

		protected virtual void FillTargetFactories( IMvxTargetBindingFactoryRegistry registry ) {
			// this base class does nothing
		}
	}
}