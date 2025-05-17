
/*
 *
 * Конфігурації "Новий проєкт"
 * Автор 
  
 * Дата конфігурації: 17.05.2025 10:13:37
 *
 *
 * Цей код згенерований в Конфігураторі 3. Шаблон GeneratedCode.xslt
 *
 */

using AccountingSoftware;
using System.Xml;

namespace GeneratedCode
{
    public static class Config
    {
        #region Const

        //Простір імен згенерованого коду
        public const string NameSpageCodeGeneration = "GeneratedCode";

        //Простір імен програми
        public const string NameSpageProgram = "NewProject";

        #endregion
        
        public static Kernel Kernel { get; set; } = new Kernel();
        public static async void StartBackgroundTask()
        {
            /*
            Схема роботи:

            1. В процесі запису в регістр залишків - додається запис у таблицю тригерів.
              Запис в таблицю тригерів містить дату запису в регістр, назву регістру.

            2. Раз на 5 сек викликається процедура SpetialTableRegAccumTrigerExecute і
              відбувається розрахунок віртуальних таблиць регістрів залишків.

              Розраховуються тільки змінені регістри на дату проведення документу і
              додатково на дату якщо змінена дата документу і документ уже був проведений.

              Додатково розраховуються підсумки в кінці всіх розрахунків.
            */

            if (Kernel.Session == Guid.Empty)
                throw new Exception("Порожня сесія користувача. Спочатку потрібно залогінитись, а тоді вже викликати функцію StartBackgroundTask()");

            while (true)
            {
                //Виконання обчислень
                await Kernel.DataBase.SpetialTableRegAccumTrigerExecute
                (
                    Kernel.Session,
                    РегістриНакопичення.VirtualTablesСalculation.Execute, 
                    РегістриНакопичення.VirtualTablesСalculation.ExecuteFinalCalculation
                );

                //Затримка на 5 сек
                await Task.Delay(5000);
            }
        }
    }

    public class Functions
    {
        /*
          Функція для типу який задається користувачем.
          Повертає презентацію для uuidAndText
        */
        public static async ValueTask<CompositePointerPresentation_Record> CompositePointerPresentation(UuidAndText uuidAndText)
        {
            CompositePointerPresentation_Record record = new();

            (bool result, string pointerGroup, string pointerType) = Configuration.PointerParse(uuidAndText.Text, out Exception? _);
            if (result)
            {
                record.pointer = pointerGroup;
                record.type = pointerType;

                if (!uuidAndText.IsEmpty())
                    if (record.pointer == "Довідники") 
                    {
                        record.result = record.type switch
                        {
                        "Користувачі" => await new Довідники.Користувачі_Pointer(uuidAndText.Uuid).GetPresentation(),
                        _ => ""
                        };
                    }
                    else if (record.pointer == "Документи") 
                    {
                        record.result = record.type switch
                        {
                        "НовийДок" => await new Документи.НовийДок_Pointer(uuidAndText.Uuid).GetPresentation(),
                        _ => ""
                        };
                    }
            }
            return record;
        }
    }
}

namespace GeneratedCode.Константи
{
    
	  #region CONSTANTS BLOCK "Системні"
    public static class Системні
    {       
        public static string НалаштуванняКористувача_Const
        {
            get 
            {
                var recordResult = Task.Run( async () => { return await Config.Kernel.DataBase.SelectConstants(SpecialTables.Constants, "col_i6"); } ).Result;
                return recordResult.Result ? (recordResult.Value.ToString() ?? "") : "";
            }
            set
            {
                Config.Kernel.DataBase.SaveConstants(SpecialTables.Constants, "col_i6", value);
            }
        }
        
        
        public class НалаштуванняКористувача_ПеріодиЖурналів_TablePart : ConstantsTablePart
        {
            public НалаштуванняКористувача_ПеріодиЖурналів_TablePart() : base(Config.Kernel, "tab_a80",
                 ["col_a1", "col_a2", "col_a3", "col_a4", "col_a5", ]) 
            { 
              
            }
            
            public const string TABLE = "tab_a80";
            
            public const string Користувач = "col_a1";
            public const string Журнал = "col_a2";
            public const string ПеріодЗначення = "col_a3";
            public const string ДатаСтарт = "col_a4";
            public const string ДатаСтоп = "col_a5";
            public List<Record> Records { get; set; } = [];

            public event EventHandler? Saved;
        
            public async ValueTask Read()
            {
                Records.Clear();
                await base.BaseRead();

                foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
                {
                    Record record = new Record()
                    {
                        UID = (Guid)fieldValue["uid"],
                        Користувач = new Довідники.Користувачі_Pointer(fieldValue["col_a1"]),
                        Журнал = fieldValue["col_a2"].ToString() ?? "",
                        ПеріодЗначення = fieldValue["col_a3"].ToString() ?? "",
                        ДатаСтарт = (fieldValue["col_a4"] != DBNull.Value) ? DateTime.Parse(fieldValue["col_a4"].ToString() ?? DateTime.MinValue.ToString()) : DateTime.MinValue,
                        ДатаСтоп = (fieldValue["col_a5"] != DBNull.Value) ? DateTime.Parse(fieldValue["col_a5"].ToString() ?? DateTime.MinValue.ToString()) : DateTime.MinValue,
                        
                    };
                    Records.Add(record);
                }
            
                base.BaseClear();
            }
        
            public async ValueTask Save(bool clear_all_before_save /*= true*/) 
            {
                await base.BaseBeginTransaction();
                
                if (clear_all_before_save)
                    await base.BaseDelete();

                

                foreach (Record record in Records)
                {
                    
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>()
                    {
                        {"col_a1", record.Користувач.UnigueID.UGuid},
                        {"col_a2", record.Журнал},
                        {"col_a3", record.ПеріодЗначення},
                        {"col_a4", record.ДатаСтарт},
                        {"col_a5", record.ДатаСтоп},
                        
                    };
                    record.UID = await base.BaseSave(record.UID, fieldValue);
                }
                
                await base.BaseCommitTransaction();
                Saved?.Invoke(this, new EventArgs());
            }

            public async ValueTask Remove(Record record)
            {
                await base.BaseRemove(record.UID);
                Records.RemoveAll(item => record.UID == item.UID);
            }

            public async ValueTask RemoveAll(List<Record> records)
            {
                List<Guid> removeList = [];

                await base.BaseBeginTransaction();
                foreach (Record record in records)
                {
                    removeList.Add(record.UID);
                    await base.BaseRemove(record.UID);
                }
                await base.BaseCommitTransaction();

                Records.RemoveAll(item => removeList.Exists(uid => uid == item.UID));
            }
        
            public async ValueTask Delete()
            {
                await base.BaseDelete();
            }
            
            public class Record : ConstantsTablePartRecord
            {
                public Довідники.Користувачі_Pointer Користувач { get; set; } = new Довідники.Користувачі_Pointer();
                public string Журнал { get; set; } = "";
                public string ПеріодЗначення { get; set; } = "";
                public DateTime ДатаСтарт { get; set; } = DateTime.MinValue;
                public DateTime ДатаСтоп { get; set; } = DateTime.MinValue;
                
            }
        }
               
    }
    #endregion
    
