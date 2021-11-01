using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjaSystem
{
    public class ExcelController
    {
        public List<PostModel> getPostExcel(string path, bool hasHeader = true)
        {
            List<PostModel> list_baiviet = new List<PostModel>();
            using (var pck = new OfficeOpenXml.ExcelPackage(new FileInfo(path)))
            {
                
                var ws = pck.Workbook.Worksheets.First();

                int totalRow=ws.Dimension.End.Row;

                for (int rowNum = 2; rowNum <= totalRow; rowNum++)
                {
                    var row = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                    PostModel bv = new PostModel();

                    bv.message = row[rowNum, 1].Text.Trim();
                    List<string> list_pic = new List<string>();
                    string linkpic= row[rowNum, 2].Text.Trim();
                    if(linkpic.Contains("\n"))
                    {
                        string[] arr = linkpic.Split('\n');
                        foreach(string link in arr)
                        {
                            if(string.IsNullOrEmpty(link.Trim())==false)
                            {
                                list_pic.Add(link.Trim());
                            }
                            
                        } 
                    }
                    else
                    {
                        list_pic.Add(linkpic.Trim());
                    }
                    bv.list_path = list_pic;
                    list_baiviet.Add(bv);
                  
                }
                return list_baiviet;
            }
        }
    }
}
