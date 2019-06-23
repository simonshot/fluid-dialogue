using System.Collections.Generic;
using CleverCrow.Fluid.Dialogues.Actions;
using CleverCrow.Fluid.Dialogues.Conditions;
using CleverCrow.Fluid.Dialogues.Graphs;

namespace CleverCrow.Fluid.Dialogues.Nodes.PlayGraph {
    public class NodePlayGraph : NodeBase {
        private readonly IGraphData _graph;

        public NodePlayGraph (
            IGraphData graph,
            List<INode> children,
            List<ICondition> conditions,
            List<IAction> enterActions,
            List<IAction> exitActions)
            : base(children, conditions, enterActions, exitActions) {
            _graph = graph;
        }

        public override void Play (IDialoguePlayback playback) {
            playback.ParentCtrl.PlayChild(_graph);
        }
    }
}
