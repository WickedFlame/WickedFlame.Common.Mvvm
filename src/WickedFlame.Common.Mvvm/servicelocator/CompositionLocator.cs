using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using wickedflame.componentmodel.composition;

namespace wickedflame.common.mvvm.servicelocator
{
    public static class CompositionLocator
    {
        public static T GetService<T>()
        {
            using (var cl = new CompositionLocator<T>())
            {
                return cl.GetService<T>();
            }
        }
    }

    public class CompositionLocator<T> : IServiceLocator, IServiceProvider, IDisposable
    {
        #region IServiceLocator Members

        public T GetService<T>()
        {
            Import();

            if (Imports == null)
                return default(T);

            return (T)(object)Imports.FirstOrDefault();
        }

        public T GetService<T, O>() where O : class, new()
        {
            Import();

            if (Imports == null)
                return default(T);

            return (T)(object)Imports.FirstOrDefault(s => s.GetType() == typeof(O));
        }

        bool IServiceLocator.RegisterService<T>(T service)
        {
            return false;
        }

        #endregion

        #region IServiceProvider Members

        public object GetService(Type serviceType)
        {
            Import();

            if (Imports == null)
                return default(T);

            return (T)(object)Imports.FirstOrDefault(s => s.GetType() == serviceType);
        }

        #endregion

        #region Composition

        public IEnumerable<T> Imports { get; set; }

        public void Import()
        {
            var stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();
            Trace.WriteLine(string.Format("###### Start Composition of Type {0} ####", typeof(T)));


            var composer = new CompositionContainer<T>();
            composer.Catalogs.Add(new ComposeableCatalog(".", "*.dll"));
            composer.Catalogs.Add(new ComposeableCatalog(".", "*.exe"));

            // fill the imports of this object
            try
            {
                composer.Compose();
                Imports = composer.ComposedParts;
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.WriteLine(e);
            }


            stopwatch.Stop();
            Trace.WriteLine(string.Format("###### End Composition of Type {0} | Duration: {1} ms ####", typeof(T), stopwatch.ElapsedMilliseconds));
        }

        #endregion

        #region IDisposeable Members

        public void Dispose()
        {
            Imports = null;
        }

        #endregion
    }
}
