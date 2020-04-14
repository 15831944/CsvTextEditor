// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CsvTextEditorAutoCompleteProjectWatcher.cs" company="WildGums">
//   Copyright (c) 2008 - 2017 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace CsvTextEditor.ProjectManagement
{
    using System;
    using System.Threading.Tasks;
    using Catel;
    using Catel.IoC;
    using Catel.Services;
    using Catel.Threading;
    using CsvTextEditor.Providers;
    using Orc.CsvTextEditor;
    using Orc.ProjectManagement;

    public class CsvTextEditorAutoCompleteProjectWatcher : ProjectWatcherBase
    {
        #region Fields
        private const int MaxLineCountWithAutoCompleteEnabled = 1000;

        private readonly IDispatcherService _dispatcherService;
        private readonly ICsvTextEditorInstanceProvider _csvTextEditorProvider;
        private readonly IServiceLocator _serviceLocator;
        private ICsvTextEditorInstance _csvTextEditorInstance;
        #endregion

        #region Constructors
        public CsvTextEditorAutoCompleteProjectWatcher(IProjectManager projectManager, IServiceLocator serviceLocator,
            IDispatcherService dispatcherService, ICsvTextEditorInstanceProvider csvTextEditorProvider)
            : base(projectManager)
        {
            Argument.IsNotNull(() => serviceLocator);
            Argument.IsNotNull(() => dispatcherService);
            Argument.IsNotNull(() => csvTextEditorProvider);

            _serviceLocator = serviceLocator;
            _dispatcherService = dispatcherService;
            _csvTextEditorProvider = csvTextEditorProvider;
        }
        #endregion

        protected override Task OnActivatedAsync(IProject oldProject, IProject newProject)
        {
            if (_csvTextEditorInstance != null)
            {
                _csvTextEditorInstance.TextChanged -= CsvTextEditorInstanceOnTextChanged;
            }

            if (newProject == null)
            {
                return TaskHelper.Completed;
            }

            _csvTextEditorInstance = _csvTextEditorProvider.GetInstance();

            if (_csvTextEditorInstance != null)
            {
                _csvTextEditorInstance.TextChanged += CsvTextEditorInstanceOnTextChanged;
            }

            _dispatcherService.Invoke(RefreshAutoComplete, true);

            return base.OnActivatedAsync(oldProject, newProject);
        }

        private void CsvTextEditorInstanceOnTextChanged(object sender, EventArgs e)
        {
            _dispatcherService.Invoke(RefreshAutoComplete, true);
        }

        private void RefreshAutoComplete()
        {
            _csvTextEditorInstance.IsAutocompleteEnabled = _csvTextEditorInstance.LinesCount <= MaxLineCountWithAutoCompleteEnabled;
        }
    }
}
