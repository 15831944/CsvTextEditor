// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainViewModel.cs" company="WildGums">
//   Copyright (c) 2008 - 2017 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace CsvTextEditor.ViewModels
{
    using System.Threading.Tasks;
    using Catel;
    using Catel.Data;
    using Catel.Fody;
    using Catel.MVVM;
    using Catel.Services;
    using CsvTextEditor.Providers;
    using Models;
    using Orc.CsvTextEditor;
    using Orc.ProjectManagement;

    public class MainViewModel : ViewModelBase
    {
        #region Fields
        private readonly IProjectManager _projectManager;
        private readonly IDispatcherService _dispatcherService;
        private readonly ICsvTextEditorInstanceProvider _csvTextEditorProvider;
        #endregion

        #region Constructors
        public MainViewModel(IProjectManager projectManager, IDispatcherService dispatcherService, ICsvTextEditorInstanceProvider csvTextEditorProvider)
        {
            Argument.IsNotNull(() => projectManager);
            Argument.IsNotNull(() => dispatcherService);
            Argument.IsNotNull(() => csvTextEditorProvider);

            _projectManager = projectManager;
            _dispatcherService = dispatcherService;
            _csvTextEditorProvider = csvTextEditorProvider;
        }
        #endregion

        [Model]
        [Expose(nameof(Models.Project.Text))]
        public Project Project { get; set; }

        public ICsvTextEditorInstance CsvTextEditorInstance { get; set; }

        #region Methods
        protected override Task InitializeAsync()
        {
            _projectManager.ProjectActivationAsync += OnProjectActivationAsync;

            return base.InitializeAsync();
        }

        private Task OnProjectActivationAsync(object sender, ProjectUpdatingCancelEventArgs e)
        {
            return _dispatcherService.InvokeAsync(() => Project = (Project)e.NewProject);
        }

        protected override Task CloseAsync()
        {
            _projectManager.ProjectActivationAsync -= OnProjectActivationAsync;

            return base.CloseAsync();
        }

        protected override void OnPropertyChanged(AdvancedPropertyChangedEventArgs e)
        {
            if (string.Equals(e.PropertyName, nameof(CsvTextEditorInstance)))
            {
                _csvTextEditorProvider.SetInstance(e.NewValue as ICsvTextEditorInstance);
            }
            base.OnPropertyChanged(e);
        }
        #endregion
    }
}
