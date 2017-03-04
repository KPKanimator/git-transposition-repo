using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace TReader
{
    public interface IMainForm
    {
        string FilePath { get; }
        string Content { get; set; }
        Font New_font { get;set;}
        string FileSave { get; set; }
        void SetSymbolCount(int count);
        event EventHandler FileOpenClick;
        event EventHandler FileSaveClick;
        event EventHandler ContentChanged;
        event EventHandler FileSaveAsClick;
        event EventHandler TransposClick;
       
        void Font_ch();

    }
}
