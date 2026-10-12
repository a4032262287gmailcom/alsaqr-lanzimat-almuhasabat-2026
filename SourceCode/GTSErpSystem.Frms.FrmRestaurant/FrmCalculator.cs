using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace GTSErpSystem.Frms.FrmRestaurant;

public class FrmCalculator : XtraForm
{
	private double resultValue = 0.0;

	private string operationPerformed = "";

	private bool isOperationPerformed = false;

	private IContainer components = null;

	private Button buttonOne;

	private Button button1;

	private Button button2;

	private Button button9;

	private Button button8;

	private Button button7;

	private Button button14;

	private Button button13;

	private Button button12;

	private Button button19;

	private Button button17;

	public TextBox textBox_Result;

	private Button BtnOK;

	private Button BtnDelete;

	public FrmCalculator()
	{
		InitializeComponent();
		textBox_Result.Focus();
	}

	private void button_click(object sender, EventArgs e)
	{
		if (textBox_Result.Text == "0" || isOperationPerformed)
		{
			textBox_Result.Clear();
		}
		isOperationPerformed = false;
		Button button = (Button)sender;
		if (button.Text == ".")
		{
			if (!textBox_Result.Text.Contains("."))
			{
				textBox_Result.Text += button.Text;
			}
		}
		else
		{
			textBox_Result.Text += button.Text;
		}
	}

	private void operator_click(object sender, EventArgs e)
	{
		Button button = (Button)sender;
		if (resultValue != 0.0)
		{
			operationPerformed = button.Text;
			isOperationPerformed = true;
		}
		else
		{
			operationPerformed = button.Text;
			resultValue = double.Parse(textBox_Result.Text);
			isOperationPerformed = true;
		}
	}

	private void button4_Click(object sender, EventArgs e)
	{
		textBox_Result.Text = "0";
	}

	private void button5_Click(object sender, EventArgs e)
	{
		textBox_Result.Text = "0";
		resultValue = 0.0;
	}

	private void button15_Click(object sender, EventArgs e)
	{
		switch (operationPerformed)
		{
		case "+":
			textBox_Result.Text = (resultValue + double.Parse(textBox_Result.Text)).ToString();
			break;
		case "-":
			textBox_Result.Text = (resultValue - double.Parse(textBox_Result.Text)).ToString();
			break;
		case "*":
			textBox_Result.Text = (resultValue * double.Parse(textBox_Result.Text)).ToString();
			break;
		case "/":
			textBox_Result.Text = (resultValue / double.Parse(textBox_Result.Text)).ToString();
			break;
		}
		resultValue = double.Parse(textBox_Result.Text);
	}

	private void BtnDelete_Click(object sender, EventArgs e)
	{
		textBox_Result.Text = "0";
	}

