using System.Collections.Generic;

namespace TesteFagronTech.Models.ViewModel
{
    public class ResponseViewModel<T>
    {
        public ResponseViewModel() { }

        public ResponseViewModel(T content, bool Success, List<string> erros)
        {
            Content = content;
            Success = Success;
            Errors = erros;
        }

        public T Content { get; set; }

        public bool Success { get; set; }

        public List<string> Errors { get; set; } = new List<string>();
    }
}
