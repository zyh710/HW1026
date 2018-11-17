using ConsoleApp1.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ConsoleApp1.Service
{
    public class ImportService
    {
        public List<OpenData> FindOpenData()
        {

            List<OpenData> result = new List<OpenData>();

            string baseDir = Directory.GetCurrentDirectory();
            var xml = XElement.Load(System.IO.Path.Combine(baseDir, "AppData/Taichung.xml"));
            var nodes = xml.Descendants("RECORD").ToList();
            result = nodes
                .Where(x => !x.IsEmpty).ToList()
                .Select(node =>
                {
                    OpenData item = new OpenData();
                    item.名稱 = getValue(node, "名稱");
                    item.地址 = getValue(node, "地址");
                    item.電話 = getValue(node, "電話");
                    item.傳真 = getValue(node, "傳真");
                    item.服務區域 = getValue(node, "服務區域");
                    return item;
                }).ToList();

            return result;
        }

        public void ImportToDb(List<OpenData> openDatas)
        {
            Repository.OpenDataRepository Repository = new Repository.OpenDataRepository();
            openDatas.ForEach(item => {
                Repository.Insert(item);
            });
        }

        private string getValue(XElement node, string header)
        {
            return node.Element(header)?.Value?.Trim();
        }
    }
}
