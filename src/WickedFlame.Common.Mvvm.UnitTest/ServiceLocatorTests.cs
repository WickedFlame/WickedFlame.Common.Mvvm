using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WickedFlame.Common.Mvvm.test.mockdata;
//using WickedFlame.Common.Mvvm.servicelocator;

namespace WickedFlame.Common.Mvvm.UnitTest
{
    [TestClass]
    public class ServiceLocatorTests
    {
        //[TestMethod]
        //public void CompositionLocator_GetServiceTest()
        //{
        //    var serviceLocator = new ServiceLocator();
        //    var service = serviceLocator.GetService<IServiceLocatorMock>();

        //    Assert.IsNotNull(service);
        //}

        //[TestMethod]
        //public void CompositionLocator_GetExplicitServiceTest()
        //{
        //    var serviceLocator = new ServiceLocator();
        //    var service = serviceLocator.GetService<IServiceLocatorMock, ServiceLocatorMock>();

        //    Assert.IsNotNull(service);
        //    Assert.IsTrue(service is ServiceLocatorMock);
        //    Assert.AreEqual(service.TestMethod(), "ServiceLocatorMock");

        //    service = serviceLocator.GetService<IServiceLocatorMock, ServiceLocatorMockTwo>();
            
        //    Assert.IsNotNull(service);
        //    Assert.IsTrue(service is ServiceLocatorMockTwo);
        //    Assert.AreEqual(service.TestMethod(), "ServiceLocatorMockTwo");
        //}

        //[TestMethod]
        //public void StaticServiceLocator_RegisterServiceTest()
        //{
        //    IServiceLocatorMock original = new ServiceLocatorMock();
        //    ConstantLocator.RegisterService<IServiceLocatorMock>(original);

        //    var serviceLocator = new ServiceLocator();
        //    var service = serviceLocator.GetService<IServiceLocatorMock>();

        //    Assert.IsNotNull(service);
        //    Assert.AreSame(service, original);
        //}
    }
}
