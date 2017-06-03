﻿using System;
using System.Collections.Generic;
using System.Linq;
using GameCloud.Manager.PluginContract.Requests;
using GameCloud.Manager.PluginContract.Responses;
using Microsoft.AspNetCore.Mvc;

namespace GameCloud.Manager.App.ApiControllers
{
    public class DemoController : Controller
    {
        private static DemoPluginSettings GlobalSettings;
        private static readonly List<DemoPluginRawData> SampleDatas = ParallelEnumerable.Range(0, 1000)
                    .Select(i => new DemoPluginRawData() { Id = i, Name = "demo data " + i.ToString(), Type = (DemoEnumType)(i % 3) })
                    .ToList();

        [HttpPost]
        [Route("api/demo/table")]
        public PluginPaginationResponse<DemoPluginRawData> GetDataForDemoList([FromBody]SearchRequestInfo<DemoPluginRawData> request)
        {
            if (request.Method == PluginRequestMethod.Update)
            {
                var updateRawData = request.RawData;
                if (updateRawData != null)
                {
                    var dbRawData = SampleDatas.FirstOrDefault(d => d.Id == updateRawData.Id);
                    if (dbRawData != null)
                    {
                        dbRawData.Name = updateRawData.Name;
                        dbRawData.Type = updateRawData.Type;
                    }

                    // update raw data here.
                }
            }
            else if (request.Method == PluginRequestMethod.Delete)
            {
                var deleteRawData = request.RawData;
                if (deleteRawData != null)
                {
                    if (SampleDatas.Any(d => d.Id == deleteRawData.Id))
                    {
                        // delete the raw data here.
                        SampleDatas.Remove(SampleDatas.First(d => d.Id == deleteRawData.Id));
                    }
                }
            }

            var keyword = request.Keyword;
            var page = request.Page;
            var pageSize = request.PageSize;

            IEnumerable<DemoPluginRawData> raws = SampleDatas;
            if (!string.IsNullOrEmpty(keyword))
            {
                raws = raws.Where(r => r.Name.Contains(keyword));
            }

            var total = raws.Count();

            raws = raws.Skip((page - 1) * pageSize).Take(pageSize);

            return new PluginPaginationResponse<DemoPluginRawData>()
            {
                Page = page,
                PageSize = pageSize,
                Raws = raws.ToList(),
                Total = total
            };
        }

        [HttpPost]
        [Route("api/demo/form")]
        public DemoPluginSettings GetDataForDemoUpdate([FromBody]DemoPluginSettings request)
        {
            if (GlobalSettings == null)
            {
                GlobalSettings = new DemoPluginSettings();
                GlobalSettings.FormData1 = "FormData1-Default";
                GlobalSettings.FormData2 = "FormData2-Default";
                GlobalSettings.TestDate = DateTime.Now;
                GlobalSettings.UpdateTime = DateTime.UtcNow;
            }

            GlobalSettings.FormData1 = request.FormData1;
            GlobalSettings.FormData2 = request.FormData2;
            GlobalSettings.TestDate = request.TestDate;
            GlobalSettings.UpdateTime = DateTime.UtcNow;

            return GlobalSettings;
        }

        [HttpPost]
        [Route("api/demo/chart")]
        public IReadOnlyList<object> GetDataForDemoReport([FromBody]PluginRequestMethod request)
        {
            var pieData = new
            {
                Labels = ParallelEnumerable.Range(0, 24).Select(i => i.ToString()).ToArray(),
                Series = new string[] { "小时数值" },
                Data = this.RandomNumbers(24),
            };
            var lineData = new
            {
                Labels = new string[] { "男", "女" },
                Data = this.RandomNumbers(2),
            };

            return new object[] { pieData, lineData };
        }

        private double[] RandomNumbers(int length)
        {
            Random random = new Random();
            return new int[length].Select(i => Math.Floor(random.NextDouble() * 10000) / 100).ToArray();
        }

    }

    public class DemoPluginRawData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DemoEnumType Type { get; set; }
    }

    public class DemoPluginSettings
    {
        public string FormData1 { get; set; }

        public string FormData2 { get; set; }

        public DateTime TestDate { get; set; }

        public DateTime UpdateTime { get; set; }
    }

    public enum DemoEnumType
    {
        Type1,
        Type2,
        Type3
    }
}
