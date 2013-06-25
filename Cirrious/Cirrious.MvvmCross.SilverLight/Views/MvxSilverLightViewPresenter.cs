// MvxSilverLightViewPresenter.cs
// (c) Copyright Cirrious Ltd. http://www.cirrious.com
// MvvmCross is licensed using Microsoft Public License (Ms-PL)
// Contributions and inspirations noted in readme.md and license.txt
// 
// Project Lead - Stuart Lodge, @slodge, me@slodge.com

using System;
using System.Windows;
using Cirrious.CrossCore.Exceptions;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.ViewModels;

namespace Cirrious.MvvmCross.SilverLight.Views
{
    public abstract class MvxSilverLightViewPresenter
        : IMvxSilverLightViewPresenter
    {
        public void Show(MvxViewModelRequest request)
        {
            try
            {
                var loader = Mvx.Resolve<IMvxSimpleSilverLightViewLoader>();
                var view = loader.CreateView(request);
                Present(view);
            }
            catch (Exception exception)
            {
                MvxTrace.Error("Error seen during navigation request to {0} - error {1}", request.ViewModelType.Name,
                               exception.ToLongString());
            }
        }

        public abstract void Present(FrameworkElement frameworkElement);

        public virtual void ChangePresentation(MvxPresentationHint hint)
        {
            MvxTrace.Warning("Hint ignored {0}", hint.GetType().Name);
        }
    }
}