// MvxSilverlightViewsContainer.cs
// (c) Copyright Cirrious Ltd. http://www.cirrious.com
// MvvmCross is licensed using Microsoft Public License (Ms-PL)
// Contributions and inspirations noted in readme.md and license.txt
// 
// Project Lead - Stuart Lodge, @slodge, me@slodge.com

using System;
using System.Windows;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Exceptions;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.Views;

namespace Cirrious.MvvmCross.Silverlight.Views {
	public class MvxSilverlightViewsContainer : MvxViewsContainer , IMvxSimpleSilverlightViewLoader {

		public FrameworkElement CreateView( MvxViewModelRequest request ) {
			var viewType = GetViewType( request.ViewModelType );
			if ( viewType == null )
				throw new MvxException( "View Type not found for " + request.ViewModelType );

			var viewObject = Activator.CreateInstance( viewType );
			if ( viewObject == null )
				throw new MvxException( "View not loaded for " + viewType );

			var silverlightView = viewObject as IMvxSilverlightView;
			if ( silverlightView == null )
				throw new MvxException( "Loaded View does not have IMvxSilverLightView interface " + viewType );

			var viewControl = viewObject as FrameworkElement;
			if ( viewControl == null )
				throw new MvxException( "Loaded View is not a FrameworkElement " + viewType );

			var viewModelLoader = Mvx.Resolve<IMvxViewModelLoader>();
			silverlightView.ViewModel = viewModelLoader.LoadViewModel( request, null );

			return viewControl;
		}

	}
}