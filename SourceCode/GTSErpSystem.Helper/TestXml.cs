using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace GTSErpSystem.Helper;

public class TestXml : Form
{
	private IContainer components = null;

	private Button button1;

	private RichTextBox richTextBox1;

	private RichTextBox richTextBox2;

	private RichTextBox richTextBox3;

	private RichTextBox richTextBox4;

	public TestXml()
	{
		InitializeComponent();
	}

	private Invoice CreateSampleInvoice()
	{
		Invoice invoice = new Invoice("A12345", DateTime.Now, CurrencyCode.USD);
		invoice.Type = InvoiceType.Invoice;
		invoice.Profile = Profile.Comfort;
		invoice.Buyer = new TradeParty
		{
			ID = "TX_1",
			Name = "Text Control GmbH",
			ContactName = "Peter Paulsen",
			City = "Bremen",
			Postcode = "28217",
			Country = CountryCode.DE,
			Street = "أœberseetor 18"
		};
		invoice.Seller = new TradeParty
		{
			ID = "TX_2",
			Name = "Text Control, LLC",
			ContactName = "Jack Jackson",
			City = "Charlotte, NC",
			Postcode = "28210",
			Country = CountryCode.US,
			Street = "6926 Shannon Willow Rd, Suite 400"
		};
		invoice.Seller.SpecifiedTaxRegistrations.Add(new TaxID
		{
			ID = "US12367623",
			Scheme = TaxScheme.VA
		});
		List<LineItem> list = new List<LineItem>();
		list.Add(new LineItem
		{
			Price = 200f,
			ProductID = "A123",
			Name = "Product A",
			Quantity = 5f,
			Total = 1000f,
			UnitCode = QuantityCodes.C62
		});
		foreach (LineItem item in list)
		{
			invoice.LineItems.Add(item);
		}
		invoice.TotalAmount = 1000f;
		return invoice;
	}

	private void button1_Click(object sender, EventArgs e)
	{
		Invoice invoice = CreateSampleInvoice();
		string text = invoice.CreateXml();
		richTextBox1.Text = text;
		using (MemoryStream memoryStream = new MemoryStream())
		{
			using XmlWriter xmlWriter = XmlWriter.Create(memoryStream);
			xmlWriter.WriteStartDocument();
			xmlWriter.WriteStartElement("Database");
			xmlWriter.WriteStartElement("Entry");
			xmlWriter.WriteAttributeString("key", "value");
			xmlWriter.WriteEndElement();
			xmlWriter.WriteEndElement();
			xmlWriter.Flush();
			xmlWriter.Close();
			Console.WriteLine(new UTF8Encoding().GetString(memoryStream.ToArray()));
			Console.WriteLine(BitConverter.ToString(memoryStream.ToArray()));
			string text2 = Convert.ToBase64String(memoryStream.ToArray());
			Console.WriteLine(text2);
			File.WriteAllText("D:\\A.enc", text2);
		}
		string text3 = File.ReadAllText("D:\\A.enc");
		Console.WriteLine(text3);
		richTextBox2.Text = text3;
		byte[] array = Convert.FromBase64String(text3);
		Console.WriteLine(BitConverter.ToString(array));
		Console.WriteLine(new UTF8Encoding().GetString(array));
		using (MemoryStream input = new MemoryStream(array))
		{
			using (XmlReader.Create(input))
			{
			}
		}
		richTextBox3.Text = HashCode.Base64Decode(text3);
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
		this.button1 = new System.Windows.Forms.Button();
		this.richTextBox1 = new System.Windows.Forms.RichTextBox();
		this.richTextBox2 = new System.Windows.Forms.RichTextBox();
		this.richTextBox3 = new System.Windows.Forms.RichTextBox();
		this.richTextBox4 = new System.Windows.Forms.RichTextBox();
		base.SuspendLayout();
		this.button1.Location = new System.Drawing.Point(570, 660);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(218, 51);
		this.button1.TabIndex = 0;
		this.button1.Text = "button1";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.richTextBox1.Location = new System.Drawing.Point(12, 2);
		this.richTextBox1.Name = "richTextBox1";
		this.richTextBox1.Size = new System.Drawing.Size(776, 153);
		this.richTextBox1.TabIndex = 1;
		this.richTextBox1.Text = "";
		this.richTextBox2.Location = new System.Drawing.Point(12, 384);
		this.richTextBox2.Name = "richTextBox2";
		this.richTextBox2.Size = new System.Drawing.Size(776, 85);
		this.richTextBox2.TabIndex = 1;
		this.richTextBox2.Text = "";
		this.richTextBox3.Location = new System.Drawing.Point(-407, -183);
		this.richTextBox3.Name = "richTextBox3";
		this.richTextBox3.Size = new System.Drawing.Size(776, 85);
		this.richTextBox3.TabIndex = 1;
		this.richTextBox3.Text = "";
		this.richTextBox4.Location = new System.Drawing.Point(12, 191);
		this.richTextBox4.Name = "richTextBox4";
		this.richTextBox4.Size = new System.Drawing.Size(776, 172);
		this.richTextBox4.TabIndex = 1;
		this.richTextBox4.Text = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(800, 738);
		base.Controls.Add(this.richTextBox3);
		base.Controls.Add(this.richTextBox4);
		base.Controls.Add(this.richTextBox2);
		base.Controls.Add(this.richTextBox1);
		base.Controls.Add(this.button1);
		base.Name = "TestXml";
		this.Text = "TestXml";
		base.ResumeLayout(false);
	}
}


