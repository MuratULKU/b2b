namespace CoreUI.Components.DataGrid
{
    public struct CssBuilder
    {
        private string stringBuffer;
        public CssBuilder(string value) => stringBuffer = value;
        public CssBuilder AddValue(string value)
        {
            stringBuffer += value;
            return this;
        }
        public CssBuilder AddClass(string value) => AddValue(" " + value);
        public CssBuilder AddClass(string value, bool when = true) => when ? this.AddClass(value) : this;
        public string Build()
        {
            // String buffer finalization code
            return stringBuffer != null ? stringBuffer.Trim() : string.Empty;
        }
    }
}