	  #region CONSTANTS BLOCK "НумераціяДовідників"
    public static class НумераціяДовідників
    {       
        public static int Користувачі_Const
        {
            get 
            {
                var recordResult = Task.Run( async () => { return await Config.Kernel.DataBase.SelectConstants(SpecialTables.Constants, "col_a1"); } ).Result;
                return recordResult.Result ? ((recordResult.Value != DBNull.Value) ? (int)recordResult.Value : 0) : 0;
            }
            set
            {
                Config.Kernel.DataBase.SaveConstants(SpecialTables.Constants, "col_a1", value);
            }
        }
             
    }
    #endregion
    
	  #region CONSTANTS BLOCK "НумераціяДокументів"
    public static class НумераціяДокументів
    {       
        public static int НовийДок_Const
        {
            get 
            {
                var recordResult = Task.Run( async () => { return await Config.Kernel.DataBase.SelectConstants(SpecialTables.Constants, "col_a2"); } ).Result;
                return recordResult.Result ? ((recordResult.Value != DBNull.Value) ? (int)recordResult.Value : 0) : 0;
            }
            set
            {
                Config.Kernel.DataBase.SaveConstants(SpecialTables.Constants, "col_a2", value);
            }
        }
             
    }
    #endregion
    
}

namespace GeneratedCode.Довідники
{
    
    #region DIRECTORY "Користувачі"
    public static class Користувачі_Const
    {
        public const string TABLE = "tab_a14";
        public const string TYPE = "Користувачі"; /* Назва вказівника */
        public const string POINTER = "Довідники.Користувачі"; /* Повна назва вказівника */
        public const string FULLNAME = "Користувачі"; /* Повна назва об'єкта */
        public const string DELETION_LABEL = "deletion_label"; /* Помітка на видалення true|false */
        public readonly static string[] PRESENTATION_FIELDS = ["col_f6", ];
        
        public const string Назва = "col_f6";
        public const string Код = "col_f7";
        public const string Коментар = "col_g6";
        public const string КодВСпеціальнійТаблиці = "col_a2";
    }

    public class Користувачі_Objest : DirectoryObject
    {
        public event EventHandler<UnigueID>? UnigueIDChanged;
        public event EventHandler<string>? CaptionChanged;

        public Користувачі_Objest() : base(Config.Kernel, "tab_a14", Користувачі_Const.TYPE,
             ["col_f6", "col_f7", "col_g6", "col_a2", ])
        {
            
                //Табличні частини
                Контакти_TablePart = new Користувачі_Контакти_TablePart(this);
                
        }
        
        public async ValueTask New()
        {
            BaseNew();
            UnigueIDChanged?.Invoke(this, base.UnigueID);
            CaptionChanged?.Invoke(this, Користувачі_Const.FULLNAME + " *");
            
                await Користувачі_Triggers.New(this);
              
        }

        public async ValueTask<bool> Read(UnigueID uid, bool readAllTablePart = false)
        {
            if (await BaseRead(uid))
            {
                Назва = base.FieldValue["col_f6"].ToString() ?? "";
                Код = base.FieldValue["col_f7"].ToString() ?? "";
                Коментар = base.FieldValue["col_g6"].ToString() ?? "";
                КодВСпеціальнійТаблиці = (base.FieldValue["col_a2"] != DBNull.Value) ? (Guid)base.FieldValue["col_a2"] : Guid.Empty;
                
                BaseClear();
                
                if (readAllTablePart)
                {
                    
                    await Контакти_TablePart.Read();
                }
                
                UnigueIDChanged?.Invoke(this, base.UnigueID);
                CaptionChanged?.Invoke(this, string.Join(", ", [Назва, ]));
                return true;
            }
            else
                return false;
        }
        
        public async ValueTask<bool> Save()
        {
            base.FieldValue["col_f6"] = Назва;
            base.FieldValue["col_f7"] = Код;
            base.FieldValue["col_g6"] = Коментар;
            base.FieldValue["col_a2"] = КодВСпеціальнійТаблиці;
            
            bool result = await BaseSave();
            if (result)
            {
                
                await BaseWriteFullTextSearch(GetBasis(), [Назва, Коментар, ]);
                
            }
            CaptionChanged?.Invoke(this, string.Join(", ", [Назва, ]));
            return result;
        }

        public async ValueTask<Користувачі_Objest> Copy(bool copyTableParts = false)
        {
            Користувачі_Objest copy = new Користувачі_Objest()
            {
                Назва = Назва,
                Код = Код,
                Коментар = Коментар,
                КодВСпеціальнійТаблиці = КодВСпеціальнійТаблиці,
                
            };
            
            if (copyTableParts)
            {
            
                //Контакти - Таблична частина
                await Контакти_TablePart.Read();
                copy.Контакти_TablePart.Records = Контакти_TablePart.Copy();
            
            }
            

            await copy.New();
            
            await Користувачі_Triggers.Copying(copy, this);      
            
            return copy;
        }

        public async ValueTask SetDeletionLabel(bool label = true)
        {
            
            await base.BaseDeletionLabel(label);
        }

        public async ValueTask Delete()
        {
            
            await base.BaseDelete(["tab_a15", ]);
        }
        
        public Користувачі_Pointer GetDirectoryPointer()
        {
            return new Користувачі_Pointer(UnigueID.UGuid);
        }

        public async ValueTask<string> GetPresentation()
        {
            return await base.BasePresentation(Користувачі_Const.PRESENTATION_FIELDS);
        }
                
        public string Назва { get; set; } = "";
        public string Код { get; set; } = "";
        public string Коментар { get; set; } = "";
        public Guid КодВСпеціальнійТаблиці { get; set; } = new Guid();
        
        //Табличні частини
        public Користувачі_Контакти_TablePart Контакти_TablePart { get; private set; }
        
    }

    public class Користувачі_Pointer : DirectoryPointer
    {
        public Користувачі_Pointer(object? uid = null) : base(Config.Kernel, "tab_a14", Користувачі_Const.TYPE)
        {
            base.Init(new UnigueID(uid));
        }
        
        public Користувачі_Pointer(UnigueID uid, Dictionary<string, object>? fields = null) : base(Config.Kernel, "tab_a14", Користувачі_Const.TYPE)
        {
            base.Init(uid, fields);
        }
        
        public async ValueTask<Користувачі_Objest?> GetDirectoryObject(bool readAllTablePart = false)
        {
            if (this.IsEmpty()) return null;
            Користувачі_Objest obj = new Користувачі_Objest();
            return await obj.Read(base.UnigueID, readAllTablePart) ? obj : null;
        }

        public Користувачі_Pointer Copy()
        {
            return new Користувачі_Pointer(base.UnigueID, base.Fields) { Name = Name };
        }

        public string Назва
        {
            get { return Name; } set { Name = value; }
        }

        public async ValueTask<string> GetPresentation()
        {
            return Name = await base.BasePresentation(Користувачі_Const.PRESENTATION_FIELDS);
        }

        public static void GetJoin(Query querySelect, string joinField, string parentTable, string joinTableAlias, string fieldAlias)
        {
            string[] presentationField = new string [Користувачі_Const.PRESENTATION_FIELDS.Length];
            for (int i = 0; i < presentationField.Length; i++) presentationField[i] = $"{joinTableAlias}.{Користувачі_Const.PRESENTATION_FIELDS[i]}";
            querySelect.Joins.Add(new Join(Користувачі_Const.TABLE, joinField, parentTable, joinTableAlias));
            querySelect.FieldAndAlias.Add(new ValueName<string>(presentationField.Length switch { 1 => presentationField[0], >1 => $"concat_ws (', ', " + string.Join(", ", presentationField) + ")", _ => "'#'" }, fieldAlias));
        }

