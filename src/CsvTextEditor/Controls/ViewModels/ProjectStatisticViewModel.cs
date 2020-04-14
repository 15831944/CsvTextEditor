// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProjectStatisticViewModel.cs" company="WildGums">
//   Copyright (c) 2008 - 2017 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace CsvTextEditor.ViewModels
{
    using System;
    using Catel;
    using Catel.MVVM;
    using CsvTextEditor.Providers;
    using Orc.CsvTextEditor;

    public class ProjectStatisticViewModel : ViewModelBase
    {
        #region Fields
        private readonly ICsvTextEditorInstanceProvider _csvTextEditorProvider;
        private ICsvTextEditorInstance _csvTextEditorInstance;
        #endregion

        #region Constructors
        public ProjectStatisticViewModel(ICsvTextEditorInstanceProvider csvTextEditorProvider)
        {
            Argument.IsNotNull(() => csvTextEditorProvider);

            _csvTextEditorProvider = csvTextEditorProvider;

            _csvTextEditorProvider.InstanceChanged += OnCsvTextEditorProviderInstanceChanged;
        }

        #endregion

        #region Properties
        public int ColumnsCount { get; private set; }
        public int RowsCount { get; private set; }
        #endregion

        #region Methods

        private void OnCsvTextEditorProviderInstanceChanged(object sender, EventArgs e)
        {
            if (_csvTextEditorInstance != null)
            {
                _csvTextEditorInstance.TextChanged -= OnTextChanged;
            }

            _csvTextEditorInstance = _csvTextEditorProvider.GetInstance();
            _csvTextEditorInstance.TextChanged += OnTextChanged;

            UpdateStatistic();
        }

        private void UpdateStatistic()
        {
            RowsCount = _csvTextEditorInstance.LinesCount;
            ColumnsCount = _csvTextEditorInstance.ColumnsCount;
        }

        private void OnTextChanged(object sender, EventArgs e)
        {
            UpdateStatistic();
        }
        #endregion
    }
}
