using System.Collections.Generic;
using CleverCrow.Fluid.Dialogues.Nodes;

namespace CleverCrow.Fluid.Dialogues {
    public interface IDialogueEvents {
        IUnityEvent Begin { get; }
        IUnityEvent End { get; }
        IUnityEvent<IActor, string> Speak { get; }
        IUnityEvent<IActor, string, List<IChoiceRuntime>> Choice { get; }
    }
}