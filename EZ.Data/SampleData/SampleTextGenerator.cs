using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EZ.Data.SampleData
{
    public class SampleTextGenerator
    {
        public string GenWords(int wordCount = 1, SourceNames sourceNames = 0)
        {
            var gen = new NLipsum.Core.LipsumGenerator(GetSource(sourceNames), false);
            return string.Join(" ", gen.GenerateWords(wordCount));
        }

        public string GenSentences(int sentenceCount = 1, SourceNames sourceNames = 0)
        {
            var gen = new NLipsum.Core.LipsumGenerator(GetSource(sourceNames), false);
            return string.Join(" ", gen.GenerateSentences(sentenceCount));
        }

        /// <summary>
        /// Original sources of text from which to generate sample text.
        /// All are out of copyright.
        /// </summary>
        public enum SourceNames
        {
            LoremIpsum,
            ChildHarold,
            TheRaven,
            Decameron,
            Faust,
        }

        protected string GetSource(SourceNames sourceName)
        {
            switch (sourceName)
            {
                case SourceNames.ChildHarold:
                    return NLipsum.Core.Lipsums.ChildHarold;
                case SourceNames.TheRaven:
                    return NLipsum.Core.Lipsums.TheRaven;
                case SourceNames.Decameron:
                    return NLipsum.Core.Lipsums.Decameron;
                case SourceNames.Faust:
                    return NLipsum.Core.Lipsums.Faust;
                default:
                    return NLipsum.Core.Lipsums.LoremIpsum;
            }
        }
    }
}
