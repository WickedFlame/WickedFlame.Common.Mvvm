The ServiceLocator Framework was downloaded from:
http://marlongrech.wordpress.com/2009/09/02/service-locator-in-mvvm/

Download:
http://cid-96f8d49aa44c79c1.skydrive.live.com/self.aspx/Public/ServiceLocator.zip?wa=wsignin1.0&sa=625953380

ServiceLocator Pattern description:
http://java.sun.com/blueprints/corej2eepatterns/Patterns/ServiceLocator.html
http://martinfowler.com/articles/injection.html


Code:
	public class ViewModelFatory : IFactory
	{
		#region IFactory Members

		public object CreateViewModel(System.Windows.DependencyObject sender)
		{
			var vm = new ViewModel();
			vm.ServiceLocator.RegisterService<IViewModelDataAccess>(new ViewModelDataAccess());
			return vm;
		}

		#endregion
	}

	public class ViewModel : StandardSelectionViewModel
	{
		...
	}

	public interface IViewModelDataAccess
	{
		ViewModelData GetModel();
	}

	public class ViewModelDataAccess : IViewModelDataAccess
	{
		public ViewModelData GetModel()
		{
			return vew ViewModelData();
		}
	}


WPF:
	<ctrl:UserControlExt 
			...
             xmlns:vm="clr-namespace:sage200.gui.common.mvvm.factory;assembly=sage200.gui.common"
             xmlns:fac="clr-namespace:sage200.gui.viewmodels.factories"
             vm:ViewModelLoader.FactoryType="{x:Type fac:ViewModelFatory}">
			 ...
	</ctrl:UserControlExt 