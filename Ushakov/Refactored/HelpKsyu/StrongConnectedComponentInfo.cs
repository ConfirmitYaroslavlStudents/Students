using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelpKsyu
{
    public class StrongConnectedComponentInfo
    {
        public KosarajuAlgorithm StrongConnectedComponent { get; set; }
        public HelpKsyuMainWindow.RGBColor[] VertexColors { get; set; }

        public StrongConnectedComponentInfo(KosarajuAlgorithm strongConnectedComponent,
            HelpKsyuMainWindow.RGBColor[] vertexColors)
        {
            StrongConnectedComponent = strongConnectedComponent;
            VertexColors = vertexColors;
        }
    }
}
