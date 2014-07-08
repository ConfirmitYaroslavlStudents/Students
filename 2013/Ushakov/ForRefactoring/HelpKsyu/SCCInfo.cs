using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HelpKsyu
{
	public partial class SCCInfo : Form
	{
		static KosarajuAlgorithm SCC;
		HelpKsyuMainWindow.Color[] VrtClrs;

		public SCCInfo(KosarajuAlgorithm scc, HelpKsyuMainWindow.Color[] v_clrs)
		{
			InitializeComponent();
			SCC = scc;
			VrtClrs = v_clrs;
		}

		private void SCCInfo_Load(object sender, EventArgs e)
		{
			for (int i = 0; i < SCC.SCCCount; ++i)
			{
				DataGridViewRow tr = new DataGridViewRow();

				DataGridViewTextBoxCell num = new DataGridViewTextBoxCell();
				num.Value = (i + 1).ToString();
				DataGridViewTextBoxCell col = new DataGridViewTextBoxCell();
				col.Style.BackColor = System.Drawing.Color.FromArgb
						(VrtClrs[i].A, VrtClrs[i].R, VrtClrs[i].G, VrtClrs[i].B);
				DataGridViewTextBoxCell vrts = new DataGridViewTextBoxCell();
				for (int j = 0; j < SCC.SCCVrtxCount(i) - 1; ++j)
					vrts.Value += (SCC.GetVertex(i, j) + 1).ToString() + ", ";
				vrts.Value += (SCC.GetVertex(i, SCC.SCCVrtxCount(i) - 1) + 1).ToString();

				tr.Cells.Add(num);
				tr.Cells.Add(col);
				tr.Cells.Add(vrts);

				SCCTable.Rows.Add(tr);
			}
		}
	}
}
