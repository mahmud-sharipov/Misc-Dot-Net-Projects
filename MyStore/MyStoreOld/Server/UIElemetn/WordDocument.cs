using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Office.Interop.Word;
using Server.Models;

namespace Server.UIElemetn
{
    public class WordDocument
    {
        _Application application;
        _Document document = null;
        Object missingObj = System.Reflection.Missing.Value;
        object oMissing = System.Reflection.Missing.Value;
        Object trueObj = true;
        Object falseObj = false;
        public WordDocument(string path)
        {
            application = new Microsoft.Office.Interop.Word.Application();

            object templatePathObj = Directory.GetCurrentDirectory() + "/" + path;
            try
            {
                document = application.Documents.Add(ref templatePathObj, ref missingObj, ref missingObj, ref missingObj);
            }
            catch (Exception error)
            {
                CloseDocument();
                MessageBox.Show(error.Message);
            }
        }

        public void ReportProductAmountPrice(List<Product> listProduct, string SaveIn = "")
        {
            try
            {
                Table table = document.Tables[1];
                int rowCount = 2;
                var resultSet = from foobars in listProduct
                                orderby foobars.Type.FullName, foobars.Number
                                select foobars;

                foreach (var item in resultSet)
                {
                    var newRow = table.Rows.Add(ref oMissing);
                    newRow.Range.Font.Bold = 0;
                    newRow.Cells[1].Range.Text = item.Number.ToString();
                    newRow.Cells[2].Range.Text = item.Type.FullName;
                    newRow.Cells[3].Range.Text = item.Name;
                    newRow.Cells[4].Range.Text = item.Code;
                    newRow.Cells[5].Range.Text = item.Amount.ToString();
                    newRow.Cells[6].Range.Text = item.Price.ToString();
                }
                object paramMissing = Type.Missing;
                object filenameOut = SaveIn + $@"\Наименования и цена товаров-{GetDateMark()}.docx";
              document.SaveAs(ref filenameOut,
              ref paramMissing, ref paramMissing, ref paramMissing, ref paramMissing,
              ref paramMissing, ref paramMissing, ref paramMissing, ref paramMissing,
              ref paramMissing, ref paramMissing, ref paramMissing, ref paramMissing,
              ref paramMissing, ref paramMissing);
                CloseDocument();
            }
            catch (Exception error)
            {
                CloseDocument();
                MessageBox.Show(error.Message);
            }
        }

        public void ReportSypplyReport(List<SupplyReport> listReport, string SaveIn = "")
        {
            try
            {
                Table table = document.Tables[1];
                int rowCount = 2;
                var resultSet = from foobars in listReport
                                orderby foobars.Product.Type.FullName, foobars.Product.Number
                                select foobars;

                foreach (var item in resultSet)
                {
                    var newRow = table.Rows.Add(ref oMissing);
                    newRow.Range.Font.Bold = 0;
                    newRow.Cells[1].Range.Text = item.Product.Number.ToString();
                    newRow.Cells[2].Range.Text = item.Product.Type.FullName;
                    newRow.Cells[3].Range.Text = item.Product.Name;
                    newRow.Cells[4].Range.Text = item.Product.Code;
                    newRow.Cells[5].Range.Text = item.Amount.ToString();
                    newRow.Cells[6].Range.Text = item.TotalPrice.ToString();
                }
                object paramMissing = Type.Missing;
                object filenameOut = SaveIn + $@"\Отчет по поставкам-{GetDateMark()}.docx";
                document.SaveAs(ref filenameOut,
              ref paramMissing, ref paramMissing, ref paramMissing, ref paramMissing,
              ref paramMissing, ref paramMissing, ref paramMissing, ref paramMissing,
              ref paramMissing, ref paramMissing, ref paramMissing, ref paramMissing,
              ref paramMissing, ref paramMissing);
                CloseDocument();
            }
            catch (Exception error)
            {
                CloseDocument();
                MessageBox.Show(error.Message);
            }
        }

        public void ReportInvoiceReport(List<InvoiceReport> listReport, string SaveIn = "")
        {
            try
            {
                Table table = document.Tables[1];
                int rowCount = 2;
                var resultSet = from foobars in listReport
                                orderby foobars.Product.Type.FullName, foobars.Product.Number
                                select foobars;

                foreach (var item in resultSet)
                {
                    var newRow = table.Rows.Add(ref oMissing);
                    newRow.Range.Font.Bold = 0;
                    newRow.Cells[1].Range.Text = item.Product.Number.ToString();
                    newRow.Cells[2].Range.Text = item.Product.Type.FullName;
                    newRow.Cells[3].Range.Text = item.Product.Name;
                    newRow.Cells[4].Range.Text = item.Product.Code;
                    newRow.Cells[5].Range.Text = item.Amount.ToString();
                    newRow.Cells[6].Range.Text = item.TotalPrice.ToString();
                }
                object paramMissing = Type.Missing;
                object filenameOut = SaveIn + $@"\Отчет по продажам(Общее)-{GetDateMark()}.docx";
                document.SaveAs(ref filenameOut,
              ref paramMissing, ref paramMissing, ref paramMissing, ref paramMissing,
              ref paramMissing, ref paramMissing, ref paramMissing, ref paramMissing,
              ref paramMissing, ref paramMissing, ref paramMissing, ref paramMissing,
              ref paramMissing, ref paramMissing);
                CloseDocument();
            }
            catch (Exception error)
            {
                CloseDocument();
                MessageBox.Show(error.Message);
            }
        }

        public void ReportEachInvoise(List<Invoice> listReport, string SaveIn = "")
        {
            try
            {
                Table table = document.Tables[1];
                int rowCount = 2;
                var resultSet = from foobars in listReport
                                orderby foobars.Product.Type.FullName, foobars.Product.Number
                                select foobars;

                foreach (var item in resultSet)
                {
                    var newRow = table.Rows.Add(ref oMissing);
                    newRow.Range.Font.Bold = 0;
                    newRow.Cells[1].Range.Text = item.Product.Number.ToString();
                    newRow.Cells[2].Range.Text = item.Product.Type.FullName;
                    newRow.Cells[3].Range.Text = item.Product.Name;
                    newRow.Cells[4].Range.Text = item.Product.Code;
                    newRow.Cells[5].Range.Text = item.Product.Price.ToString();
                    newRow.Cells[6].Range.Text = item.Amount.ToString();
                    newRow.Cells[7].Range.Text = item.TotalPrice.ToString();
                }
                object paramMissing = Type.Missing;
                object filenameOut = SaveIn + $@"\Отчет по продажам-{GetDateMark()}.docx";
                document.SaveAs(ref filenameOut,
              ref paramMissing, ref paramMissing, ref paramMissing, ref paramMissing,
              ref paramMissing, ref paramMissing, ref paramMissing, ref paramMissing,
              ref paramMissing, ref paramMissing, ref paramMissing, ref paramMissing,
              ref paramMissing, ref paramMissing);
                CloseDocument();
            }
            catch (Exception error)
            {
                CloseDocument();
                MessageBox.Show(error.Message);
            }
        }

        public void CloseDocument()
        {
            document.Close(ref falseObj, ref missingObj, ref missingObj);
            application.Quit(ref missingObj, ref missingObj, ref missingObj);
            document = null;
            application = null;
        }

        protected string GetDateMark() => $"{DateTime.Now.Day}{DateTime.Now.Month}{DateTime.Now.Year} {DateTime.Now.Hour}{DateTime.Now.Minute}{DateTime.Now.Second}";
    }
}
