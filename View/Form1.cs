using MiniTC.Model;
using MiniTC.Presenter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniTC.View
{
    public partial class Form1: Form
    {
        private PanelTC _activePanel;
        private PanelTC _inactivePanel;

        private FileManager _fileManager;
        private PanelTCPresenter _leftPresenter;
        private PanelTCPresenter _rightPresenter;

        public Form1()
        {
            InitializeComponent();

            _fileManager = new FileManager();
            _leftPresenter = new PanelTCPresenter(panelLeft, _fileManager);
            _rightPresenter = new PanelTCPresenter(panelRight, _fileManager);

            _activePanel = panelLeft;
            _inactivePanel = panelRight;

            panelLeft.GotFocus += Panel_GotFocus;
            panelRight.GotFocus += Panel_GotFocus;

            panelLeft.treeViewItems.NodeMouseClick += (s, e) => ActivatePanel(panelLeft);
            panelRight.treeViewItems.NodeMouseClick += (s, e) => ActivatePanel(panelRight);

            buttonСopy.Click += ButtonCopy_Click;
        }

        private void Panel_GotFocus(object sender, EventArgs e)
        {
            if (sender is PanelTC p)
                ActivatePanel(p);
        }

        private void ActivatePanel(PanelTC panel)
        {
            if (_activePanel == panel)
                return;

            _activePanel = panel;
            _inactivePanel = (panel == panelLeft) ? panelRight : panelLeft;
        }

        private void ButtonCopy_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedItem = _activePanel.SelectedItem;
                var sourcePath = _activePanel.CurrentPath;
                var destPath = _inactivePanel.CurrentPath;

                if (string.IsNullOrEmpty(selectedItem))
                {
                    MessageBox.Show("Brak zaznaczonego pliku lub folderu do skopiowania.", "Uwaga",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (Directory.Exists(selectedItem))
                {
                    var folderName = Path.GetFileName(selectedItem);
                    var destFolderFullPath = Path.Combine(destPath.TrimEnd('\\'), folderName);

                    _fileManager.CopyDirectory(selectedItem, destFolderFullPath);
                    _inactivePanel.RaiseRefreshRequested();

                    MessageBox.Show($"Folder '{folderName}' został skopiowany do '{destPath}'.", "Sukces",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (!File.Exists(selectedItem))
                {
                    MessageBox.Show("Wybrany element nie jest plikiem ani katalogiem lub nie istnieje.", "Błąd",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var fileName = Path.GetFileName(selectedItem);
                var fullSource = selectedItem;
                var fullDest = Path.Combine(destPath.TrimEnd('\\'), fileName);

                _fileManager.CopyFile(fullSource, fullDest);
                _inactivePanel.RaiseRefreshRequested();

                MessageBox.Show($"Plik '{fileName}' został skopiowany do '{destPath}'.", "Sukces",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas kopiowania: {ex.Message}", "Błąd",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
