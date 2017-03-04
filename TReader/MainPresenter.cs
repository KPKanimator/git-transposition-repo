using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEditor.BusinessLogic;
using System.Drawing;

namespace TReader
{
    public class MainPresenter
    {
        private readonly IMainForm _view; // здесь события и вытягиваем содержимое из окошек
        private readonly IFileManager _manager; //работа с файлом, запись сохранение, проверка пути
        private readonly IMassageService _massageService;//выводит ошибки
        private ITransposition _transpos; //перестановка
        private string _currentFilePath;

        public MainPresenter(IMainForm view, IFileManager manager, IMassageService service,ITransposition trans)
        {
            _view = view;
            _manager = manager;
            _massageService = service;
            _transpos = trans;
            _view.SetSymbolCount(0);
            _view.ContentChanged += new EventHandler(_view_ContentChanged);
            _view.FileOpenClick += new EventHandler(_view_FileOpenClick);
            _view.FileSaveClick += new EventHandler(_view_FileSaveClick);
            _view.FileSaveAsClick += new EventHandler(_view_FileSaveAsClick);
            _view.TransposClick += new EventHandler(_view_GenerationTransport);
            ////////
            
        }

        private void _view_FileOpenClick(object sender, EventArgs e)
        {
            try
            {
                string filePath = _view.FilePath;
                bool isExist = _manager.IsExist(filePath);
                if (!isExist)
                {
                    _massageService.ShowMessage("Выбранный файл не существует");
                    return;
                }
                _currentFilePath = filePath;
                string content = _manager.GetContent(filePath);
                int count = _manager.GetSymbolCount(content);

                _view.Content = content;
                _view.SetSymbolCount(count);
            }
            catch (Exception ex)
            {
                _massageService.ShowError(ex.Message);
            }
        }

        private void _view_ContentChanged(object sender, EventArgs e)
        {
            string content = _view.Content;
            int count = _manager.GetSymbolCount(content);
            _view.SetSymbolCount(count);
            _view.Font_ch();
        }

        private void _view_FileSaveClick(object sender, EventArgs e)
        {
            try
            {
                string content = _view.Content;
                _manager.SaveContent(content, _currentFilePath);
                _massageService.ShowMessage("Файл успешно сохранён");
            }
            catch (Exception ex)
            {
                _massageService.ShowError(ex.Message);
            }
        }
        /////
        //private void _view_FontClick(object sender, EventArgs e)
        //{
        //  //  _view.ContentChanged += new EventHandler(_view_ContentChanged);
        //    _view.Font_ch();
        //}

        private void _view_FileSaveAsClick(object sender, EventArgs e)
        {
            try
            {
                string content = _view.Content;
                _manager.SaveContent(content, _view.FileSave);
                _massageService.ShowMessage("Файл успешно сохранён");
            }
            catch (Exception ex)
            {
                _massageService.ShowError(ex.Message);
            }
        }

        private void _view_GenerationTransport(object sender, EventArgs e)
        {
            string[] first = _view.Content.Split(' ');
            int[] first_transp = new int[first.Length];
            for (int i = 0; i < first.Length; ++i)
            first_transp[i] = Int32.Parse(first[i]);
            string permut;
            _transpos.Call(first.Length,first_transp,out permut);
            _view.Content = permut;
            
        }





    }
}
