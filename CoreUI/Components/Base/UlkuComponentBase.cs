using CoreUI.Components.Utilities;

using Microsoft.AspNetCore.Components;


namespace CoreUI.Components.Base
{
    public class UlkuComponentBase : ComponentBase, IDisposable, IAsyncDisposable
    {

        [Parameter]
        public string? ElementId { get; set; }
        public ElementReference ElementRef { get; set; }

        private Queue<Func<Task>>? executeAfterRenderQueue;

        [Inject]
        protected BootstrapClassProvider BootstrapClassProvider { get; set; }
        protected CssClassBuilder? ClassBuilder { get; private set; }
        protected CssStyleBuilder? StyleBuilder { get; private set; }
      
        protected virtual string? StyleNames => Style;
        protected bool Disposed { get; private set; }
        protected bool AsyncDisposed { get; private set; }
        protected virtual bool ShouldAutoGenerateId => false;
        [Inject] IIdGenerator IdGenerator { get; set; }
      

        private string? customClass;
        protected virtual string? ClassNames => Class;
        [Parameter]
        public string? Class
        {
            get => customClass;
            set
            {
                customClass = value;

                DirtyClasses();
            }
        }
        protected virtual void BuildClasses(CssClassBuilder builder)
        {
            if (Class != null)
                builder.Append(Class);
        }
        public void Dispose() => Dispose(true);

        protected virtual void DirtyStyles() => StyleBuilder?.Dirty();
        protected internal virtual void DirtyClasses() => ClassBuilder?.Dirty();
       
        private string? customStyle;
        [Parameter]
        public string? Style
        {
            get => customStyle;
            set
            {
                customStyle = value;

                DirtyStyles();
            }
        }

        protected virtual void BuildStyles(CssStyleBuilder builder)
        {
            if (Style != null)
                builder.Append(Style);
        }
        protected override void OnInitialized()
        {
            if (ShouldAutoGenerateId && ElementId == null) ElementId = IdGenerator.GetNextId();
           
            base.OnInitialized();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!Disposed)
            {
                if (disposing)
                {
                    ClassBuilder = null;
                    StyleBuilder = null;
                }

                if (disposing && executeAfterRenderQueue != null)
                {
                    executeAfterRenderQueue.Clear();
                    executeAfterRenderQueue = null;
                }

                Disposed = true;
            }
        }

        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object> Attributes { get; set; }
      
        public async ValueTask DisposeAsync()
        {
            await DisposeAsync(true);

            Dispose(false);
        }

        protected virtual ValueTask DisposeAsync(bool disposing)
        {
            try
            {
                if (!AsyncDisposed)
                {
                    if (disposing)
                    {
                        ClassBuilder = null;
                        StyleBuilder = null;
                    }

                    AsyncDisposed = true;
                }

                return default;
            }
            catch (Exception ex)
            {
                return new ValueTask(Task.FromException(ex));
            }
        }
    }
}
