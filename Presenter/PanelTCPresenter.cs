using MiniTC.Model;
using MiniTC.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniTC.Presenter
{
    class PanelTCPresenter
    {
        private readonly IPanelTCView _view;
        private readonly FileManager _model;

        public PanelTCPresenter(IPanelTCView view, FileManager model)
        {
            _view = view;
            _model = model;

            _view.DriveSelected += OnDriveSelected;
            _view.PathItemSelected += OnPathItemSelected;
            _view.RefreshRequested += OnRefreshRequested;

            InitializeDrives();
        }

        private void InitializeDrives()
        {
            var drives = _model.GetDrives();
            _view.Drives = drives;
        }

        private void OnDriveSelected(string selectedDriveEvent)
        {
            var drives = _model.GetDrives();
            _view.Drives = drives;

            string current = _view.CurrentPath;
            if (string.IsNullOrEmpty(current) || current != selectedDriveEvent)
            {
                _view.CurrentPath = selectedDriveEvent;
                UpdateItems(selectedDriveEvent);
            }
        }

        private void OnPathItemSelected(string selectedPath)
        {
            if (string.IsNullOrEmpty(selectedPath))
                return;

            if (Directory.Exists(selectedPath))
            {
                _view.CurrentPath = selectedPath;
                UpdateItems(selectedPath);
            }
        }

        private void OnRefreshRequested()
        {
            var path = _view.CurrentPath;
            if (!string.IsNullOrEmpty(path))
                UpdateItems(path);
        }

        private void UpdateItems(string path)
        {
            var items = new List<string>();

            var parent = _model.GetParentDirectory(path);
            if (!string.IsNullOrEmpty(parent))
            {
                items.Add("..");
            }

            var dirs = _model.GetDirectories(path);
            foreach ( var dir in dirs.OrderBy(d => d))
            {
                var folderName = Path.GetFileName(dir);
                items.Add("<D> " + folderName);
            }

            var files = _model.GetFiles(path);
            foreach (var file in files.OrderBy(f => f))
            {
                var fileName = Path.GetFileName(file);
                items.Add(fileName);
            }

            _view.Items = items.ToArray();
        }
    }
}
