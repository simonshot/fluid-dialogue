using System.Linq;
using CleverCrow.Fluid.Dialogues.Editors.NodeDisplays;
using CleverCrow.Fluid.Dialogues.Nodes;
using UnityEditor;
using UnityEngine;

namespace CleverCrow.Fluid.Dialogues.Editors {
    public class RightClickHandler {
        private readonly DialogueWindow _window;
        private readonly NodeSelection _selection;
        private readonly ScrollManager _scroll;
        private readonly DelayedMenu _menu;

        private NodeDisplayBase _clickedNode;
        private bool _isCameraDragging;

        public RightClickHandler (
            DialogueWindow window,
            NodeSelection selection,
            ScrollManager scroll,
            DelayedMenu menu) {
            _window = window;
            _selection = selection;
            _scroll = scroll;
            _menu = menu;
        }

        public void Update (Event e) {
            if (e.button != 1) return;

            if (e.type == EventType.MouseDown) {
                _clickedNode = _window.Nodes.Find(n => n.Data.rect.Contains(e.mousePosition));
            }

            if (_clickedNode == null) {
                EmptyContextClick(e);
            } else if (_clickedNode != null) {
                NodeContextClick(e);
            }

            if (e.type == EventType.MouseUp) {
                _clickedNode = null;
            }
        }

        private void NodeContextClick (Event e) {
            switch (e.type) {
                case EventType.MouseDown when !_selection.Contains(_clickedNode):
                    _selection.RemoveAll();
                    _selection.Add(_clickedNode);
                    GUI.changed = true;
                    break;
                case EventType.MouseUp when _selection.Selected.Count == 1:
                    _clickedNode.ShowContextMenu();
                    break;
                case EventType.MouseUp:
                    ShowEditGroupMenu(e);
                    break;
            }
        }

        private void EmptyContextClick (Event e) {
            switch (e.type) {
                case EventType.MouseDrag:
                    _scroll.ScrollPos -= e.delta;
                    _isCameraDragging = true;
                    e.Use();
                    break;

                case EventType.MouseUp: {
                    var wasDragging = _isCameraDragging;
                    _isCameraDragging = false;

                    if (wasDragging) break;

                    _selection.RemoveAll();
                    GUI.changed = true;
                    _menu.Display = () => { ShowCreateMenu(e); };

                    break;
                }
            }
        }

        private void ShowEditGroupMenu (Event e) {
            if (_selection.Selected.Any(i => i.Protected)) return;

            var menu = new GenericMenu();
            menu.AddItem(new GUIContent("Duplicate All"), false, () => {
                _window.DuplicateNode(_selection.Selected);
            });
            menu.AddItem(new GUIContent("Delete All"), false, () => {
                _window.DeleteNode(_selection.Selected);
            });

            menu.ShowAsContext();
        }

        private void ShowCreateMenu (Event e) {
            var menu = new GenericMenu();
            var mousePosition = e.mousePosition;
            foreach (var menuLine in NodeAssemblies.StringToData) {
                menu.AddItem(new GUIContent(menuLine.Key), false, () => {
                    var data = ScriptableObject.CreateInstance(menuLine.Value);
                    _window.CreateData(data as NodeDataBase, mousePosition);
                });
            }

            menu.ShowAsContext();
        }
    }
}
