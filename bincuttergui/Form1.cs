using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bincuttergui
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		const int bufsize = 1024;
		byte[] buf = new byte[bufsize];
		byte[] zero_array = new byte[bufsize];
		int readed_size = 0;
		int total_readed_size = 0;

		private void label1_Click(object sender, EventArgs e)
		{

		}

		void btnOpen_Click(object sender, EventArgs e)
		{
			if (txtStart.Text.Length == 0 | txtSize.Text.Length == 0)
				MessageBox.Show("Error!");
			else
			{
				OpenFileDialog ofd = new OpenFileDialog();
				ofd.FileName = "";
				ofd.InitialDirectory = "";
				ofd.Title = "Open File Name";
				ofd.RestoreDirectory = true;
				if (ofd.ShowDialog() == DialogResult.OK)
				{
					FileStream ofs = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read);

					SaveFileDialog sfd = new SaveFileDialog();
					sfd.Title = "Save File Name";
					sfd.FileName = "";
					sfd.InitialDirectory = "";
					sfd.RestoreDirectory = true;
					if (sfd.ShowDialog() == DialogResult.OK)
					{
						FileStream sfs = new FileStream(sfd.FileName, FileMode.Create, FileAccess.Write);

						ofs.Seek(Convert.ToInt32(txtStart.Text, 16), SeekOrigin.Begin);

						while (true)
						{
							try
							{
								readed_size = ofs.Read(buf, 0, bufsize);
								total_readed_size += readed_size;
							}
							catch
							{
								MessageBox.Show("Read Err at " + ofs.Position);

								if (ofs.Length - ofs.Position < bufsize)
								{
									readed_size = (int)(ofs.Length - ofs.Position);
								}
								else
								{
									readed_size = bufsize;
								}
								ofs.Seek(readed_size, SeekOrigin.Current);
								sfs.Write(zero_array, 0, readed_size);
								continue;
							}
							if (total_readed_size > Convert.ToInt32(txtSize.Text, 16) | readed_size == 0)
								break;
							sfs.Write(buf, 0, readed_size);
						}
						ofs.Close();
						sfs.Close();
						readed_size = 0;
						total_readed_size = 0;
						MessageBox.Show("Done!");
					}
				}
			}
		}

		private void btnMinus100h_Click(object sender, EventArgs e)
		{
			if (txtStart.Text.Length != 0)
			{
				int tmp = Convert.ToInt32(txtStart.Text, 16) - 0x100;
				txtStart.Text = Convert.ToString(tmp, 16);
			}
		}
	}
}
