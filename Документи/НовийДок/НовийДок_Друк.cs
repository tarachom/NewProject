

/*
        НовийДок_Друк.cs
        Друк
*/

using AccountingSoftware;
using GeneratedCode.Константи;
using GeneratedCode.Документи;

using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace NewProject
{
    static class НовийДок_Друк
    {
        public static async ValueTask PDF(UnigueID unigueID)
        {
            НовийДок_Objest? Обєкт = await new НовийДок_Pointer(unigueID).GetDocumentObject();
            if (Обєкт != null)
            {
                Обєкт.Накопичення_TablePart.FillJoin([]);
                await Обєкт.Накопичення_TablePart.Read();

                //Назва та розмір колонок
                Dictionary<string, int> Columns = new()
                {
                    {"Користувач", 200 },
                    {"Сума", 50 }
                };

                QuestPDF.Settings.License = LicenseType.Community;
                Document doc = Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A4);
                        page.Margin(10, Unit.Point);

                        page.Content().Column(x =>
                        {
                            //Назва
                            x.Item().Text(Обєкт.Назва).FontSize(14).Bold();

                            x.Item().PaddingVertical(5).LineHorizontal(1);

                            x.Item().Table(table =>
                            {
                                table.ColumnsDefinition(cols =>
                                {
                                    foreach (var item in Columns.Values)
                                        cols.ConstantColumn(item);
                                });

                                table.Header(cell =>
                                {
                                    foreach (var item in Columns.Keys)
                                        cell.Cell().Border(1).Padding(1).Text(item).FontSize(8).AlignCenter();
                                });

                                decimal Сума = 0;

                                foreach (var record in Обєкт.Накопичення_TablePart.Records)
                                {
                                    table.Cell().Border(1).Padding(1).Text(record.Користувач.Назва).FontSize(8);
                                    table.Cell().Border(1).Padding(1).Text(record.Сума.ToString()).FontSize(8).AlignRight();

                                    Сума += record.Сума;
                                }

                                for (int i = 1; i < Columns.Count; i++)
                                    table.Cell().Border(0).Padding(1);

                                table.Cell().Border(1).Padding(1).Text(Сума.ToString()).FontSize(8).AlignRight();
                            });
                        });
                    });
                });

                doc.GeneratePdfAndShow();
            }
        }
    }
}
