// MvxSilverLightUIThreadDispatcher.cs
// (c) Copyright Cirrious Ltd. http://www.cirrious.com
// MvvmCross is licensed using Microsoft Public License (Ms-PL)
// Contributions and inspirations noted in readme.md and license.txt
// 
// Project Lead - Stuart Lodge, @slodge, me@slodge.com

using System;
using System.Windows.Threading;
using Cirrious.CrossCore.Core;

namespace Cirrious.MvvmCross.SilverLight.Views
{
    public class MvxSilverLightUIThreadDispatcher
        : MvxMainThreadDispatcher
    {
        private readonly Dispatcher _dispatcher;

        public MvxSilverLightUIThreadDispatcher(Dispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public bool RequestMainThreadAction(Action action)
        {
            if (_dispatcher.CheckAccess())
            {
                action();
            }
            else
            {
#if SILVERLIGHT
				_dispatcher.BeginInvoke( () => ExceptionMaskedAction( action ) );
#else
                _dispatcher.Invoke(() => ExceptionMaskedAction(action));
#endif
            }

            // TODO - why return bool at all?
            return true;
        }
    }
}