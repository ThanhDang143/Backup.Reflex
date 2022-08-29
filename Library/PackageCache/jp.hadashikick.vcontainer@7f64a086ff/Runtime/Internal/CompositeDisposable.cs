using System;
using System.Collections.Generic;

namespace VContainer.Internal
{
    sealed class CompositeDisposable : IDisposable
    {
        readonly Stack<IDisposable> disposables = new Stack<IDisposable>();

        public void Dispose()
        {
            IDisposable disposable;
            do
            {
                lock (disposables)
                {
                    disposable = disposables.Count > 0
                        ? disposables.Pop()
                        : null;
                }
                disposable?.Dispose();
            } while (disposable != null);
        }

        public void Add(IDisposable disposable)
        {
            lock (disposables)
            {
                disposables.Push(disposable);
            }
        }
    }
}