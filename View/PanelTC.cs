using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MiniTC.View;

namespace MiniTC
{
    public partial class PanelTC : UserControl, IPanelTCView
    {
        public PanelTC()
        {
            InitializeComponent();

            comboBoxDrives.DropDown += (s, e) => DriveSelected?.Invoke(null);

            comboBoxDrives.SelectedIndexChanged += (s, e) =>
            {
                if (comboBoxDrives.SelectedItem != null)
                {
                    string selectedDrive = comboBoxDrives.SelectedItem.ToString();
                    DriveSelected?.Invoke(selectedDrive);
                }
            };

            treeViewItems.NodeMouseDoubleClick += (s, e) =>
            {
                if (e.Node != null)
                {
                    PathItemSelected?.Invoke(e.Node.Tag.ToString());
                }
            };
            treeViewItems.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter || treeViewItems.SelectedNode != null)
                {
                    PathItemSelected?.Invoke(treeViewItems.SelectedNode.Tag.ToString());
                    e.Handled = true;
                }
            };
        }

        public event Action<string> DriveSelected;
        public event Action<string> PathItemSelected;
        public event Action RefreshRequested;

        private string _currentPath;
        public string CurrentPath
        {
            get => _currentPath;
            set
            {
                _currentPath = value;
                textBoxPath.Text = value;
            }
        }

        private string[] _drives;
        public string[] Drives
        {
            get => _drives;
            set
            {
                _drives = value;
                comboBoxDrives.Items.Clear();
                if (_drives != null && _drives.Length > 0)
                    comboBoxDrives.Items.AddRange(_drives);
            }
        }

        private string[] _items;
        public string[] Items
        {
            get => _items;
            set
            {
                _items = value;
                treeViewItems.Nodes.Clear();

                if (_items == null || _items.Length == 0)
                    return;

                foreach (var item in _items)
                {
                    TreeNode node = new TreeNode(item);

                    if (item == "..")
                    {
                        var parent = Directory.GetParent(_currentPath);
                        if (parent != null)
                        {
                            node.Tag = parent.FullName;
                        }
                        else
                        {
                            node.Tag = null;
                        }
                    }

                    else if (item.StartsWith("<D> "))
                    {
                        var folderName = item.Substring(4);
                        node.Tag = Path.Combine(_currentPath, folderName);
                    }
                    else
                    {
                        node.Tag = Path.Combine(_currentPath, item);
                    }

                    treeViewItems.Nodes.Add(node);
                }
            }
        }

        public string SelectedItem
        {
            get => treeViewItems.SelectedNode?.Tag?.ToString();
        }

        public void RaiseRefreshRequested()
        {
            RefreshRequested?.Invoke();
        }
    }
}
