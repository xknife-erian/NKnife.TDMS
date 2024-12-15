using NKnife.TDMSDataViewer.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKnife.TDMSDataViewer.Common
{
    static class Helper
    {
        public static DbFileStructure BuildSampleData()
        {
            var dfi = new DbFileStructure
            {
                Properties =
                [
                    new LevelProperty { Name = "Name", Value        = $"Name-{Rand(5)}" },
                    new LevelProperty { Name = "Description", Value = $"Description-{Rand(16)}" },
                    new LevelProperty { Name = "Author", Value      = $"Author-{Rand(8)}" }
                ],
            };
            var groups = BuildSampleGroup(5);

            foreach (var group in groups)
                dfi.Groups.Add(group);

            return dfi;
        }

        private static IEnumerable<Group> BuildSampleGroup(uint count)
        {
            var list = new List<Group>();

            for (uint i = 0; i < count; i++)
            {
                var group = new Group()
                {
                    Properties =
                    [
                        new LevelProperty() { Name = "Name", Value        = $"GroupName-{Rand(6)}" },
                        new LevelProperty() { Name = "Description", Value = $"GroupDesc-{Rand(3)}" },
                    ]
                };
                var channels = BuildSampleChannel(5);
                foreach (var channel in channels)
                    group.Channels.Add(channel);
                list.Add(group);
            }

            return list;
        }

        private static IEnumerable<Channel> BuildSampleChannel(uint count)
        {
            var list = new List<Channel>();

            for (uint i = 0; i < count; i++)
            {
                var channel = new Channel()
                {
                    Properties =
                    [
                        new LevelProperty() { Name = "Name", Value = $"ChannelName-{Rand(7)}" },
                        new LevelProperty() { Name = "Unit", Value = $"V" },
                    ],
                    Datas =
                    [
                        new Data(),
                        new Data()
                    ]
                };
                list.Add(channel);
            }

            return list;
        }

        private static string Rand(ushort count)
        {
            return Guid.NewGuid().ToString("N").ToUpper().Substring(0, count);
        }
    }
}