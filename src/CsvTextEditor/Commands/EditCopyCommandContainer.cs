﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EditCopyCommandContainer.cs" company="WildGums">
//   Copyright (c) 2008 - 2017 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace CsvTextEditor
{
    using Catel.MVVM;
    using CsvTextEditor.Providers;
    using Orc.ProjectManagement;

    public class EditCopyCommandContainer : EditProjectCommandContainerBase
    {
        #region Constructors
        public EditCopyCommandContainer(ICommandManager commandManager, IProjectManager projectManager, ICsvTextEditorInstanceProvider csvTextEditorInstanceProvider)
            : base(Commands.Edit.Copy, commandManager, projectManager, csvTextEditorInstanceProvider)
        {
        }
        #endregion

        #region Methods
        protected override bool CanExecute(object parameter)
        {
            if (!base.CanExecute(parameter))
            {
                return false;
            }

            return CsvTextEditorInstance?.HasSelection ?? false;
        }

        protected override void Execute(object parameter)
        {
            CsvTextEditorInstance.Copy();

            base.Execute(parameter);
        }
        #endregion
    }
}
