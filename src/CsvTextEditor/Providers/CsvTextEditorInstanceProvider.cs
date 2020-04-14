namespace CsvTextEditor.Providers
{
    using System;
    using Orc.CsvTextEditor;

    public class CsvTextEditorInstanceProvider : ICsvTextEditorInstanceProvider
    {
        private ICsvTextEditorInstance _csvTextEditorInstance;

        public event EventHandler InstanceChanged;

        public void SetInstance(ICsvTextEditorInstance csvTextEditorInstance)
        {
            _csvTextEditorInstance = csvTextEditorInstance;
            RaiseInstanceChanged();
        }

        public ICsvTextEditorInstance GetInstance()
        {
            return _csvTextEditorInstance;
        }

        private void RaiseInstanceChanged()
        {
            InstanceChanged?.Invoke(this, new EventArgs());
        }
    }
}