        public async ValueTask<bool?> GetDeletionLabel()
        {
            return await base.BaseGetDeletionLabel();
        }

        public async ValueTask SetDeletionLabel(bool label = true)
        {
            
            await base.BaseDeletionLabel(label);
        }
		
        public Користувачі_Pointer GetEmptyPointer()
        {
            return new Користувачі_Pointer();
        }
    }
    
    public class Користувачі_Select : DirectorySelect
    {
        public Користувачі_Select() : base(Config.Kernel, "tab_a14") { }        
        public async ValueTask<bool> Select() { return await base.BaseSelect(); }
        public async ValueTask<bool> SelectSingle() { if (await base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        public bool MoveNext() { if (base.MoveToPosition() && base.CurrentPointerPosition.HasValue) { Current = new Користувачі_Pointer(base.CurrentPointerPosition.Value.UnigueID, base.CurrentPointerPosition.Value.Fields); return true; } else { Current = null; return false; } }
        public Користувачі_Pointer? Current { get; private set; }
        
        public async ValueTask<Користувачі_Pointer> FindByField(string name, object value)
        {
            UnigueID? pointer = await base.BaseFindByField(name, value);
            return pointer != null ? new Користувачі_Pointer(pointer) : new Користувачі_Pointer();
        }
        
        public async ValueTask<List<Користувачі_Pointer>> FindListByField(string name, object value, int limit = 0, int offset = 0)
        {
            List<Користувачі_Pointer> directoryPointerList = [];
            foreach (var directoryPointer in await base.BaseFindListByField(name, value, limit, offset)) 
                directoryPointerList.Add(new Користувачі_Pointer(directoryPointer.UnigueID, directoryPointer.Fields));
            return directoryPointerList;
        }
    }

    
    
    public class Користувачі_Контакти_TablePart : DirectoryTablePart
    {
        public Користувачі_Контакти_TablePart(Користувачі_Objest owner) : base(Config.Kernel, "tab_a15",
             ["col_f8", "col_g5", "col_g4", "col_f9", "col_g1", "col_g2", "col_g3", "col_a1", ])
        {
            if (owner == null) throw new Exception("owner null");
            Owner = owner;
            
        }

        public const string TABLE = "tab_a15";
        
        public const string Тип = "col_f8";
        public const string Телефон = "col_g5";
        public const string ЕлектроннаПошта = "col_g4";
        public const string Країна = "col_f9";
        public const string Область = "col_g1";
        public const string Район = "col_g2";
        public const string Місто = "col_g3";
        public const string Значення = "col_a1";

        public Користувачі_Objest Owner { get; private set; }
        
        public List<Record> Records { get; set; } = [];

        public event EventHandler? Saved;
        
        public void FillJoin(string[]? orderFields = null)
        {
            QuerySelect.Clear();

            if (orderFields != null)
            {
              foreach(string field in orderFields)
                QuerySelect.Order.Add(field, SelectOrder.ASC);
            }
            
        }

        public async ValueTask Read()
        {
            Records.Clear();
            await base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Record record = new Record()
                {
                    UID = (Guid)fieldValue["uid"],
                    Тип = (fieldValue["col_f8"] != DBNull.Value) ? (Перелічення.ТипиКонтактноїІнформації)fieldValue["col_f8"] : 0,
                    Телефон = fieldValue["col_g5"].ToString() ?? "",
                    ЕлектроннаПошта = fieldValue["col_g4"].ToString() ?? "",
                    Країна = fieldValue["col_f9"].ToString() ?? "",
                    Область = fieldValue["col_g1"].ToString() ?? "",
                    Район = fieldValue["col_g2"].ToString() ?? "",
                    Місто = fieldValue["col_g3"].ToString() ?? "",
                    Значення = fieldValue["col_a1"].ToString() ?? "",
                    
                };
                Records.Add(record);
                
            }
            
            base.BaseClear();
        }
        
        public async ValueTask Save(bool clear_all_before_save) 
        {
            if (!await base.IsExistOwner(Owner.UnigueID, "tab_a14"))
                throw new Exception("Owner not exist");
            
                
            await base.BaseBeginTransaction();
            
            if (clear_all_before_save)
                await base.BaseDelete(Owner.UnigueID);
            
            
            foreach (Record record in Records)
            {
                
                Dictionary<string, object> fieldValue = new()
                {
                    {"col_f8", (int)record.Тип},
                    {"col_g5", record.Телефон},
                    {"col_g4", record.ЕлектроннаПошта},
                    {"col_f9", record.Країна},
                    {"col_g1", record.Область},
                    {"col_g2", record.Район},
                    {"col_g3", record.Місто},
                    {"col_a1", record.Значення},
                    
                };
                record.UID = await base.BaseSave(record.UID, Owner.UnigueID, fieldValue);
            }
                
            await base.BaseCommitTransaction();

            
            Saved?.Invoke(this, new EventArgs());
        }
        
        public List<Record> Copy()
        {
            List<Record> copyRecords = new(Records);
            foreach (Record copyRecordItem in Records)
                copyRecordItem.UID = Guid.Empty;

            return copyRecords;
        }
        
        public class Record : DirectoryTablePartRecord
        {
            public Перелічення.ТипиКонтактноїІнформації Тип { get; set; } = 0;
            public string Телефон { get; set; } = "";
            public string ЕлектроннаПошта { get; set; } = "";
            public string Країна { get; set; } = "";
            public string Область { get; set; } = "";
            public string Район { get; set; } = "";
            public string Місто { get; set; } = "";
            public string Значення { get; set; } = "";
            
        }
    }
      
   
    #endregion
    
}

namespace GeneratedCode.Перелічення
{
    
    #region ENUM "ТипиКонтактноїІнформації"
    public enum ТипиКонтактноїІнформації
    {
         Адрес = 1,
         Телефон = 2,
         ЕлектроннаПошта = 3,
         Сайт = 4,
         Інше = 5
    }
    #endregion
    

    public static class ПсевдонімиПерелічення
    {
    
        #region ENUM "ТипиКонтактноїІнформації"
        public static string ТипиКонтактноїІнформації_Alias(ТипиКонтактноїІнформації value)
        {
            return value switch
            {
                ТипиКонтактноїІнформації.Адрес => "Адрес",
                ТипиКонтактноїІнформації.Телефон => "Телефон",
                ТипиКонтактноїІнформації.ЕлектроннаПошта => "Електронна пошта",
                ТипиКонтактноїІнформації.Сайт => "Сайт",
                ТипиКонтактноїІнформації.Інше => "Інше",
                _ => ""
            };
        }

        public static ТипиКонтактноїІнформації ТипиКонтактноїІнформації_FindByName(string name)
        {
            return name switch
            {
                "Адрес" => ТипиКонтактноїІнформації.Адрес,
                  "Телефон" => ТипиКонтактноїІнформації.Телефон,
                  "ЕлектроннаПошта" => ТипиКонтактноїІнформації.ЕлектроннаПошта,
                  "Електронна пошта" => ТипиКонтактноїІнформації.ЕлектроннаПошта,
                  "Сайт" => ТипиКонтактноїІнформації.Сайт,
                  "Інше" => ТипиКонтактноїІнформації.Інше,
                  _ => 0
            };
        }

