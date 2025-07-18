﻿
/*
 *
 * Конфігурації "Новий проєкт"
 * Автор 
  
 * Дата конфігурації: 14.07.2025 14:05:51
 *
 *
 * Цей код згенерований в Конфігураторі 3. Шаблон Gtk.xslt
 *
 */

using Gtk;
using InterfaceGtk3;
using AccountingSoftware;
using GeneratedCode.Перелічення;
using NewProject;

namespace GeneratedCode.Довідники.ТабличніСписки
{
    
    #region DIRECTORY "Користувачі"
      
    public class Користувачі_Записи : ТабличнийСписок
    {
        bool DeletionLabel = false;
        string ID = "";
        
        string Код = "";
        
        string Назва = "";
        

        object[] ToArray()
        {
            return
            [
                DeletionLabel ? InterfaceGtk3.Іконки.ДляТабличногоСписку.Delete : InterfaceGtk3.Іконки.ДляТабличногоСписку.Normal,
                ID,
                /*Код*/ Код,
                /*Назва*/ Назва,
                
            ];
        }

        public static void AddColumns(TreeView treeView)
        {
            treeView.Model = new ListStore(
            [
                /*Image*/ typeof(Gdk.Pixbuf), 
                /*ID*/ typeof(string),
                /*Код*/ typeof(string),  
                /*Назва*/ typeof(string),  
                
            ]);

            treeView.AppendColumn(new TreeViewColumn("", new CellRendererPixbuf(), "pixbuf", 0)); /* { Ypad = 4 } */
            treeView.AppendColumn(new TreeViewColumn("ID", new CellRendererText(), "text", 1) { Visible = false });

            /* Поля */
            treeView.AppendColumn(new TreeViewColumn("Код", new CellRendererText() { Xpad = 4 }, "text", 2) { MinWidth = 20, Resizable = true, SortColumnId = 2 } ); /*Код*/
            treeView.AppendColumn(new TreeViewColumn("Назва", new CellRendererText() { Xpad = 4 }, "text", 3) { MinWidth = 20, Resizable = true, SortColumnId = 3 } ); /*Назва*/
            

            /* Додаткові поля */
            

            //Пустишка
            treeView.AppendColumn(new TreeViewColumn());
        }

        public static void CreateFilter(TreeView treeView, ListFilterControl filterControl)
        {
          
        }

        public static async ValueTask UpdateRecords(TreeView treeView, List<ObjectChanged> recordsChanged)
        {
            ListStore Store = (ListStore)treeView.Model;
            Dictionary<Guid, (TreeIter Iter, TypeObjectChanged Type)> records = [];

            //Update
            List<ObjectChanged> recordsChangedUpdate = [.. recordsChanged.Where(x => x.Type == TypeObjectChanged.Update)];
            void findIter(TreeIter iter)
            {
                do
                {
                    Guid uid = Guid.Parse((string)Store.GetValue(iter, 1));
                    if (recordsChangedUpdate.Any(x => x.Uid == uid)) records.Add(uid, (iter, TypeObjectChanged.Update));
                    
                }
                while (Store.IterNext(ref iter));
            }
            if (Store.GetIterFirst(out TreeIter iter)) findIter(iter);

            if (records.Count > 0)
            {
                Довідники.Користувачі_Select Користувачі_Select = new Довідники.Користувачі_Select();
                Користувачі_Select.QuerySelect.Field.AddRange(
                [
                    /*Помітка на видалення*/ "deletion_label",
                    /*Код*/ Довідники.Користувачі_Const.Код,
                    /*Назва*/ Довідники.Користувачі_Const.Назва,
                    
                ]);

                Користувачі_Select.QuerySelect.Where.Add(new Where("uid", Comparison.IN, "'" + string.Join("', '", records.Select(x => x.Key)) + "'", true));

                

                /* SELECT */
                await Користувачі_Select.Select();

                while (Користувачі_Select.MoveNext())
                {
                    Довідники.Користувачі_Pointer? current = Користувачі_Select.Current;
                    if (current != null)
                    {
                        Dictionary<string, object> Fields = current.Fields;
                        Користувачі_Записи Record = new Користувачі_Записи
                        {
                            ID = current.UnigueID.ToString(),
                            DeletionLabel = (bool)Fields["deletion_label"], /*Помітка на видалення*/
                            Код = Fields[Користувачі_Const.Код].ToString() ?? "",
                                Назва = Fields[Користувачі_Const.Назва].ToString() ?? "",
                                
                        };
                        (TreeIter Iter, TypeObjectChanged Type) = records[current.UnigueID.UGuid];
                        Store.SetValues(Iter, Record.ToArray());
                    }
                }
            }
        }

