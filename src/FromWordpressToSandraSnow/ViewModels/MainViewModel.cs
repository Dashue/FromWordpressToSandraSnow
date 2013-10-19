using ReactiveUI;
using System;

namespace FromWordpressToSandraSnow.ViewModels
{
    public class MainViewModel : ReactiveObject
    {
        public MainViewModel()
        {
            _wordPressToMarkdown = new FromWordpressToMarkdown();
            ConvertBlog = new ReactiveCommand(this.WhenAny(x => x.Path, s => false == string.IsNullOrWhiteSpace(s.Value)));


            ConvertBlog.Subscribe(param => _wordPressToMarkdown.Convert(Path));
        }

        private string _path;
        private FromWordpressToMarkdown _wordPressToMarkdown;

        public string Path
        {
            get { return _path; }
            set
            {
                this.RaiseAndSetIfChanged(ref _path, value);
            }
        }

        public ReactiveCommand ConvertBlog { get; set; }
    }
}