        public static List<NameValue<ТипиКонтактноїІнформації>> ТипиКонтактноїІнформації_List()
        {
            return [
            new NameValue<ТипиКонтактноїІнформації>("Адрес", ТипиКонтактноїІнформації.Адрес),
            new NameValue<ТипиКонтактноїІнформації>("Телефон", ТипиКонтактноїІнформації.Телефон),
            new NameValue<ТипиКонтактноїІнформації>("Електронна пошта", ТипиКонтактноїІнформації.ЕлектроннаПошта),
            new NameValue<ТипиКонтактноїІнформації>("Сайт", ТипиКонтактноїІнформації.Сайт),
            new NameValue<ТипиКонтактноїІнформації>("Інше", ТипиКонтактноїІнформації.Інше),
            ];
        }
        #endregion
    
    }
}

namespace GeneratedCode.Документи
{
    
    #region DOCUMENT "НовийДок"
    public static class НовийДок_Const
    {
        public const string TABLE = "tab_a01";
        public const string TYPE = "НовийДок"; /* Назва вказівника */
        public const string POINTER = "Документи.НовийДок"; /* Повна назва вказівника */
        public const string FULLNAME = "НовийДок"; /* Повна назва об'єкта */
        public const string DELETION_LABEL = "deletion_label"; /* Помітка на видалення true|false */
        public const string SPEND = "spend"; /* Проведений true|false */
        public const string SPEND_DATE = "spend_date"; /* Дата проведення DateTime */
        public readonly static string[] PRESENTATION_FIELDS = ["docname", ];
        
        
        public const string Назва = "docname";
        public const string НомерДок = "docnomer";
        public const string ДатаДок = "docdate";
        public const string Коментар = "col_a1";
    }

    public class НовийДок_Objest : DocumentObject
    {
        public event EventHandler<UnigueID>? UnigueIDChanged;
        public event EventHandler<string>? CaptionChanged;

        public НовийДок_Objest() : base(Config.Kernel, "tab_a01", НовийДок_Const.TYPE,
             ["docname", "docnomer", "docdate", "col_a1", ])
        {
            
                //Табличні частини
                Накопичення_TablePart = new НовийДок_Накопичення_TablePart(this);
                
        }
        
        public async ValueTask New()
        {
            BaseNew();
            UnigueIDChanged?.Invoke(this, base.UnigueID);
            CaptionChanged?.Invoke(this, НовийДок_Const.FULLNAME + " *");
            
                await НовийДок_Triggers.New(this);
              
        }

        public async ValueTask<bool> Read(UnigueID uid, bool readAllTablePart = false)
        {
            if (await BaseRead(uid))
            {
                Назва = base.FieldValue["docname"].ToString() ?? "";
                НомерДок = base.FieldValue["docnomer"].ToString() ?? "";
                ДатаДок = (base.FieldValue["docdate"] != DBNull.Value) ? DateTime.Parse(base.FieldValue["docdate"].ToString() ?? DateTime.MinValue.ToString()) : DateTime.MinValue;
                Коментар = base.FieldValue["col_a1"].ToString() ?? "";
                
                BaseClear();
                
                if (readAllTablePart)
                {
                    
                    await Накопичення_TablePart.Read();
                }
                
                UnigueIDChanged?.Invoke(this, base.UnigueID);
                CaptionChanged?.Invoke(this, string.Join(", ", [Назва, ]));
                return true;
            }
            else
                return false;
        }
        
        public async ValueTask<bool> Save()
        {
            
                await НовийДок_Triggers.BeforeSave(this);
            base.FieldValue["docname"] = Назва;
            base.FieldValue["docnomer"] = НомерДок;
            base.FieldValue["docdate"] = ДатаДок;
            base.FieldValue["col_a1"] = Коментар;
            
            bool result = await BaseSave();
            if (result)
            {
                
            }
            CaptionChanged?.Invoke(this, string.Join(", ", [Назва, ]));
            return result;
        }

        public async ValueTask<bool> SpendTheDocument(DateTime spendDate)
        {
            
            await BaseAddIgnoreDocumentList();
            bool spend = await НовийДок_SpendTheDocument.Spend(this);
            if (!spend) ClearRegAccum();
            await BaseSpend(spend, spend ? spendDate : DateTime.MinValue);
            await BaseRemoveIgnoreDocumentList();
            return spend;
                
        }

        
        /* Очищення регістрів накопичення */
        async void ClearRegAccum()
        {
          
            if(!this.UnigueID.IsEmpty())
            {
              
                await new РегістриНакопичення.РегНакопичення_RecordsSet().Delete(this.UnigueID.UGuid);
              
            }
            
        }
  

        public async ValueTask ClearSpendTheDocument()
        {
            ClearRegAccum();
            
            await BaseSpend(false, DateTime.MinValue);
        }

        public async ValueTask<НовийДок_Objest> Copy(bool copyTableParts = false)
        {
            НовийДок_Objest copy = new НовийДок_Objest()
            {
                Назва = Назва,
                НомерДок = НомерДок,
                ДатаДок = ДатаДок,
                Коментар = Коментар,
                
            };
            
            if (copyTableParts)
            {
            
                //Накопичення - Таблична частина
                await Накопичення_TablePart.Read();
                copy.Накопичення_TablePart.Records = Накопичення_TablePart.Copy();
            
            }
            

            await copy.New();
            
                await НовийДок_Triggers.Copying(copy, this);      
            
            return copy;
        }

        public async ValueTask SetDeletionLabel(bool label = true)
        {
            
            await ClearSpendTheDocument();
            await base.BaseDeletionLabel(label);
        }

        public async ValueTask Delete()
        {
            
            await ClearSpendTheDocument();
            await base.BaseDelete(["tab_a03", ]);
        }
        
        public НовийДок_Pointer GetDocumentPointer()
        {
            return new НовийДок_Pointer(UnigueID.UGuid);
        }

        public async ValueTask<string> GetPresentation()
        {
            return await base.BasePresentation(НовийДок_Const.PRESENTATION_FIELDS);
        }
        
        public string Назва { get; set; } = "";
        public string НомерДок { get; set; } = "";
        public DateTime ДатаДок { get; set; } = DateTime.MinValue;
        public string Коментар { get; set; } = "";
        
        //Табличні частини
        public НовийДок_Накопичення_TablePart Накопичення_TablePart { get; set; }
        
    }
    
    public class НовийДок_Pointer : DocumentPointer
    {
        public НовийДок_Pointer(object? uid = null) : base(Config.Kernel, "tab_a01", НовийДок_Const.TYPE)
        {
            base.Init(new UnigueID(uid));
        }
        
        public НовийДок_Pointer(UnigueID uid, Dictionary<string, object>? fields = null) : base(Config.Kernel, "tab_a01", "НовийДок")
        {
            base.Init(uid, fields);
        }

        public string Назва
        {
            get { return Name; } set { Name = value; }
        }

        public async ValueTask<string> GetPresentation()
        {
            return Name = await base.BasePresentation(НовийДок_Const.PRESENTATION_FIELDS);
        }

        public static void GetJoin(Query querySelect, string joinField, string parentTable, string joinTableAlias, string fieldAlias)
        {
            string[] presentationField = new string [НовийДок_Const.PRESENTATION_FIELDS.Length];
            for (int i = 0; i < presentationField.Length; i++) presentationField[i] = $"{joinTableAlias}.{НовийДок_Const.PRESENTATION_FIELDS[i]}";
            querySelect.Joins.Add(new Join(НовийДок_Const.TABLE, joinField, parentTable, joinTableAlias));
            querySelect.FieldAndAlias.Add(new ValueName<string>(presentationField.Length switch { 1 => presentationField[0], >1 => $"concat_ws (', ', " + string.Join(", ", presentationField) + ")", _ => "'#'" }, fieldAlias));
        }

