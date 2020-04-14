// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EditTrimWhitespacesCommandContainer.cs" company="WildGums">
//   Copyright (c) 2008 - 2017 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace CsvTextEditor
{
    using Catel.MVVM;
    using CsvTextEditor.Providers;
    using Orc.CsvTextEditor.Operations;
    using Orc.Notifications;
    using Orc.ProjectManagement;

    public class EditTrimWhitespacesCommandContainer : QuickFormatCommandContainerBase
    {
        #region Constructors
        public EditTrimWhitespacesCommandContainer(ICommandManager commandManager, IProjectManager projectManager, INotificationService notificationService,
            ICsvTextEditorInstanceProvider csvTextEditorInstanceProvider)
            : base(Commands.Edit.TrimWhitespaces, commandManager, projectManager, notificationService, csvTextEditorInstanceProvider)
        {
        }

        #endregion

        #region Methods
        protected override void EcecuteOperation()
        {
            CsvTextEditorInstance.ExecuteOperation<TrimWhitespacesOperation>();
        }

        protected override string GetOperationDescription()
        {
            return "trimming white-spaces";
        }
        #endregion
    }
}