        public static async ValueTask LoadRecords(TreeView treeView, UnigueID? openFolder = null, 
          UnigueID? selectPointerItem = null, UnigueID? directoryPointerItem = null)
        {
            TreePath? /*FirstPath = null,*/ SelectPath = null, CurrentPath = null;
            UnigueID? unigueIDSelect = selectPointerItem ?? directoryPointerItem;
            ListStore Store = (ListStore)treeView.Model;
            
            Довідники.Користувачі_Select Користувачі_Select = new Довідники.Користувачі_Select();
            Користувачі_Select.QuerySelect.Field.AddRange(
            [
                /*Помітка на видалення*/ "deletion_label",
                /*Код*/ Довідники.Користувачі_Const.Код,
                /*Назва*/ Довідники.Користувачі_Const.Назва,
                
            ]);

            

            /* Where */
            var where = treeView.Data["Where"];
            if (where != null) Користувачі_Select.QuerySelect.Where = (List<Where>)where;

            Користувачі_Select.QuerySelect.Order.Add(
               Довідники.Користувачі_Const.Назва, SelectOrder.ASC);
            
            /* Pages */
            var pages = treeView.Data["Pages"];
            Сторінки.Налаштування? settingsPages = pages != null ? (Сторінки.Налаштування)pages : null;
            if (settingsPages != null)
                await ЗаповнитиСторінки(Користувачі_Select.SplitSelectToPages, settingsPages, Користувачі_Select.QuerySelect, unigueIDSelect);
            

            /* SELECT */
            await Користувачі_Select.Select();
            Store.Clear();

            

            string? uidSelect = unigueIDSelect?.ToString();
            while (Користувачі_Select.MoveNext())
            {
                Довідники.Користувачі_Pointer? current = Користувачі_Select.Current;
                
                if (current != null)
                {
                    Dictionary<string, object> Fields = current.Fields;
                    Користувачі_Записи Record = new Користувачі_Записи
                    {
                        ID = current.UnigueID.ToString(),
                        DeletionLabel = (bool)Fields["deletion_label"], /*Помітка на видалення*/
                        Код = Fields[Користувачі_Const.Код].ToString() ?? "",
                            Назва = Fields[Користувачі_Const.Назва].ToString() ?? "",
                            
                    };
                    
                        TreeIter CurrentIter = Store.AppendValues(Record.ToArray());
                      

                    CurrentPath = Store.GetPath(CurrentIter);
                    /*FirstPath ??= CurrentPath;*/
                    if (uidSelect != null && Record.ID == uidSelect) SelectPath = CurrentPath;
                }
            }
            
            if (SelectPath != null) treeView.SetCursor(SelectPath, treeView.Columns[0], false);
            
        }
    }
	    
    #endregion
    
}

namespace GeneratedCode.Документи.ТабличніСписки
{
    
    #region DOCUMENT "НовийДок"
    
      
    public class НовийДок_Записи : ТабличнийСписок
    {
        bool DeletionLabel = false;
        bool Spend = false;
        string ID = "";
        
        string Назва = "";
        string НомерДок = "";
        string ДатаДок = "";
        string Коментар = "";

        object[] ToArray()
        {
            return
            [ 
                DeletionLabel ? InterfaceGtk3.Іконки.ДляТабличногоСписку.Delete : InterfaceGtk3.Іконки.ДляТабличногоСписку.Normal,
                ID, 
                /*Проведений документ*/ Spend, 
                /*Назва*/ Назва,
                /*НомерДок*/ НомерДок,
                /*ДатаДок*/ ДатаДок,
                /*Коментар*/ Коментар,
                
            ];
        }

