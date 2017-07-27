using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using StructureMap;

namespace StructureMapFuncDemo
{
    public class RegistryContainer
    {
        private Container _container;

        public RegistryContainer()
        {
            _container = new Container();
        }

        public void Configure()
        {
            _container.Configure(_ => _.Scan(x =>
            {
                x.TheCallingAssembly();
                x.WithDefaultConventions();
            }));
        }

        public T GetInstance<T>()
        {
            return _container.GetInstance<T>();
        }
    }
}
