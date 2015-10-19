using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task1
{
    public static class EventArgExtensions
    {
        public static void Raise<TEventArgs>(this TEventArgs e,
        Object sender, ref EventHandler<TEventArgs> eventDelegate)
        {
            // Копирование ссылки на поле делегата во временное поле
            // для безопасности в отношении потоков
            EventHandler<TEventArgs> temp = Volatile.Read(ref eventDelegate);
            // Если зарегистрированный метод заинтересован в событии, уведомите его
            if (temp != null) temp(sender, e);
        }
    }
    public abstract class Idispose : IDisposable
    {
        bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //
            disposed = true;
        }
    }
}