        public static void AddColumns(TreeView treeView)
        {
            treeView.Model = new ListStore(
            [
                /*Image*/ typeof(Gdk.Pixbuf), 
                /*ID*/ typeof(string), 
                /*Spend Проведений документ*/ typeof(bool),
                /*Назва*/ typeof(string),  
                /*НомерДок*/ typeof(string),  
                /*ДатаДок*/ typeof(string),  
                /*Коментар*/ typeof(string),  
                
            ]);

            treeView.AppendColumn(new TreeViewColumn("", new CellRendererPixbuf(), "pixbuf", 0)); /*Image*/ /* { Ypad = 0 } */
            treeView.AppendColumn(new TreeViewColumn("ID", new CellRendererText(), "text", 1) { Visible = false }); /*UID*/
            treeView.AppendColumn(new TreeViewColumn("", new CellRendererToggle(), "active", 2)); /*Проведений документ*/
            /* */
            treeView.AppendColumn(new TreeViewColumn("Назва", new CellRendererText() { Xpad = 4 }, "text", 3) { MinWidth = 20, Resizable = true } ); /*Назва*/
            treeView.AppendColumn(new TreeViewColumn("Номер", new CellRendererText() { Xpad = 4 }, "text", 4) { MinWidth = 20, Resizable = true } ); /*НомерДок*/
            treeView.AppendColumn(new TreeViewColumn("Дата", new CellRendererText() { Xpad = 4 }, "text", 5) { MinWidth = 20, Resizable = true } ); /*ДатаДок*/
            treeView.AppendColumn(new TreeViewColumn("Коментар", new CellRendererText() { Xpad = 4 }, "text", 6) { MinWidth = 20, Resizable = true } ); /*Коментар*/
            
            //Пустишка
            treeView.AppendColumn(new TreeViewColumn());
        }

        public static void ДодатиВідбірПоПеріоду(TreeView treeView, ПеріодДляЖурналу.ТипПеріоду типПеріоду, DateTime? start = null, DateTime? stop = null)
        {
            ОчиститиВідбір(treeView);
            Where? where = ПеріодДляЖурналу.ВідбірПоПеріоду(Документи.НовийДок_Const.ДатаДок, типПеріоду, start, stop);
            if (where != null) ДодатиВідбір(treeView, where);
        }

        public static void CreateFilter(TreeView treeView, ListFilterControl filterControl)
        {
          
        }

        public static async ValueTask UpdateRecords(TreeView treeView, List<ObjectChanged> recordsChanged)
        {
            ListStore Store = (ListStore)treeView.Model;
            Dictionary<Guid, (TreeIter Iter, TypeObjectChanged Type)> records = [];

            //Update
            List<ObjectChanged> recordsChangedUpdate = [.. recordsChanged.Where(x => x.Type == TypeObjectChanged.Update)];
            if (Store.GetIterFirst(out TreeIter iter)) 
                do
                {
                    Guid uid = Guid.Parse((string)Store.GetValue(iter, 1));
                    if (recordsChangedUpdate.Any(x => x.Uid == uid)) records.Add(uid, (iter, TypeObjectChanged.Update));
                }
                while (Store.IterNext(ref iter));

            if (records.Count > 0)
            {
                Документи.НовийДок_Select НовийДок_Select = new Документи.НовийДок_Select();
                НовийДок_Select.QuerySelect.Field.AddRange(
                [
                    /*Помітка на видалення*/ "deletion_label",
                    /*Проведений документ*/ "spend",
                    /*Назва*/ Документи.НовийДок_Const.Назва,
                    /*НомерДок*/ Документи.НовийДок_Const.НомерДок,
                    /*ДатаДок*/ Документи.НовийДок_Const.ДатаДок,
                    /*Коментар*/ Документи.НовийДок_Const.Коментар,
                    
                ]);

                НовийДок_Select.QuerySelect.Where.Add(new Where("uid", Comparison.IN, "'" + string.Join("', '", records.Select(x => x.Key)) + "'", true));

                

                /* SELECT */
                await НовийДок_Select.Select();
                
                while (НовийДок_Select.MoveNext())
                {
                    Документи.НовийДок_Pointer? current = НовийДок_Select.Current;
                    if (current != null)
                    {
                        Dictionary<string, object> Fields = current.Fields;
                        НовийДок_Записи Record = new НовийДок_Записи
                        {
                            ID = current.UnigueID.ToString(),
                            Spend = (bool)Fields["spend"], /*Проведений документ*/
                            DeletionLabel = (bool)Fields["deletion_label"], /*Помітка на видалення*/
                            Назва = Fields[НовийДок_Const.Назва].ToString() ?? "",
                                НомерДок = Fields[НовийДок_Const.НомерДок].ToString() ?? "",
                                ДатаДок = Fields[НовийДок_Const.ДатаДок].ToString() ?? "",
                                Коментар = Fields[НовийДок_Const.Коментар].ToString() ?? "",
                                
                        };
                        (TreeIter Iter, TypeObjectChanged Type) = records[current.UnigueID.UGuid];
                        Store.SetValues(Iter, Record.ToArray());
                    }
                }
            }
        }

