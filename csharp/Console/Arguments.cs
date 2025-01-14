﻿using System;
using System.Collections.Generic;
using CommandLine;
using OpenSvip.Framework;

namespace OpenSvip.Console
{
    [Verb("convert", true, HelpText = "进行格式转换。")]
    public class ConvertOptions
    {
        [Option('i', "input-type", Required = true,
            HelpText = "输入文件格式（json 或 svip）", MetaValue = "FORMAT")]
        public string InType { get; set; }

        [Option('o', "output-type", Required = true,
            HelpText = "输出文件格式（json 或 svip）", MetaValue = "FORMAT")]
        public string OutType { get; set; }

        [Value(0, Required = true, HelpText = "输入文件路径", MetaValue = "FILE")]
        public string InPath { get; set; }

        [Value(1, Required = true, HelpText = "输出文件路径", MetaValue = "FILE")]
        public string OutPath { get; set; }

        [Option("input-options", Separator = ';', Required = false,
            HelpText = "输入转换选项，格式为 [选项名]=[选项值]。多个选项之间以 \";\" 分隔。")]
        public IEnumerable<string> _inputOptions { private get; set; } = Array.Empty<string>();

        [Option("output-options", Separator = ';', Required = false,
            HelpText = "输出转换选项，格式为 [选项名]=[选项值]。多个选项之间以 \";\" 分隔。")]
        public IEnumerable<string> _outputOptions { get; set; } = Array.Empty<string>();

        public ConverterOptions InputOptions => new ConverterOptions(OptionsToDictionary(_inputOptions));

        public ConverterOptions OutputOptions => new ConverterOptions(OptionsToDictionary(_outputOptions));

        private static Dictionary<string, string> OptionsToDictionary(IEnumerable<string> options)
        {
            var dict = new Dictionary<string, string>();
            foreach (var option in options)
            {
                var kv = option.Split(new[] {'='}, 2);
                if (kv.Length < 2)
                {
                    throw new ArgumentException("给定的转换选项格式不合法：应为 [选项名]=[选项值]。");
                }
                dict[kv[0]] = kv[1];
            }
            return dict;
        }
    }

    [Verb("plugins", HelpText = "显示当前已安装的所有插件。")]
    class PluginsOptions
    {
        
    }
}
