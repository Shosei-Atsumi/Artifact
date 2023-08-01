using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixedLengthFileParser.Utility
{
    class ParserUtility
    {
        public struct Block
        {
            public string name;
            public int length;
            public int startLength;
            public string format;
        }

        private Block CreateBlock(int length)
        {
            var block = new Block();
            block.name = string.Empty;
            block.length = length;
            block.startLength = 0;
            block.format = string.Empty;
            return block;
        }

        private Block CreateBlock(int length, string name)
        {
            var block = new Block();
            block.name = name;
            block.length = length;
            block.startLength = 0;
            block.format = string.Empty;
            return block;
        }

        private Block CreateBlock(int length, string name, int startLength)
        {
            var block = new Block();
            block.name = name;
            block.length = length;
            block.startLength = startLength;
            block.format = string.Empty;
            return block;
        }

        private Block CreateBlock(int length, string name, int startLength, string format)
        {
            var block = new Block();
            block.name = name;
            block.length = length;
            block.startLength = startLength;
            block.format = format;
            return block;
        }

        private Block CreateBlock(int length, string name, string format)
        {
            var block = new Block();
            block.name = name;
            block.length = length;
            block.startLength = 0;
            block.format = format;
            return block;
        }

        private List<Block> _fileFormat = new List<Block>();

        public List<Block> fileFormat
        {
            get { return _fileFormat; }
            set { _fileFormat = value; }
        }

        private void FileFormatClear()
        {
            fileFormat = new List<Block>();
        }

        public bool LoadFileFormat(List<int> lengths)
        {
            FileFormatClear();
            bool res = true;

            foreach(var length in lengths)
            {
                fileFormat.Add(CreateBlock(length));
            }

            return res;
        }

        public bool LoadFileFormat(List<(int,string)> Blocks)
        {
            FileFormatClear();
            bool res = true;

            foreach (var block in Blocks)
            {
                fileFormat.Add(CreateBlock(block.Item1, block.Item2));
            }

            return res;
        }

        public bool LoadFileFormat(List<(int, string, int)> Blocks)
        {
            FileFormatClear();
            bool res = true;

            foreach (var block in Blocks)
            {
                fileFormat.Add(CreateBlock(block.Item1, block.Item2, block.Item3));
            }

            return res;
        }

        public bool LoadFileFormat(List<(int, string, int, string)> Blocks)
        {
            FileFormatClear();
            bool res = true;

            foreach (var block in Blocks)
            {
                fileFormat.Add(CreateBlock(block.Item1, block.Item2, block.Item3, block.Item4));
            }

            return res;
        }


    }
}