        public static async ValueTask LoadRecords(TreeView treeView, UnigueID? selectPointerItem = null, UnigueID? directoryPointerItem = null)
        {
            TreePath? /*FirstPath = null,*/ SelectPath = null, CurrentPath = null;
            UnigueID? unigueIDSelect = selectPointerItem ?? directoryPointerItem;
            ListStore Store = (ListStore)treeView.Model;

            Документи.НовийДок_Select НовийДок_Select = new Документи.НовийДок_Select();
            НовийДок_Select.QuerySelect.Field.AddRange(
            [
                /*Помітка на видалення*/ "deletion_label",
                /*Проведений документ*/ "spend",
                /*Назва*/ Документи.НовийДок_Const.Назва,
                /*НомерДок*/ Документи.НовийДок_Const.НомерДок,
                /*ДатаДок*/ Документи.НовийДок_Const.ДатаДок,
                /*Коментар*/ Документи.НовийДок_Const.Коментар,
                
            ]);

            

            /* Where */
            var where = treeView.Data["Where"];
            if (where != null) НовийДок_Select.QuerySelect.Where = (List<Where>)where;

            НовийДок_Select.QuerySelect.Order.Add(
               Документи.НовийДок_Const.ДатаДок, SelectOrder.ASC);
            

            /* Pages */
            var pages = treeView.Data["Pages"];
            Сторінки.Налаштування? settingsPages = pages != null ? (Сторінки.Налаштування)pages : null;
            if (settingsPages != null)
                await ЗаповнитиСторінки(НовийДок_Select.SplitSelectToPages, settingsPages, НовийДок_Select.QuerySelect, unigueIDSelect);

            /* SELECT */
            await НовийДок_Select.Select();
            Store.Clear();

            string? uidSelect = unigueIDSelect?.ToString();
            while (НовийДок_Select.MoveNext())
            {
                Документи.НовийДок_Pointer? current = НовийДок_Select.Current;

                if (current != null)
                {
                    Dictionary<string, object> Fields = current.Fields;
                    НовийДок_Записи Record = new НовийДок_Записи
                    {
                        ID = current.UnigueID.ToString(),
                        Spend = (bool)Fields["spend"], /*Проведений документ*/
                        DeletionLabel = (bool)Fields["deletion_label"], /*Помітка на видалення*/
                        Назва = Fields[НовийДок_Const.Назва].ToString() ?? "",
                            НомерДок = Fields[НовийДок_Const.НомерДок].ToString() ?? "",
                            ДатаДок = Fields[НовийДок_Const.ДатаДок].ToString() ?? "",
                            Коментар = Fields[НовийДок_Const.Коментар].ToString() ?? "",
                            
                    };

                    TreeIter CurrentIter = Store.AppendValues(Record.ToArray());
                    CurrentPath = Store.GetPath(CurrentIter);
                    /*FirstPath ??= CurrentPath;*/
                    if (uidSelect != null && Record.ID == uidSelect) SelectPath = CurrentPath;
                }
            }
            if (SelectPath != null)
                treeView.SetCursor(SelectPath, treeView.Columns[0], false);
            else if (CurrentPath != null && settingsPages != null && settingsPages.CurrentPage == settingsPages.Record.Pages) //Для останньої сторінки
                treeView.SetCursor(CurrentPath, treeView.Columns[0], false);
        }
    }
	    
    #endregion
    

    //
    // Журнали
    //

    
    #region JOURNAL "Повний"
    
    public class Журнали_Повний : ТабличнийСписок
    {
        bool DeletionLabel = false;
        bool Spend = false;
        string ID = "";
        string Type = ""; //Тип документу
        
        string Назва = "";
        string Дата = "";
        string Номер = "";
        string Коментар = "";