        public async ValueTask<bool?> IsSpend()
        {
            return await base.BaseIsSpend();
        }

        public async ValueTask<(bool? Spend, DateTime SpendDate)> GetSpend()
        {
            return await base.BaseGetSpend();
        }

        public async ValueTask<bool> SpendTheDocument(DateTime spendDate)
        {
            НовийДок_Objest? obj = await GetDocumentObject();
            return obj != null && await obj.SpendTheDocument(spendDate);
        }

        public async ValueTask ClearSpendTheDocument()
        {
            
            ClearRegAccum();
            await BaseSpend(false, DateTime.MinValue);
                
        }

        public async ValueTask<bool?> GetDeletionLabel()
        {
            return await base.BaseGetDeletionLabel();
        }

        
        /* Очищення регістрів накопичення */
        async void ClearRegAccum()
        {
          
            if(!this.UnigueID.IsEmpty())
            {
              
                await new РегістриНакопичення.РегНакопичення_RecordsSet().Delete(this.UnigueID.UGuid);
              
            }
            
        }
  

        public async ValueTask SetDeletionLabel(bool label = true)
        {
          
            if (label)
            {
                ClearRegAccum();
                await BaseSpend(false, DateTime.MinValue);
            }
            
          await base.BaseDeletionLabel(label);
        }

        public НовийДок_Pointer Copy()
        {
            return new НовийДок_Pointer(base.UnigueID, base.Fields) { Name = Name };
        }

        public НовийДок_Pointer GetEmptyPointer()
        {
            return new НовийДок_Pointer();
        }

        public async ValueTask<НовийДок_Objest?> GetDocumentObject(bool readAllTablePart = false)
        {
            if (this.IsEmpty()) return null;
            НовийДок_Objest obj = new НовийДок_Objest();
            return await obj.Read(base.UnigueID, readAllTablePart) ? obj : null;
        }
    }

    public class НовийДок_Select : DocumentSelect
    {		
        public НовийДок_Select() : base(Config.Kernel, "tab_a01") { }
        public async ValueTask<bool> Select() { return await base.BaseSelect(); }
        public async ValueTask<bool> SelectSingle() { if (await base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        public bool MoveNext() { if (base.MoveToPosition() && base.CurrentPointerPosition.HasValue) { Current = new НовийДок_Pointer(base.CurrentPointerPosition.Value.UnigueID, base.CurrentPointerPosition.Value.Fields); return true; } else { Current = null; return false; } }
        public НовийДок_Pointer? Current { get; private set; }

        public async ValueTask<НовийДок_Pointer> FindByField(string name, object value)
        {
            UnigueID? pointer = await base.BaseFindByField(name, value);
            return pointer != null ? new НовийДок_Pointer(pointer) : new НовийДок_Pointer();
        }
        
        public async ValueTask<List<НовийДок_Pointer>> FindListByField(string name, object value, int limit = 0, int offset = 0)
        {
            List<НовийДок_Pointer> documentPointerList = [];
            foreach (var documentPointer in await base.BaseFindListByField(name, value, limit, offset)) 
                documentPointerList.Add(new НовийДок_Pointer(documentPointer.UnigueID, documentPointer.Fields));
            return documentPointerList;
        }
    }

      
    
    public class НовийДок_Накопичення_TablePart : DocumentTablePart
    {
        public НовийДок_Накопичення_TablePart(НовийДок_Objest owner) : base(Config.Kernel, "tab_a03",
             ["col_a3", "col_a4", ])
        {
            if (owner == null) throw new Exception("owner null");
            Owner = owner;
            
        }

        public const string TABLE = "tab_a03";
        
        public const string Користувач = "col_a3";
        public const string Сума = "col_a4";

        public НовийДок_Objest Owner { get; private set; }
        
        public List<Record> Records { get; set; } = [];

        public event EventHandler? Saved;
        
        public void FillJoin(string[]? orderFields = null)
        {
            QuerySelect.Clear();

            if (orderFields != null)
            {
              foreach(string field in orderFields)
                QuerySelect.Order.Add(field, SelectOrder.ASC);
            }
            Довідники.Користувачі_Pointer.GetJoin(QuerySelect, Користувач, "tab_a03", "join_tab_1", "Користувач");
                
        }

        public async ValueTask Read()
        {
            Records.Clear();
            await base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Record record = new Record()
                {
                    UID = (Guid)fieldValue["uid"],
                    Користувач = new Довідники.Користувачі_Pointer(fieldValue["col_a3"]),
                    Сума = (fieldValue["col_a4"] != DBNull.Value) ? (decimal)fieldValue["col_a4"] : 0,
                    
                };
                Records.Add(record);
                
                if (JoinValue.TryGetValue(record.UID.ToString(), out var ItemValue))
                {
                  record.JoinItemValue = ItemValue;
                  record.Користувач.Name = ItemValue["Користувач"];
                      
                }
                
            }
            
            base.BaseClear();
        }
        
        public async ValueTask Save(bool clear_all_before_save) 
        {
            if (!await base.IsExistOwner(Owner.UnigueID, "tab_a01"))
                throw new Exception("Owner not exist");
            

            await base.BaseBeginTransaction();
            
            if (clear_all_before_save)
                await base.BaseDelete(Owner.UnigueID);
            

            foreach (Record record in Records)
            {
                
                Dictionary<string, object> fieldValue = new Dictionary<string, object>()
                {
                    {"col_a3", record.Користувач.UnigueID.UGuid},
                    {"col_a4", record.Сума},
                    
                };
                record.UID = await base.BaseSave(record.UID, Owner.UnigueID, fieldValue);
            }
            
            await base.BaseCommitTransaction();

            
            Saved?.Invoke(this, new EventArgs());
        }
        
        public List<Record> Copy()
        {
            List<Record> copyRecords = new(Records);
            foreach (Record copyRecordItem in copyRecords)
                copyRecordItem.UID = Guid.Empty;

            return copyRecords;
        }

        public class Record : DocumentTablePartRecord
        {
            public Довідники.Користувачі_Pointer Користувач { get; set; } = new Довідники.Користувачі_Pointer();
            public decimal Сума { get; set; } = 0;
            
        }
    }
      
    
    public static class НовийДок_Export
    {
        public static async ValueTask ToXmlFile(НовийДок_Pointer НовийДок, string pathToSave)
        {
        НовийДок_Objest? obj = await НовийДок.GetDocumentObject(true);
            if (obj == null) return;

            XmlWriter xmlWriter = XmlWriter.Create(pathToSave, new XmlWriterSettings() { Indent = true, Encoding = System.Text.Encoding.UTF8 });
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Document");
            xmlWriter.WriteAttributeString("uid", obj.UnigueID.ToString());
            
            xmlWriter.WriteStartElement("Назва");
            xmlWriter.WriteAttributeString("type", "string");
            
                xmlWriter.WriteCData(obj.Назва);
              
            xmlWriter.WriteEndElement(); //Назва
            xmlWriter.WriteStartElement("НомерДок");
            xmlWriter.WriteAttributeString("type", "string");
            
                xmlWriter.WriteCData(obj.НомерДок);
              
            xmlWriter.WriteEndElement(); //НомерДок
            xmlWriter.WriteStartElement("ДатаДок");
            xmlWriter.WriteAttributeString("type", "datetime");
            
                xmlWriter.WriteValue(obj.ДатаДок.ToString("dd.MM.yyyy HH:mm:ss"));
              
            xmlWriter.WriteEndElement(); //ДатаДок
            xmlWriter.WriteStartElement("Коментар");
            xmlWriter.WriteAttributeString("type", "string");
            
                xmlWriter.WriteCData(obj.Коментар);
              
            xmlWriter.WriteEndElement(); //Коментар
                /*  Табличні частини */
                xmlWriter.WriteStartElement("TabularParts");
                
                    xmlWriter.WriteStartElement("TablePart");
                    xmlWriter.WriteAttributeString("name", "Накопичення");

                    foreach(НовийДок_Накопичення_TablePart.Record record in obj.Накопичення_TablePart.Records)
                    {
                        xmlWriter.WriteStartElement("row");
                        xmlWriter.WriteAttributeString("uid", record.UID.ToString());
                        
                        xmlWriter.WriteStartElement("Користувач");
                        xmlWriter.WriteAttributeString("type", "pointer");
                        
                                xmlWriter.WriteAttributeString("pointer", "Довідники.Користувачі");
                                xmlWriter.WriteAttributeString("uid", record.Користувач.UnigueID.ToString());
                                xmlWriter.WriteCData(await record.Користувач.GetPresentation());
                              
                        xmlWriter.WriteEndElement(); //Користувач
                        xmlWriter.WriteStartElement("Сума");
                        xmlWriter.WriteAttributeString("type", "numeric");
                        
                            xmlWriter.WriteValue(record.Сума);
                          
                        xmlWriter.WriteEndElement(); //Сума
                        xmlWriter.WriteEndElement(); //row
                    }

                    xmlWriter.WriteEndElement(); //TablePart
                
                xmlWriter.WriteEndElement(); //TabularParts
            

            xmlWriter.WriteEndElement(); //root
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
          
        }
    }

