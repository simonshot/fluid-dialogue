using CleverCrow.Fluid.Dialogues.Nodes;

namespace CleverCrow.Fluid.Dialogues.Editors.NodeDisplays {
    [NodeType(typeof(NodeRootData))]
    public class RootNode : NodeDisplayBase {
        private Connection _out;

        public override bool Protected => true;
        protected override bool HasInConnection => false;

        protected override void OnSetup () {
        }
    }
}