        // Масив для запису стрічки в Store
        object[] ToArray()
        {
            return 
            [
                DeletionLabel ? InterfaceGtk3.Іконки.ДляТабличногоСписку.Delete : InterfaceGtk3.Іконки.ДляТабличногоСписку.Normal, 
                ID, 
                Type, 
                /*Проведений документ*/ Spend,
                /*Назва*/ Назва,
                /*Дата*/ Дата,
                /*Номер*/ Номер,
                /*Коментар*/ Коментар,
                 
            ];
        }

        // Добавлення колонок в список
        public static void AddColumns(TreeView treeView)
        {
            treeView.Model = new ListStore(
            [
                typeof(Gdk.Pixbuf), /* Image */
                typeof(string), /* ID */
                typeof(string), /* Type */
                typeof(bool), /* Spend Проведений документ */
                typeof(string), /*Назва*/
                typeof(string), /*Дата*/
                typeof(string), /*Номер*/
                typeof(string), /*Коментар*/
                
            ]);

            treeView.AppendColumn(new TreeViewColumn("", new CellRendererPixbuf(), "pixbuf", 0)); /*Image*/ /* { Ypad = 0 } */
            treeView.AppendColumn(new TreeViewColumn("ID", new CellRendererText(), "text", 1) { Visible = false }); /*UID*/
            treeView.AppendColumn(new TreeViewColumn("Type", new CellRendererText(), "text", 2) { Visible = false }); /*Type*/
            treeView.AppendColumn(new TreeViewColumn("", new CellRendererToggle(), "active", 3)); /*Проведений документ*/
            /* */
            treeView.AppendColumn(new TreeViewColumn("Назва", new CellRendererText() { Xpad = 4 }, "text", 4) { MinWidth = 20, Resizable = true } ); /*Назва*/
            treeView.AppendColumn(new TreeViewColumn("Дата", new CellRendererText() { Xpad = 4 }, "text", 5) { MinWidth = 20, Resizable = true } ); /*Дата*/
            treeView.AppendColumn(new TreeViewColumn("Номер", new CellRendererText() { Xpad = 4 }, "text", 6) { MinWidth = 20, Resizable = true } ); /*Номер*/
            treeView.AppendColumn(new TreeViewColumn("Коментар", new CellRendererText() { Xpad = 4 }, "text", 7) { MinWidth = 20, Resizable = true } ); /*Коментар*/
            
            //Пустишка
            treeView.AppendColumn(new TreeViewColumn());
        }

        public static void ДодатиВідбірПоПеріоду(TreeView treeView, ПеріодДляЖурналу.ТипПеріоду типПеріоду, DateTime? start = null, DateTime? stop = null)
        {
            Dictionary<string, List<Where>> WhereDict = [];
            if (!treeView.Data.ContainsKey("Where"))
                treeView.Data.Add("Where", WhereDict);
            else
                treeView.Data["Where"] = WhereDict;
            
            {
                Where? where = ПеріодДляЖурналу.ВідбірПоПеріоду(Документи.НовийДок_Const.ДатаДок, типПеріоду, start, stop);
                if (where != null) WhereDict.Add("НовийДок", [where]);
            }
              
        }

        // Список документів які входять в журнал
        public static Dictionary<string, string> AllowDocument()
        {
            return new Dictionary<string, string>()
            {
                {"НовийДок", "НовийДок"},
                
            };
        }

