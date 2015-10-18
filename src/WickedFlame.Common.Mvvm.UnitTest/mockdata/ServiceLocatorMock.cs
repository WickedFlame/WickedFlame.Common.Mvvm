using System.ComponentModel.Composition;

namespace WickedFlame.Common.Mvvm.test.mockdata
{
    public interface IServiceLocatorMock
    {
        string TestMethod();
    }

    [Export(typeof(IServiceLocatorMock))]
    public class ServiceLocatorMock : IServiceLocatorMock
    {
        public string TestMethod()
        {
            return "ServiceLocatorMock";
        }
    }

    [Export(typeof(IServiceLocatorMock))]
    public class ServiceLocatorMockTwo : IServiceLocatorMock
    {
        public string TestMethod()
        {
            return "ServiceLocatorMockTwo";
        }
    }

    public interface IDifferentServiceLocatorMock
    {
    }

    [Export(typeof(IDifferentServiceLocatorMock))]
    public class ServiceLocatorMockThree : IDifferentServiceLocatorMock
    {
    }
}
