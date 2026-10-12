using System.Threading;
using System.Windows.Forms;
using GTSErpSystem.Frms.FrmsMain;

namespace GTSErpSystem.BLL.Main;

public class Class_WaitFormFunc
{
	private WaitForm wait;

	private Thread loadthread;

	public void Show()
	{
		loadthread = new Thread((ThreadStart)LoadingProcess);
		loadthread.Start();
	}

	public void Show(Form parent)
	{
		loadthread = new Thread((ParameterizedThreadStart)LoadingProcess);
		loadthread.Start(parent);
	}

	public void Close()
	{
		if (wait != null)
		{
			wait.BeginInvoke(new ThreadStart(wait.CloseWaitForm));
			wait = null;
			loadthread = null;
		}
	}

	private void LoadingProcess()
	{
		wait = new WaitForm();
		wait.ShowDialog();
	}

	private void LoadingProcess(object parent)
	{
		Form parent2 = parent as Form;
		wait = new WaitForm(parent2);
		wait.ShowDialog();
	}
}


