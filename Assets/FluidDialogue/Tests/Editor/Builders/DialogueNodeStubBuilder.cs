using System.Collections.Generic;
using CleverCrow.Fluid.Dialogues.Actions;
using CleverCrow.Fluid.Dialogues.Choices;
using CleverCrow.Fluid.Dialogues.Nodes;
using NSubstitute;

namespace CleverCrow.Fluid.Dialogues.Builders {
    public class DialogueNodeStubBuilder {
        private INode _next;
        private readonly List<IAction> _exitActions = new List<IAction>();
        private readonly List<IAction> _enterActions = new List<IAction>();
        private readonly List<IChoice> _choices = new List<IChoice>();
        private bool _isValid = true;
        private INode _clone;

        public DialogueNodeStubBuilder WithNextResult (INode node) {
            _next = node;
            return this;
        }

        public DialogueNodeStubBuilder WithEnterAction (IAction action) {
            _enterActions.Add(action);
            return this;
        }

        public DialogueNodeStubBuilder WithExitAction (IAction action) {
            _exitActions.Add(action);
            return this;
        }

        public DialogueNodeStubBuilder WithChoice (IChoice choice) {
            _choices.Add(choice);
            return this;
        }

        public DialogueNodeStubBuilder WithIsValid (bool valid) {
            _isValid = valid;
            return this;
        }

        public INode Build () {
            var node = Substitute.For<INode>();
            node.Next().Returns(_next);
            node.ExitActions.Returns(_exitActions);
            node.EnterActions.Returns(_enterActions);
            node.IsValid.Returns(_isValid);

            for (var i = 0; i < _choices.Count; i++) {
                node.GetChoice(i).Returns(_choices[i]);
            }

            return node;
        }
    }
}
