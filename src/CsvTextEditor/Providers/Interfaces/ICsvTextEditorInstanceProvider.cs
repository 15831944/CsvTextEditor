namespace CsvTextEditor.Providers
{
    using System;
    using Orc.CsvTextEditor;

    public interface ICsvTextEditorInstanceProvider
    {
        event EventHandler InstanceChanged;

        ICsvTextEditorInstance GetInstance();
        void SetInstance(ICsvTextEditorInstance csvTextEditorInstance);
    }
}
