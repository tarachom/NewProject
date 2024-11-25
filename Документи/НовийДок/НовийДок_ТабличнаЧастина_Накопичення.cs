
/*
        НовийДок_ТабличнаЧастина_Накопичення.cs
        Таблична Частина
*/

using Gtk;
using InterfaceGtk;
using AccountingSoftware;
using NewProject_1_0.Довідники;
using NewProject_1_0.Документи;
using NewProject_1_0.Перелічення;

namespace NewProject
{
    class НовийДок_ТабличнаЧастина_Накопичення : ДокументТабличнаЧастина
    {
        public НовийДок_Objest? ЕлементВласник { get; set; }

        #region Записи

        enum Columns
        {
            Користувач,
            Сума,

        }

        ListStore Store = new ListStore([

            typeof(string), //Користувач
            typeof(float), //Сума
        ]);

        List<Запис> Записи = [];

        private class Запис
        {
            public Guid ID { get; set; } = Guid.Empty;
            public Користувачі_Pointer Користувач { get; set; } = new Користувачі_Pointer();
            public decimal Сума { get; set; } = 0;


            public object[] ToArray()
            {
                return
                [
                    Користувач.Назва,
                    (float)Сума,
                ];
            }

            public static Запис Clone(Запис запис)
            {
                return new Запис
                {
                    ID = Guid.Empty,
                    Користувач = запис.Користувач.Copy(),
                    Сума = запис.Сума,

                };
            }

            public static async ValueTask ПісляЗміни_Користувач(Запис запис)
            {
                await запис.Користувач.GetPresentation();
            }
        }

        #endregion

        public НовийДок_ТабличнаЧастина_Накопичення()
        {
            TreeViewGrid.Model = Store;
            AddColumn();
        }

        void AddColumn()
        {
            //Користувач
            {
                TreeViewColumn column = new TreeViewColumn("Користувач", new CellRendererText(), "text", (int)Columns.Користувач) { Resizable = true, MinWidth = 200 };

                SetColIndex(column, Columns.Користувач);
                TreeViewGrid.AppendColumn(column);
            }

            //Сума
            {
                CellRendererText cellNumber = new CellRendererText() { Editable = true, Xalign = 1 };
                cellNumber.Edited += EditCell;
                TreeViewColumn column = new TreeViewColumn("Сума", cellNumber, "text", (int)Columns.Сума) { Resizable = true, Alignment = 1, MinWidth = 100 };
                column.SetCellDataFunc(cellNumber, new TreeCellDataFunc(NumericCellDataFunc));

                SetColIndex(column, Columns.Сума);
                TreeViewGrid.AppendColumn(column);
            }

            //Колонка пустишка для заповнення вільного простору
            TreeViewGrid.AppendColumn(new TreeViewColumn());
        }
        #region Load and Save

        public override async ValueTask LoadRecords()
        {
            Store.Clear();
            Записи.Clear();

            if (ЕлементВласник != null)
            {
                ЕлементВласник.Накопичення_TablePart.FillJoin([]);
                await ЕлементВласник.Накопичення_TablePart.Read();
                foreach (НовийДок_Накопичення_TablePart.Record record in ЕлементВласник.Накопичення_TablePart.Records)
                {
                    Запис запис = new Запис
                    {
                        ID = record.UID,
                        Користувач = record.Користувач,
                        Сума = record.Сума,

                    };

                    Записи.Add(запис);
                    Store.AppendValues(запис.ToArray());
                }
            }
        }

        public override async ValueTask SaveRecords()
        {
            if (ЕлементВласник != null)
            {
                ЕлементВласник.Накопичення_TablePart.Records.Clear();
                foreach (Запис запис in Записи)
                {
                    ЕлементВласник.Накопичення_TablePart.Records.Add(new НовийДок_Накопичення_TablePart.Record()
                    {
                        UID = запис.ID,
                        Користувач = запис.Користувач,
                        Сума = запис.Сума,

                    });
                }
                await ЕлементВласник.Накопичення_TablePart.Save(true);
                await LoadRecords();
            }
        }

        #endregion

        #region Func

        protected override ФормаЖурнал? OpenSelect(TreeIter iter, int rowNumber, int colNumber)
        {
            Запис запис = Записи[rowNumber];
            switch ((Columns)colNumber)
            {
                case Columns.Користувач:
                    {
                        Користувачі_ШвидкийВибір page = new()
                        {
                            DirectoryPointerItem = запис.Користувач.UnigueID,
                            CallBack_OnSelectPointer = async (UnigueID selectPointer) =>
                            {
                                запис.Користувач = new Користувачі_Pointer(selectPointer);
                                await Запис.ПісляЗміни_Користувач(запис);
                                Store.SetValues(iter, запис.ToArray());
                            },
                            CallBack_OnMultipleSelectPointer = async (UnigueID[] selectPointers) =>
                            {
                                foreach (var selectPointer in selectPointers)
                                {
                                    (Запис запис, TreeIter iter) = НовийЗапис();

                                    запис.Користувач = new Користувачі_Pointer(selectPointer);
                                    await Запис.ПісляЗміни_Користувач(запис);
                                    Store.SetValues(iter, запис.ToArray());
                                }
                            }
                        };
                        return page;
                    }

                default: return null;
            }
        }

        protected override void ClearCell(TreeIter iter, int rowNumber, int colNumber)
        {
            Запис запис = Записи[rowNumber];
            switch ((Columns)colNumber)
            {
                case Columns.Користувач: { запис.Користувач.Clear(); break; }

                default: break;
            }
            Store.SetValues(iter, запис.ToArray());
        }

        protected override void ChangeCell(TreeIter iter, int rowNumber, int colNumber, string newText)
        {
            Запис запис = Записи[rowNumber];
            switch ((Columns)colNumber)
            {
                case Columns.Сума: { var (check, value) = Validate.IsDecimal(newText); if (check) запис.Сума = value; break; }

                default: break;
            }
            Store.SetValues(iter, запис.ToArray());
        }

        void NumericCellDataFunc(TreeViewColumn column, CellRenderer cell, ITreeModel model, TreeIter iter)
        {
            if (GetColIndex(column, out int colNumber))
            {
                int rowNumber = int.Parse(Store.GetPath(iter).ToString());
                Запис запис = Записи[rowNumber];

                CellRendererText cellText = (CellRendererText)cell;
                cellText.Foreground = "green";

                switch ((Columns)colNumber)
                {
                    case Columns.Сума:
                        {
                            cellText.Text = запис.Сума.ToString();
                            break;
                        }
                    default: break;
                }
            }
        }


        #endregion

        #region ToolBar

        (Запис запис, TreeIter iter) НовийЗапис()
        {
            Запис запис = new Запис();
            Записи.Add(запис);

            TreeIter iter = Store.AppendValues(запис.ToArray());
            TreeViewGrid.SetCursor(Store.GetPath(iter), TreeViewGrid.Columns[0], false);

            return (запис, iter);
        }

        protected override void AddRecord()
        {
            НовийЗапис();
        }

        protected override void CopyRecord(int rowNumber)
        {
            Запис запис = Записи[rowNumber];
            Запис записНовий = Запис.Clone(запис);
            Записи.Add(записНовий);

            TreeIter iter = Store.AppendValues(записНовий.ToArray());
            TreeViewGrid.SetCursor(Store.GetPath(iter), TreeViewGrid.Columns[0], false);
        }

        protected override void DeleteRecord(TreeIter iter, int rowNumber)
        {
            Запис запис = Записи[rowNumber];
            Записи.Remove(запис);
            Store.Remove(ref iter);
        }

        #endregion
    }
}