using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WickedFlame.Common.Mvvm.Commands;

namespace WickedFlame.Common.Mvvm.UnitTest
{
    [TestClass]
    public class RelayCommandTests
    {
        [TestMethod]
        public void RelayCommand()
        {
            int a = 0;
            var command = new RelayCommand<int>(v => a = v);

            command.Execute(1);

            Assert.AreEqual(a, 1);
        }
    }
}