        // Завантаження даних
        public static async ValueTask LoadRecords(TreeView treeView, UnigueID? selectPointerItem = null) 
        {
            TreePath? SelectPath = null, CurrentPath = null;
            ListStore Store = (ListStore)treeView.Model;

            List<string> allQuery = [];
            Dictionary<string, object> paramQuery = [];

          
              //Документ: НовийДок
              {
                  Query query = new Query(Документи.НовийДок_Const.TABLE);

                  // Встановлення відбору
                  var dataWhere = treeView.Data["Where"];
                  if (dataWhere != null)
                  {
                      var dictWhere = (Dictionary<string, List<Where>>)dataWhere;
                      if (dictWhere.TryGetValue("НовийДок", out List<Where>? listWhere))
                      {
                          query.Where = listWhere;
                          foreach(Where where in listWhere)
                              paramQuery.Add(where.Alias, where.Value);
                      }
                  }
                  
                  query.FieldAndAlias.Add(new ValueName<string>("'НовийДок'", "type"));
                  query.Field.Add("deletion_label");
                  query.Field.Add("spend");
                  
                              query.FieldAndAlias.Add(new ValueName<string>(Документи.НовийДок_Const.TABLE + "." + Документи.НовийДок_Const.Назва, "Назва"));
                            
                              query.FieldAndAlias.Add(new ValueName<string>(Документи.НовийДок_Const.TABLE + "." + Документи.НовийДок_Const.ДатаДок, "Дата"));
                            
                              query.FieldAndAlias.Add(new ValueName<string>(Документи.НовийДок_Const.TABLE + "." + Документи.НовийДок_Const.НомерДок, "Номер"));
                            
                              query.FieldAndAlias.Add(new ValueName<string>(Документи.НовийДок_Const.TABLE + "." + Документи.НовийДок_Const.Коментар, "Коментар"));
                            
                  allQuery.Add(query.Construct());
              }
              

            string unionAllQuery = string.Join("\nUNION\n", allQuery);

            unionAllQuery += "\nORDER BY Дата";

            /* Pages */
            var pages = treeView.Data["Pages"];
            Сторінки.Налаштування? settingsPages = pages != null ? (Сторінки.Налаштування)pages : null;
            if (settingsPages != null)
               unionAllQuery = await ЗаповнитиСторінки(Config.Kernel.DataBase.SplitSelectToPagesForJournal, settingsPages, unionAllQuery, paramQuery);

            var recordResult = await Config.Kernel.DataBase.SelectRequest(unionAllQuery, paramQuery);
            Store.Clear();

            string? uidSelect = selectPointerItem?.ToString();
            foreach (Dictionary<string, object> row in recordResult.ListRow)
            {
                Журнали_Повний record = new Журнали_Повний
                {
                    ID = row["uid"].ToString() ?? "",
                    Type = row["type"].ToString() ?? "",
                    DeletionLabel = (bool)row["deletion_label"],
                    Spend = (bool)row["spend"],
                    Назва = row["Назва"] != DBNull.Value ? (row["Назва"].ToString() ?? "") : "",
                    Дата = row["Дата"] != DBNull.Value ? (row["Дата"].ToString() ?? "") : "",
                    Номер = row["Номер"] != DBNull.Value ? (row["Номер"].ToString() ?? "") : "",
                    Коментар = row["Коментар"] != DBNull.Value ? (row["Коментар"].ToString() ?? "") : "",
                    
                };

                TreeIter CurrentIter = Store.AppendValues(record.ToArray());
                CurrentPath = Store.GetPath(CurrentIter);
                if (uidSelect != null && record.ID == uidSelect) SelectPath = CurrentPath;
            }
            if (SelectPath != null)
                treeView.SetCursor(SelectPath, treeView.Columns[0], false);
            else if (CurrentPath != null && settingsPages != null && settingsPages.CurrentPage == settingsPages.Record.Pages) //Для останньої сторінки
                treeView.SetCursor(CurrentPath, treeView.Columns[0], false);
          
        }
    }
    #endregion
    
}

namespace GeneratedCode.РегістриВідомостей.ТабличніСписки
{
    
}

namespace GeneratedCode.РегістриНакопичення.ТабличніСписки
{
    
    #region REGISTER "РегНакопичення"
    
      
    public class РегНакопичення_Записи : ТабличнийСписок
    {
        string ID = "";
        bool Income = false;
        string Period = "";
        string OwnerName = "";
        
        string Користувач = "";
        string Сума = "";

        object[] ToArray()
        {
            return
            [
                InterfaceGtk3.Іконки.ДляТабличногоСписку.Normal, 
                ID, 
                Income ? "+" : "-", 
                Period, 
                OwnerName,
                /*Користувач*/ Користувач,
                /*Сума*/ Сума,
                 
            ];
        }