    #endregion
    
}

namespace GeneratedCode.Журнали
{
    #region Journal
    public class JournalSelect: AccountingSoftware.JournalSelect
    {
        public JournalSelect() : base(Config.Kernel,
             ["tab_a01", ],
             ["НовийДок", ]) { }

        public async ValueTask<DocumentObject?> GetDocumentObject(bool readAllTablePart = true)
        {
            if (Current == null) return null;
            return Current.TypeDocument switch
            {
                "НовийДок" => await new Документи.НовийДок_Pointer(Current.UnigueID).GetDocumentObject(readAllTablePart),
                _ => null
            };
        }
    }
    #endregion
}

namespace GeneratedCode.РегістриВідомостей
{
    
}

namespace GeneratedCode.РегістриНакопичення
{
    public static class VirtualTablesСalculation
    {
        /* Функція повного очищення віртуальних таблиць */
        public static void ClearAll()
        {
            /*  */
        }

        /* Функція для обчислення віртуальних таблиць  */
        public static async ValueTask Execute(DateTime period, string regAccumName)
        {
            
            Dictionary<string, object> paramQuery = new Dictionary<string, object>{ { "ПеріодДеньВідбір", period } };

            switch(regAccumName)
            {
            
                case "РегНакопичення":
                {
                    byte transactionID = await Config.Kernel.DataBase.BeginTransaction();
                    
                    /* QueryBlock: Залишки */
                        
                    await Config.Kernel.DataBase.ExecuteSQL($@"DELETE FROM {РегНакопичення_Залишки_TablePart.TABLE} WHERE {РегНакопичення_Залишки_TablePart.TABLE}.{РегНакопичення_Залишки_TablePart.Період} = @ПеріодДеньВідбір", paramQuery, transactionID);
                        
                    await Config.Kernel.DataBase.ExecuteSQL($@"INSERT INTO {РегНакопичення_Залишки_TablePart.TABLE} ( uid, {РегНакопичення_Залишки_TablePart.Період}, {РегНакопичення_Залишки_TablePart.Користувач}, {РегНакопичення_Залишки_TablePart.Сума} ) SELECT uuid_generate_v4(), date_trunc('day', РегНакопичення.period::timestamp) AS Період, РегНакопичення.{РегНакопичення_Const.Користувач} AS Користувач, /* Сума */ SUM(CASE WHEN РегНакопичення.income = true THEN РегНакопичення.{РегНакопичення_Const.Сума} ELSE -РегНакопичення.{РегНакопичення_Const.Сума} END) AS Сума FROM {РегНакопичення_Const.TABLE} AS РегНакопичення WHERE date_trunc('day', РегНакопичення.period::timestamp) = @ПеріодДеньВідбір GROUP BY Період, Користувач HAVING /* Сума */ SUM(CASE WHEN РегНакопичення.income = true THEN РегНакопичення.{РегНакопичення_Const.Сума} ELSE -РегНакопичення.{РегНакопичення_Const.Сума} END) != 0", paramQuery, transactionID);
                        
                    /* QueryBlock: ЗалишкиТаОбороти */
                        
                    await Config.Kernel.DataBase.ExecuteSQL($@"DELETE FROM {РегНакопичення_ЗалишкиТаОбороти_TablePart.TABLE} WHERE {РегНакопичення_ЗалишкиТаОбороти_TablePart.TABLE}.{РегНакопичення_ЗалишкиТаОбороти_TablePart.Період} = @ПеріодДеньВідбір", paramQuery, transactionID);
                        
                    await Config.Kernel.DataBase.ExecuteSQL($@"INSERT INTO {РегНакопичення_ЗалишкиТаОбороти_TablePart.TABLE} ( uid, {РегНакопичення_ЗалишкиТаОбороти_TablePart.Період}, {РегНакопичення_ЗалишкиТаОбороти_TablePart.Користувач}, {РегНакопичення_ЗалишкиТаОбороти_TablePart.СумаПрихід}, {РегНакопичення_ЗалишкиТаОбороти_TablePart.СумаРозхід}, {РегНакопичення_ЗалишкиТаОбороти_TablePart.СумаЗалишок} ) SELECT uuid_generate_v4(), date_trunc('day', РегНакопичення.period::timestamp) AS Період, РегНакопичення.{РегНакопичення_Const.Користувач} AS Користувач, /* Сума */ SUM(CASE WHEN РегНакопичення.income = true THEN РегНакопичення.{РегНакопичення_Const.Сума} ELSE 0 END) AS СумаПрихід, SUM(CASE WHEN РегНакопичення.income = false THEN РегНакопичення.{РегНакопичення_Const.Сума} ELSE 0 END) AS СумаРозхід, SUM(CASE WHEN РегНакопичення.income = true THEN РегНакопичення.{РегНакопичення_Const.Сума} ELSE -РегНакопичення.{РегНакопичення_Const.Сума} END) AS СумаЗалишок FROM {РегНакопичення_Const.TABLE} AS РегНакопичення WHERE date_trunc('day', РегНакопичення.period::timestamp) = @ПеріодДеньВідбір GROUP BY Період, Користувач HAVING /* Сума */ SUM(CASE WHEN РегНакопичення.income = true THEN РегНакопичення.{РегНакопичення_Const.Сума} ELSE 0 END) != 0 OR SUM(CASE WHEN РегНакопичення.income = false THEN РегНакопичення.{РегНакопичення_Const.Сума} ELSE 0 END) != 0 OR SUM(CASE WHEN РегНакопичення.income = true THEN РегНакопичення.{РегНакопичення_Const.Сума} ELSE -РегНакопичення.{РегНакопичення_Const.Сума} END) != 0", paramQuery, transactionID);
                        
                    await Config.Kernel.DataBase.CommitTransaction(transactionID);
                    break;
                }
                
                    default:
                        break;
            }
            
        }

