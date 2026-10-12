using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace GTSErpSystem;

public class FrmLogin26 : MetroForm
{
	private IContainer components = null;

	public FrmLogin26()
	{
		InitializeComponent();
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(800, 450);
		this.Text = "FrmLogin26";
	}
}


