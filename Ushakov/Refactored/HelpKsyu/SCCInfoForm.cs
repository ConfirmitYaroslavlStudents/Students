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
	public partial class StrongConnectedComponentInfoForm : Form
	{
        StrongConnectedComponentInfo SCCInfo;

		public StrongConnectedComponentInfoForm(StrongConnectedComponentInfo sccInfo)
		{
            InitializeComponent();
            SCCInfo = sccInfo;
	    }

		private void SCCInfoFormLoad(object sender, EventArgs e)
		{
            byte DEFAULT_ALPHA = 255;
			for (int i = 0; i < SCCInfo.StrongConnectedComponent.StrongConnectedComponentCount; ++i)
			{
				DataGridViewRow tableRow = new DataGridViewRow();

				DataGridViewTextBoxCell numCell = new DataGridViewTextBoxCell();
				numCell.Value = (i + 1).ToString();

				DataGridViewTextBoxCell color = new DataGridViewTextBoxCell();
				color.Style.BackColor = System.Drawing.Color.FromArgb
						(DEFAULT_ALPHA, SCCInfo.VertexColors[i].R, SCCInfo.VertexColors[i].G, SCCInfo.VertexColors[i].B);
				
                DataGridViewTextBoxCell vertices = new DataGridViewTextBoxCell();
                for (int j = 0; j < SCCInfo.StrongConnectedComponent.GetStrongConnegtionComponentVerticesCount(i) - 1; ++j)
					vertices.Value += (SCCInfo.StrongConnectedComponent.GetVertex(i, j) + 1).ToString() + ", ";
				vertices.Value += (SCCInfo.StrongConnectedComponent.GetVertex(i, SCCInfo.StrongConnectedComponent.GetStrongConnegtionComponentVerticesCount(i) - 1) + 1).ToString();

				tableRow.Cells.Add(numCell);
				tableRow.Cells.Add(color);
				tableRow.Cells.Add(vertices);

				SCCTable.Rows.Add(tableRow);
			}
		}
	}
}