	private void BtnOK_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void textBox_Result_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Return)
		{
			Close();
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
		this.buttonOne = new System.Windows.Forms.Button();
		this.button1 = new System.Windows.Forms.Button();
		this.button2 = new System.Windows.Forms.Button();
		this.button9 = new System.Windows.Forms.Button();
		this.button8 = new System.Windows.Forms.Button();
		this.button7 = new System.Windows.Forms.Button();
		this.button14 = new System.Windows.Forms.Button();
		this.button13 = new System.Windows.Forms.Button();
		this.button12 = new System.Windows.Forms.Button();
		this.button19 = new System.Windows.Forms.Button();
		this.button17 = new System.Windows.Forms.Button();
		this.textBox_Result = new System.Windows.Forms.TextBox();
		this.BtnOK = new System.Windows.Forms.Button();
		this.BtnDelete = new System.Windows.Forms.Button();
		base.SuspendLayout();
		this.buttonOne.BackColor = System.Drawing.Color.LightCyan;
		this.buttonOne.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.buttonOne.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.buttonOne.ForeColor = System.Drawing.Color.RoyalBlue;
		this.buttonOne.Location = new System.Drawing.Point(11, 56);
		this.buttonOne.Margin = new System.Windows.Forms.Padding(4);
		this.buttonOne.Name = "buttonOne";
		this.buttonOne.Size = new System.Drawing.Size(79, 55);
		this.buttonOne.TabIndex = 24;
		this.buttonOne.Text = "7";
		this.buttonOne.UseVisualStyleBackColor = false;
		this.buttonOne.Click += new System.EventHandler(button_click);
		this.button1.BackColor = System.Drawing.Color.LightCyan;
		this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.button1.ForeColor = System.Drawing.Color.RoyalBlue;
		this.button1.Location = new System.Drawing.Point(97, 57);
		this.button1.Margin = new System.Windows.Forms.Padding(4);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(79, 55);
		this.button1.TabIndex = 25;
		this.button1.Text = "8";
		this.button1.UseVisualStyleBackColor = false;
		this.button1.Click += new System.EventHandler(button_click);
		this.button2.BackColor = System.Drawing.Color.LightCyan;
		this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.button2.ForeColor = System.Drawing.Color.RoyalBlue;
		this.button2.Location = new System.Drawing.Point(183, 57);
		this.button2.Margin = new System.Windows.Forms.Padding(4);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(79, 55);
		this.button2.TabIndex = 26;
		this.button2.Text = "9";
		this.button2.UseVisualStyleBackColor = false;
		this.button2.Click += new System.EventHandler(button_click);
		this.button9.BackColor = System.Drawing.Color.LightCyan;
		this.button9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button9.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.button9.ForeColor = System.Drawing.Color.RoyalBlue;
		this.button9.Location = new System.Drawing.Point(11, 120);
		this.button9.Margin = new System.Windows.Forms.Padding(4);
		this.button9.Name = "button9";
		this.button9.Size = new System.Drawing.Size(79, 55);
		this.button9.TabIndex = 27;
		this.button9.Text = "4";
		this.button9.UseVisualStyleBackColor = false;
		this.button9.Click += new System.EventHandler(button_click);
		this.button8.BackColor = System.Drawing.Color.LightCyan;
		this.button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button8.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.button8.ForeColor = System.Drawing.Color.RoyalBlue;
		this.button8.Location = new System.Drawing.Point(97, 120);
		this.button8.Margin = new System.Windows.Forms.Padding(4);
		this.button8.Name = "button8";
		this.button8.Size = new System.Drawing.Size(79, 55);
		this.button8.TabIndex = 28;
		this.button8.Text = "5";
		this.button8.UseVisualStyleBackColor = false;
		this.button8.Click += new System.EventHandler(button_click);
		this.button7.BackColor = System.Drawing.Color.LightCyan;
		this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.button7.ForeColor = System.Drawing.Color.RoyalBlue;
		this.button7.Location = new System.Drawing.Point(183, 120);
		this.button7.Margin = new System.Windows.Forms.Padding(4);
		this.button7.Name = "button7";
		this.button7.Size = new System.Drawing.Size(79, 55);
		this.button7.TabIndex = 29;
		this.button7.Text = "6";
		this.button7.UseVisualStyleBackColor = false;
		this.button7.Click += new System.EventHandler(button_click);
		this.button14.BackColor = System.Drawing.Color.LightCyan;
		this.button14.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button14.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.button14.ForeColor = System.Drawing.Color.RoyalBlue;
		this.button14.Location = new System.Drawing.Point(11, 183);
		this.button14.Margin = new System.Windows.Forms.Padding(4);
		this.button14.Name = "button14";
		this.button14.Size = new System.Drawing.Size(79, 55);
		this.button14.TabIndex = 30;
		this.button14.Text = "1";
		this.button14.UseVisualStyleBackColor = false;
		this.button14.Click += new System.EventHandler(button_click);
		this.button13.BackColor = System.Drawing.Color.LightCyan;
		this.button13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button13.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.button13.ForeColor = System.Drawing.Color.RoyalBlue;
		this.button13.Location = new System.Drawing.Point(97, 183);
		this.button13.Margin = new System.Windows.Forms.Padding(4);
		this.button13.Name = "button13";
		this.button13.Size = new System.Drawing.Size(79, 55);
		this.button13.TabIndex = 31;
		this.button13.Text = "2";
		this.button13.UseVisualStyleBackColor = false;
		this.button13.Click += new System.EventHandler(button_click);
		this.button12.BackColor = System.Drawing.Color.LightCyan;
		this.button12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button12.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.button12.ForeColor = System.Drawing.Color.RoyalBlue;
		this.button12.Location = new System.Drawing.Point(183, 183);
		this.button12.Margin = new System.Windows.Forms.Padding(4);
		this.button12.Name = "button12";
		this.button12.Size = new System.Drawing.Size(79, 55);
		this.button12.TabIndex = 32;
		this.button12.Text = "3";
		this.button12.UseVisualStyleBackColor = false;
		this.button12.Click += new System.EventHandler(button_click);
		this.button19.BackColor = System.Drawing.Color.LightCyan;
		this.button19.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button19.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.button19.ForeColor = System.Drawing.Color.RoyalBlue;
		this.button19.Location = new System.Drawing.Point(11, 246);
		this.button19.Margin = new System.Windows.Forms.Padding(4);
		this.button19.Name = "button19";
		this.button19.Size = new System.Drawing.Size(164, 55);
		this.button19.TabIndex = 33;
		this.button19.Text = "0";
		this.button19.UseVisualStyleBackColor = false;
		this.button19.Click += new System.EventHandler(button_click);
		this.button17.BackColor = System.Drawing.Color.LightCyan;
		this.button17.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button17.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.button17.ForeColor = System.Drawing.Color.RoyalBlue;
		this.button17.Location = new System.Drawing.Point(183, 246);
		this.button17.Margin = new System.Windows.Forms.Padding(4);
		this.button17.Name = "button17";
		this.button17.Size = new System.Drawing.Size(79, 55);
		this.button17.TabIndex = 34;
		this.button17.Text = ".";
		this.button17.UseVisualStyleBackColor = false;
		this.button17.Click += new System.EventHandler(button_click);
		this.textBox_Result.BackColor = System.Drawing.Color.White;
		this.textBox_Result.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.textBox_Result.ForeColor = System.Drawing.Color.RoyalBlue;
		this.textBox_Result.Location = new System.Drawing.Point(11, 14);
		this.textBox_Result.Margin = new System.Windows.Forms.Padding(4);
		this.textBox_Result.Name = "textBox_Result";
		this.textBox_Result.Size = new System.Drawing.Size(370, 34);
		this.textBox_Result.TabIndex = 0;
		this.textBox_Result.Text = "0";
		this.textBox_Result.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.textBox_Result.KeyDown += new System.Windows.Forms.KeyEventHandler(textBox_Result_KeyDown);
		this.BtnOK.BackColor = System.Drawing.Color.LightCyan;
		this.BtnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.BtnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 15f);
		this.BtnOK.ForeColor = System.Drawing.Color.RoyalBlue;
		this.BtnOK.Location = new System.Drawing.Point(268, 120);
		this.BtnOK.Name = "BtnOK";
		this.BtnOK.Size = new System.Drawing.Size(113, 181);
		this.BtnOK.TabIndex = 36;
		this.BtnOK.Text = "OK موافق";
		this.BtnOK.UseVisualStyleBackColor = false;
		this.BtnOK.Click += new System.EventHandler(BtnOK_Click);
		this.BtnDelete.BackColor = System.Drawing.Color.LightCyan;
		this.BtnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.BtnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.BtnDelete.ForeColor = System.Drawing.Color.RoyalBlue;
		this.BtnDelete.Location = new System.Drawing.Point(269, 57);
		this.BtnDelete.Margin = new System.Windows.Forms.Padding(4);
		this.BtnDelete.Name = "BtnDelete";
		this.BtnDelete.Size = new System.Drawing.Size(112, 56);
		this.BtnDelete.TabIndex = 37;
		this.BtnDelete.Text = "مسح";
		this.BtnDelete.UseVisualStyleBackColor = false;
		this.BtnDelete.Click += new System.EventHandler(BtnDelete_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(393, 315);
		base.Controls.Add(this.BtnDelete);
		base.Controls.Add(this.BtnOK);
		base.Controls.Add(this.textBox_Result);
		base.Controls.Add(this.button17);
		base.Controls.Add(this.button19);
		base.Controls.Add(this.button12);
		base.Controls.Add(this.button13);
		base.Controls.Add(this.button14);
		base.Controls.Add(this.button7);
		base.Controls.Add(this.button8);
		base.Controls.Add(this.button9);
		base.Controls.Add(this.button2);
		base.Controls.Add(this.button1);
		base.Controls.Add(this.buttonOne);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FrmCalculator";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Calculator";
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}


