
/*     
        НовийДок_PointerControl.cs
        PointerControl
*/
using Gtk;
using InterfaceGtk;
using AccountingSoftware;
using NewProject_1_0.Документи;

namespace NewProject
{
    class НовийДок_PointerControl : PointerControl
    {
        event EventHandler<НовийДок_Pointer> PointerChanged;

        public НовийДок_PointerControl()
        {
            pointer = new НовийДок_Pointer();
            WidthPresentation = 300;
            Caption = $"{НовийДок_Const.FULLNAME}:";
            PointerChanged += async (object? _, НовийДок_Pointer pointer) =>
            {
                Presentation = pointer != null ? await pointer.GetPresentation() : "";
            };
        }

        НовийДок_Pointer pointer;
        public НовийДок_Pointer Pointer
        {
            get
            {
                return pointer;
            }
            set
            {
                pointer = value;
                PointerChanged?.Invoke(null, pointer);
            }
        }

        protected override void OpenSelect(object? sender, EventArgs args)
        {
            BeforeClickOpenFunc?.Invoke();
            НовийДок page = new НовийДок
            {
                DocumentPointerItem = Pointer.UnigueID,
                CallBack_OnSelectPointer = (UnigueID selectPointer) =>
                {
                    Pointer = new НовийДок_Pointer(selectPointer);
                    AfterSelectFunc?.Invoke();
                }
            };

            NotebookFunction.CreateNotebookPage(Program.GeneralNotebook, $"Вибір - {НовийДок_Const.FULLNAME}", () => page);
            page.SetValue();
        }

        protected override void OnClear(object? sender, EventArgs args)
        {
            Pointer = new НовийДок_Pointer();
            AfterSelectFunc?.Invoke();
        }
    }
}
    