        /* Функція для обчислення підсумкових віртуальних таблиць */
        public static async ValueTask ExecuteFinalCalculation(List<string> regAccumNameList)
        {
            
            foreach (string regAccumName in regAccumNameList)
                switch(regAccumName)
                {
                
                    case "РегНакопичення":
                    {
                        byte transactionID = await Config.Kernel.DataBase.BeginTransaction();
                        
                        /* QueryBlock: Підсумки */
                            
                        await Config.Kernel.DataBase.ExecuteSQL($@"DELETE FROM {РегНакопичення_Підсумки_TablePart.TABLE}", null, transactionID);
                            
                        await Config.Kernel.DataBase.ExecuteSQL($@"INSERT INTO {РегНакопичення_Підсумки_TablePart.TABLE} ( uid, {РегНакопичення_Підсумки_TablePart.Користувач}, {РегНакопичення_Підсумки_TablePart.Сума} ) SELECT uuid_generate_v4(), РегНакопичення.{РегНакопичення_Залишки_TablePart.Користувач} AS Користувач, /* Сума */ SUM(РегНакопичення.{РегНакопичення_Залишки_TablePart.Сума}) AS Сума FROM {РегНакопичення_Залишки_TablePart.TABLE} AS РегНакопичення GROUP BY Користувач HAVING /* Сума */ SUM(РегНакопичення.{РегНакопичення_Залишки_TablePart.Сума}) != 0", null, transactionID);
                            
                        await Config.Kernel.DataBase.CommitTransaction(transactionID);
                        break;
                    }
                    
                        default:
                            break;
                }
            
        }
    }

    
    #region REGISTER "РегНакопичення"
    public static class РегНакопичення_Const
    {
        public const string FULLNAME = "РегНакопичення";
        public const string TABLE = "tab_a02";
		    public static readonly string[] AllowDocumentSpendTable = ["tab_a01", ];
		    public static readonly string[] AllowDocumentSpendType = ["НовийДок", ];
        
        public const string Користувач = "col_a1";
        public const string Сума = "col_a2";
    }
	
    public class РегНакопичення_RecordsSet : RegisterAccumulationRecordsSet
    {
        public РегНакопичення_RecordsSet() : base(Config.Kernel, "tab_a02", "РегНакопичення",
             ["col_a1", "col_a2", ]) { }
		
        public List<Record> Records { get; set; } = [];
        
        public void FillJoin(string[]? orderFields = null, bool docname_required = true)
        {
            QuerySelect.Clear();

            if (orderFields!=null)
              foreach(string field in orderFields)
                QuerySelect.Order.Add(field, SelectOrder.ASC);

            Довідники.Користувачі_Pointer.GetJoin(QuerySelect, 
                  РегНакопичення_Const.Користувач, "tab_a02", "join_tab_1", "Користувач");
                

            //Назва документу
            if (docname_required)
            {
              string query_case = $"CASE WHEN join_doc_1.uid IS NOT NULL THEN join_doc_1.{Документи.НовийДок_Const.Назва} END";
              QuerySelect.FieldAndAlias.Add(new ValueName<string>(query_case, "docname"));

              int i = 0;
              foreach (string table in РегНакопичення_Const.AllowDocumentSpendTable)
                  QuerySelect.Joins.Add(new Join(table, "owner", "tab_a02", $"join_doc_{++i}"));
            }
        }

        public async ValueTask Read()
        {
            Records.Clear();
            await base.BaseRead();
            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Record record = new Record()
                {
                    UID = (Guid)fieldValue["uid"],
                    Period = DateTime.Parse(fieldValue["period"]?.ToString() ?? DateTime.MinValue.ToString()),
                    Income = (bool)fieldValue["income"],
                    Owner = (Guid)fieldValue["owner"],
                    OwnerType = fieldValue["ownertype"] != DBNull.Value ? (NameAndText)fieldValue["ownertype"] : new NameAndText(),
                    Користувач = new Довідники.Користувачі_Pointer(fieldValue["col_a1"]),
                    Сума = (fieldValue["col_a2"] != DBNull.Value) ? (decimal)fieldValue["col_a2"] : 0,
                    
                };
                Records.Add(record);
                
                if (JoinValue.TryGetValue(record.UID.ToString(), out var ItemValue))
                {
                    //record.JoinItemValue = ItemValue;
                    if (ItemValue.TryGetValue("docname", out var ownerName)) record.OwnerName = ownerName;
                    record.Користувач.Name = ItemValue["Користувач"];
                        
                }
                
            }
            base.BaseClear();
        }
        
        public async ValueTask Save(DateTime period, UuidAndText owner) 
        {
            await base.BaseBeginTransaction();
            await base.BaseSelectPeriodForOwner(owner.Uuid, period);
            await base.BaseDelete(owner.Uuid);
            foreach (Record record in Records)
            {
                record.Period = period;
                record.Owner = owner.Uuid;
                record.OwnerType = owner.GetNameAndText();
                Dictionary<string, object> fieldValue = new()
                {
                    {"col_a1", record.Користувач.UnigueID.UGuid},
                    {"col_a2", record.Сума},
                    
                };
                record.UID = await base.BaseSave(record.UID, record.Period, record.Income, record.Owner, record.OwnerType, fieldValue);
            }
            await base.BaseTrigerAdd(period, owner.Uuid);
            await base.BaseCommitTransaction();
        }

        public async ValueTask Delete(Guid owner)
        {
            await base.BaseSelectPeriodForOwner(owner);
            await base.BaseDelete(owner);
        }
        
        public class Record : RegisterAccumulationRecord
        {
            public Довідники.Користувачі_Pointer Користувач { get; set; } = new Довідники.Користувачі_Pointer();
            public decimal Сума { get; set; } = 0;
            
        }
    }
    
    
    
    public class РегНакопичення_Залишки_TablePart : RegisterAccumulationTablePart
    {
        public РегНакопичення_Залишки_TablePart() : base(Config.Kernel, "tab_a04",
              ["col_a1", "col_a2", "col_a3", ]) { }
        
        public const string TABLE = "tab_a04";
        
        public const string Період = "col_a1";
        public const string Користувач = "col_a2";
        public const string Сума = "col_a3";
        public List<Record> Records { get; set; } = [];
    
        public async ValueTask Read()
        {
            Records.Clear();
            await base.BaseRead();
            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Record record = new Record()
                {
                    UID = (Guid)fieldValue["uid"],
                    Період = (fieldValue["col_a1"] != DBNull.Value) ? DateTime.Parse(fieldValue["col_a1"].ToString() ?? DateTime.MinValue.ToString()) : DateTime.MinValue,
                    Користувач = new Довідники.Користувачі_Pointer(fieldValue["col_a2"]),
                    Сума = (fieldValue["col_a3"] != DBNull.Value) ? (decimal)fieldValue["col_a3"] : 0,
                    
                };
                Records.Add(record);
            }
            base.BaseClear();
        }
    