        public static void AddColumns(TreeView treeView, string[]? hiddenColumn = null)
        {
            treeView.Model = new ListStore(
            [
                /*Image*/ typeof(Gdk.Pixbuf), 
                /*ID*/ typeof(string), 
                /*Income*/ typeof(string), 
                /*Period*/ typeof(string),
                /*OwnerName*/ typeof(string),
                /*Користувач*/ typeof(string),
                /*Сума*/ typeof(string),
                
            ]);

            bool IsHiddenColumn(string column){ return hiddenColumn != null ? !hiddenColumn.Contains(column) : true; }

            treeView.AppendColumn(new TreeViewColumn("", new CellRendererPixbuf(), "pixbuf", 0)); /* { Ypad = 0 } */
            treeView.AppendColumn(new TreeViewColumn("ID", new CellRendererText(), "text", 1) { Visible = false });
            treeView.AppendColumn(new TreeViewColumn("Рух", new CellRendererText() { Xalign = 0.5f }, "text", 2) { Visible = IsHiddenColumn("income") });
            treeView.AppendColumn(new TreeViewColumn("Період", new CellRendererText(), "text", 3) { Visible = IsHiddenColumn("period") });
            treeView.AppendColumn(new TreeViewColumn("Регістратор", new CellRendererText(), "text", 4) { Visible = IsHiddenColumn("owner") });
            /* */
            treeView.AppendColumn(new TreeViewColumn("Користувач", new CellRendererText() { Xpad = 4 }, "text", 5) { MinWidth = 20, Resizable = true, SortColumnId = 5 } ); /*Користувач*/
            treeView.AppendColumn(new TreeViewColumn("Сума", new CellRendererText() { Xpad = 4 }, "text", 6) { MinWidth = 20, Resizable = true, SortColumnId = 6 } ); /*Сума*/
            
            //Пустишка
            treeView.AppendColumn(new TreeViewColumn());
        }

        public static void ДодатиВідбірПоПеріоду(TreeView treeView, ПеріодДляЖурналу.ТипПеріоду типПеріоду, DateTime? start = null, DateTime? stop = null)
        {
            ОчиститиВідбір(treeView);
            Where? where = ПеріодДляЖурналу.ВідбірПоПеріоду("period", типПеріоду, start, stop);
            if (where != null) ДодатиВідбір(treeView, where);               
        }

        public static void ДодатиВідбірПоДокументу(TreeView treeView, Guid owner)
        {
            ДодатиВідбір(treeView, new Where("owner", Comparison.EQ, owner), true);
        }

        public static async ValueTask LoadRecords(TreeView treeView, UnigueID? selectPointerItem = null, bool docname_required = true, bool position_last = true)
        {
            TreePath? SelectPath = null, CurrentPath = null;
            ListStore Store = (ListStore)treeView.Model;

            РегістриНакопичення.РегНакопичення_RecordsSet РегНакопичення_RecordsSet = new РегістриНакопичення.РегНакопичення_RecordsSet();
            РегНакопичення_RecordsSet.FillJoin(["period"], docname_required);

            /* Where */
            var where = treeView.Data["Where"];
            if (where != null) РегНакопичення_RecordsSet.QuerySelect.Where = (List<Where>)where;

            

            /* Pages */
            var pages = treeView.Data["Pages"];
            Сторінки.Налаштування? settingsPages = pages != null ? (Сторінки.Налаштування)pages : null;
            if (settingsPages != null)
                await ЗаповнитиСторінки(РегНакопичення_RecordsSet.SplitSelectToPages, settingsPages, РегНакопичення_RecordsSet.QuerySelect, selectPointerItem);
                
            /* Read */
            await РегНакопичення_RecordsSet.Read();
            Store.Clear();

            string? uidSelect = selectPointerItem?.ToString();
            foreach (РегНакопичення_RecordsSet.Record record in РегНакопичення_RecordsSet.Records)
            {
                РегНакопичення_Записи row = new РегНакопичення_Записи
                {
                    ID = record.UID.ToString(),
                    Period = record.Period.ToString(),
                    Income = record.Income,
                    OwnerName = record.OwnerName,
                    Користувач = record.Користувач.Назва,
                        Сума = record.Сума.ToString() ?? "",
                        
                };

                TreeIter CurrentIter = Store.AppendValues(row.ToArray());
                CurrentPath = Store.GetPath(CurrentIter);
                if (uidSelect != null && row.ID == uidSelect) SelectPath = CurrentPath;
            }
            if (position_last)
            {
                if (SelectPath != null)
                    treeView.SetCursor(SelectPath, treeView.Columns[0], false);
                /*else if (CurrentPath != null && settingsPages != null && settingsPages.CurrentPage == settingsPages.Record.Pages) //Для останньої сторінки
                treeView.SetCursor(CurrentPath, treeView.Columns[0], false);*/
            }
        }
    }
	    
    #endregion
    
}

  