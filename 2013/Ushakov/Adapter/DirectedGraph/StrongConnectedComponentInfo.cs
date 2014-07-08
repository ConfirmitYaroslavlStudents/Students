using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonComponents;
using GraphicalAdapter;

namespace CommonComponents
{
    public class StrongConnectedComponentInfo
    {
        public KosarajuAlgorithm StrongConnectedComponent { get; set; }
        public RGBColor[] VertexColors { get; set; }

        public StrongConnectedComponentInfo(KosarajuAlgorithm strongConnectedComponent, RGBColor[] vertexColors)
        {
            StrongConnectedComponent = strongConnectedComponent;
            VertexColors = vertexColors;
        }
    }
}
