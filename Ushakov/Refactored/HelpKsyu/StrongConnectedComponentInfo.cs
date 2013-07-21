using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelpKsyu
{
    public class StrongConnectedComponentInfo
    {
        public KosarajuAlgorithm StrongConnectedComponent { get; set; }
        public HelpKsyuMainWindow.RGBAColor[] VertexColors { get; set; }

        public StrongConnectedComponentInfo(KosarajuAlgorithm strongConnectedComponent,
            HelpKsyuMainWindow.RGBAColor[] vertexColors)
        {
            StrongConnectedComponent = strongConnectedComponent;
            VertexColors = vertexColors;
        }
    }
}