        public async ValueTask Save(bool clear_all_before_save /*= true*/) 
        {
            await base.BaseBeginTransaction();
            if (clear_all_before_save) await base.BaseDelete();
            foreach (Record record in Records)
            {
                Dictionary<string, object> fieldValue = new Dictionary<string, object>()
                {
                    {"col_a1", record.Період},
                    {"col_a2", record.Користувач.UnigueID.UGuid},
                    {"col_a3", record.Сума},
                    
                };
                record.UID = await base.BaseSave(record.UID, fieldValue);
            }
            await base.BaseCommitTransaction();
        }
        
        public async ValueTask Remove(Record record)
        {
            await base.BaseRemove(record.UID);
            Records.RemoveAll((Record item) => record.UID == item.UID);
        }

        public async ValueTask RemoveAll(List<Record> records)
        {
            List<Guid> removeList = [];

            await base.BaseBeginTransaction();
            foreach (Record record in records)
            {
                removeList.Add(record.UID);
                await base.BaseRemove(record.UID);
            }
            await base.BaseCommitTransaction();

            Records.RemoveAll((Record item) => removeList.Exists((Guid uid) => uid == item.UID));
        }
    
        public async ValueTask Delete()
        {
            await base.BaseDelete();
        }
        
        public class Record : RegisterAccumulationTablePartRecord
        {
            public DateTime Період { get; set; } = DateTime.MinValue;
            public Довідники.Користувачі_Pointer Користувач { get; set; } = new Довідники.Користувачі_Pointer();
            public decimal Сума { get; set; } = 0;
            
        }            
    }
    
    
    public class РегНакопичення_ЗалишкиТаОбороти_TablePart : RegisterAccumulationTablePart
    {
        public РегНакопичення_ЗалишкиТаОбороти_TablePart() : base(Config.Kernel, "tab_a05",
              ["col_a1", "col_a2", "col_a3", "col_a4", "col_a5", ]) { }
        
        public const string TABLE = "tab_a05";
        
        public const string Період = "col_a1";
        public const string Користувач = "col_a2";
        public const string СумаПрихід = "col_a3";
        public const string СумаРозхід = "col_a4";
        public const string СумаЗалишок = "col_a5";
        public List<Record> Records { get; set; } = [];
    
        public async ValueTask Read()
        {
            Records.Clear();
            await base.BaseRead();
            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Record record = new Record()
                {
                    UID = (Guid)fieldValue["uid"],
                    Період = (fieldValue["col_a1"] != DBNull.Value) ? DateTime.Parse(fieldValue["col_a1"].ToString() ?? DateTime.MinValue.ToString()) : DateTime.MinValue,
                    Користувач = new Довідники.Користувачі_Pointer(fieldValue["col_a2"]),
                    СумаПрихід = (fieldValue["col_a3"] != DBNull.Value) ? (decimal)fieldValue["col_a3"] : 0,
                    СумаРозхід = (fieldValue["col_a4"] != DBNull.Value) ? (decimal)fieldValue["col_a4"] : 0,
                    СумаЗалишок = (fieldValue["col_a5"] != DBNull.Value) ? (decimal)fieldValue["col_a5"] : 0,
                    
                };
                Records.Add(record);
            }
            base.BaseClear();
        }
    
        public async ValueTask Save(bool clear_all_before_save /*= true*/) 
        {
            await base.BaseBeginTransaction();
            if (clear_all_before_save) await base.BaseDelete();
            foreach (Record record in Records)
            {
                Dictionary<string, object> fieldValue = new Dictionary<string, object>()
                {
                    {"col_a1", record.Період},
                    {"col_a2", record.Користувач.UnigueID.UGuid},
                    {"col_a3", record.СумаПрихід},
                    {"col_a4", record.СумаРозхід},
                    {"col_a5", record.СумаЗалишок},
                    
                };
                record.UID = await base.BaseSave(record.UID, fieldValue);
            }
            await base.BaseCommitTransaction();
        }
        
        public async ValueTask Remove(Record record)
        {
            await base.BaseRemove(record.UID);
            Records.RemoveAll((Record item) => record.UID == item.UID);
        }

        public async ValueTask RemoveAll(List<Record> records)
        {
            List<Guid> removeList = [];

            await base.BaseBeginTransaction();
            foreach (Record record in records)
            {
                removeList.Add(record.UID);
                await base.BaseRemove(record.UID);
            }
            await base.BaseCommitTransaction();

            Records.RemoveAll((Record item) => removeList.Exists((Guid uid) => uid == item.UID));
        }
    
        public async ValueTask Delete()
        {
            await base.BaseDelete();
        }
        
        public class Record : RegisterAccumulationTablePartRecord
        {
            public DateTime Період { get; set; } = DateTime.MinValue;
            public Довідники.Користувачі_Pointer Користувач { get; set; } = new Довідники.Користувачі_Pointer();
            public decimal СумаПрихід { get; set; } = 0;
            public decimal СумаРозхід { get; set; } = 0;
            public decimal СумаЗалишок { get; set; } = 0;
            
        }            
    }
    
    
    public class РегНакопичення_Підсумки_TablePart : RegisterAccumulationTablePart
    {
        public РегНакопичення_Підсумки_TablePart() : base(Config.Kernel, "tab_a06",
              ["col_a1", "col_a2", ]) { }
        
        public const string TABLE = "tab_a06";
        
        public const string Користувач = "col_a1";
        public const string Сума = "col_a2";
        public List<Record> Records { get; set; } = [];
    
        public async ValueTask Read()
        {
            Records.Clear();
            await base.BaseRead();
            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Record record = new Record()
                {
                    UID = (Guid)fieldValue["uid"],
                    Користувач = new Довідники.Користувачі_Pointer(fieldValue["col_a1"]),
                    Сума = (fieldValue["col_a2"] != DBNull.Value) ? (decimal)fieldValue["col_a2"] : 0,
                    
                };
                Records.Add(record);
            }
            base.BaseClear();
        }
    
        public async ValueTask Save(bool clear_all_before_save /*= true*/) 
        {
            await base.BaseBeginTransaction();
            if (clear_all_before_save) await base.BaseDelete();
            foreach (Record record in Records)
            {
                Dictionary<string, object> fieldValue = new Dictionary<string, object>()
                {
                    {"col_a1", record.Користувач.UnigueID.UGuid},
                    {"col_a2", record.Сума},
                    
                };
                record.UID = await base.BaseSave(record.UID, fieldValue);
            }
            await base.BaseCommitTransaction();
        }
        
        public async ValueTask Remove(Record record)
        {
            await base.BaseRemove(record.UID);
            Records.RemoveAll((Record item) => record.UID == item.UID);
        }

        public async ValueTask RemoveAll(List<Record> records)
        {
            List<Guid> removeList = [];

            await base.BaseBeginTransaction();
            foreach (Record record in records)
            {
                removeList.Add(record.UID);
                await base.BaseRemove(record.UID);
            }
            await base.BaseCommitTransaction();

            Records.RemoveAll((Record item) => removeList.Exists((Guid uid) => uid == item.UID));
        }
    
        public async ValueTask Delete()
        {
            await base.BaseDelete();
        }
        
        public class Record : RegisterAccumulationTablePartRecord
        {
            public Довідники.Користувачі_Pointer Користувач { get; set; } = new Довідники.Користувачі_Pointer();
            public decimal Сума { get; set; } = 0;
            
        }            
    }
    
    #endregion
  
}
  