using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using GTSErpSystem.Properties;

namespace GTSErpSystem.Frms.FrmsMain;

public class WaitForm : Form
{
	private IContainer components = null;

	private Panel panel1;

	private Label label2;

	private Label label1;

	public WaitForm()
	{
		InitializeComponent();
		base.StartPosition = FormStartPosition.CenterParent;
	}

	public WaitForm(Form parent)
	{
		InitializeComponent();
		if (parent != null)
		{
			base.StartPosition = FormStartPosition.Manual;
			base.Location = new Point(parent.Location.X + parent.Width / 2 - base.Width / 2, parent.Location.Y + parent.Height / 2 - base.Height / 2);
		}
		else
		{
			base.StartPosition = FormStartPosition.CenterParent;
		}
	}

	public void CloseWaitForm()
	{
		base.DialogResult = DialogResult.OK;
		Close();
		if (label1.Image != null)
		{
			label1.Image.Dispose();
		}
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
		this.panel1 = new System.Windows.Forms.Panel();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.panel1.SuspendLayout();
		base.SuspendLayout();
		this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel1.Controls.Add(this.label1);
		this.panel1.Controls.Add(this.label2);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(257, 100);
		this.panel1.TabIndex = 0;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Century Gothic", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.Location = new System.Drawing.Point(98, 37);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(146, 23);
		this.label2.TabIndex = 1;
		this.label2.Text = "Please Wait....";
		this.label1.Dock = System.Windows.Forms.DockStyle.Left;
		this.label1.Image = GTSErpSystem.Properties.Resources._126;
		this.label1.Location = new System.Drawing.Point(0, 0);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(91, 98);
		this.label1.TabIndex = 2;
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		this.BackColor = System.Drawing.Color.White;
		base.ClientSize = new System.Drawing.Size(257, 100);
		base.Controls.Add(this.panel1);
		this.Font = new System.Drawing.Font("Century Gothic", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Name = "WaitForm";
		this.Text = "WaitForm";
		base.TopMost = true;
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		base.ResumeLayout(false);
	}
}


