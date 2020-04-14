// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EditProjectCommandContainerBase.cs" company="WildGums">
//   Copyright (c) 2008 - 2017 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace CsvTextEditor
{
    using System;
    using System.Windows.Threading;
    using Catel;
    using Catel.MVVM;
    using CsvTextEditor.Providers;
    using Models;
    using Orc.CsvTextEditor;
    using Orc.ProjectManagement;

    public abstract class EditProjectCommandContainerBase : ProjectCommandContainerBase
    {
        #region Fields
        private readonly ICsvTextEditorInstanceProvider _csvTextEditorProvider;
        private readonly DispatcherTimer _invalidateTimer;
        #endregion

        #region Constructors
        protected EditProjectCommandContainerBase(string commandName, ICommandManager commandManager, IProjectManager projectManager, ICsvTextEditorInstanceProvider csvTextEditorProvider)
            : base(commandName, commandManager, projectManager)
        {
            Argument.IsNotNull(() => csvTextEditorProvider);

            _csvTextEditorProvider = csvTextEditorProvider;
            _invalidateTimer = new DispatcherTimer();
            _invalidateTimer.Interval = TimeSpan.FromMilliseconds(100);
            _invalidateTimer.Tick += OnInvalidateTimerTick;
        }
        #endregion

        protected ICsvTextEditorInstance CsvTextEditorInstance => _csvTextEditorProvider.GetInstance();

        protected override void ProjectActivated(Project oldProject, Project newProject)
        {
            base.ProjectActivated(oldProject, newProject);

            var csvTextEditorInstance = CsvTextEditorInstance;

            if (csvTextEditorInstance != null)
            {
                csvTextEditorInstance.TextChanged -= CsvTextEditorInstanceOnTextChanged;
            }
        }

        private void CsvTextEditorInstanceOnTextChanged(object sender, EventArgs eventArgs)
        {
            _commandManager.InvalidateCommands();
        }

        #region Methods
        protected override bool CanExecute(object parameter)
        {
            if (!base.CanExecute(parameter))
            {
                return false;
            }

            var isEditorServiceNull = ReferenceEquals(CsvTextEditorInstance, null);
            if (isEditorServiceNull && !_invalidateTimer.IsEnabled)
            {
                _invalidateTimer.Start();
            }

            return !isEditorServiceNull;
        }

        private void OnInvalidateTimerTick(object sender, EventArgs e)
        {
            _invalidateTimer.Stop();

            _commandManager.InvalidateCommands();
        }
        #endregion
    }
}
