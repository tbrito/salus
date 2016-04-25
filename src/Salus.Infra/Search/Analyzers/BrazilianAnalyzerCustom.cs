/*
 * Licensed to the Apache Software Foundation (ASF) under one or more
 * contributor license agreements.  See the NOTICE file distributed with
 * this work for additional information regarding copyright ownership.
 * The ASF licenses this file to You under the Apache License, Version 2.0
 * (the "License"); you may not use this file except in compliance with
 * the License.  You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

/*
 * Analyzer for Brazilian language. Supports an external list of stopwords (words that
 * will not be indexed at all) and an external list of exclusions (word that will
 * not be stemmed, but indexed).
 *
 */
namespace Salus.Infra.Search.Analyzers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.IO;
    using Lucene.Net.Analysis;
    using Lucene.Net.Analysis.Standard;
    using Lucene.Net.Support.Compatibility;

    public sealed class BrazilianAnalyzerCustom : Analyzer
    {
        /*
         * List of typical Brazilian stopwords.
         */
        private static readonly string[] brazilianStopWords = 
        {
            "a", "ainda", "alem", "ambas", "ambos", "antes",
            "ao", "aonde", "aos", "apos", "aquele", "aqueles",
            "as", "assim", "com", "como", "contra", "contudo",
            "cuja", "cujas", "cujo", "cujos", "da", "das", "de",
            "dela", "dele", "deles", "demais", "depois", "desde",
            "desta", "deste", "dispoe", "dispoem",
            "do", "dos", "durante", "e",
            "ela", "elas", "ele", "eles", "em", "entao", "entre",
            "essa", "essas", "esse", "esses", "esta", "estas",
            "este", "estes", "ha", "isso", "isto", "logo", "mais",
            "mas", "mediante", "menos", "mesma", "mesmas", "mesmo",
            "mesmos", "na", "nas", "nao", "nas", "nem", "nesse", "neste",
            "nos", "o", "os", "ou", "outra", "outras", "outro", "outros",
            "pelas", "pelas", "pelo", "pelos", "perante", "pois", "por",
            "porque", "portanto", "proprio", "propios", "quais", "qual",
            "qualquer", "quando", "quanto", "que", "quem", "quer", "se",
            "seja", "sem", "sendo", "seu", "seus", "sob", "sobre", "sua",
            "suas", "tal", "tambem", "teu", "teus", "toda", "todas",
            "todo",
            "todos", "tua", "tuas", "tudo", "um", "uma", "umas", "uns"
        };

        /// <summary>
        /// Contains the stopwords used with the StopFilter.
        /// </summary>
        private readonly ISet<string> stoptable = SetFactory.CreateHashSet<string>();

        private readonly Lucene.Net.Util.Version matchVersion;

        /// <summary>
        /// Contains words that should be indexed but not stemmed.
        /// </summary>
        private ISet<string> excltable = SetFactory.CreateHashSet<string>();

        public BrazilianAnalyzerCustom(Lucene.Net.Util.Version matchVersion)
            : this(matchVersion, DefaultSetHolder.DefaultStopSet)
        {
        }

        /*
           * Builds an analyzer with the given stop words
           * 
           * @param matchVersion
           *          lucene compatibility version
           * @param stopwords
           *          a stopword set
           */

        public BrazilianAnalyzerCustom(Lucene.Net.Util.Version matchVersion, ISet<string> stopwords)
        {
            this.stoptable = CharArraySet.UnmodifiableSet(CharArraySet.Copy(stopwords));
            this.matchVersion = matchVersion;
        }

        /*
         * Builds an analyzer with the given stop words and stemming exclusion words
         * 
         * @param matchVersion
         *          lucene compatibility version
         * @param stopwords
         *          a stopword set
         */

        public BrazilianAnalyzerCustom(Lucene.Net.Util.Version matchVersion, ISet<string> stopwords,
                                 ISet<string> stemExclusionSet)
            : this(matchVersion, stopwords)
        {
            this.excltable = CharArraySet.UnmodifiableSet(CharArraySet.Copy(stemExclusionSet));
        }

        /*
         * Builds an analyzer with the given stop words.
         * @deprecated use {@link #BrazilianAnalyzer(Version, Set)} instead
         */

        public BrazilianAnalyzerCustom(Lucene.Net.Util.Version matchVersion, params string[] stopwords)
            : this(matchVersion, StopFilter.MakeStopSet(stopwords))
        {
        }

        /*
        * Builds an analyzer with the given stop words. 
        * @deprecated use {@link #BrazilianAnalyzer(Version, Set)} instead
        */

        public BrazilianAnalyzerCustom(Lucene.Net.Util.Version matchVersion, IDictionary<string, string> stopwords)
            : this(matchVersion, stopwords.Keys.ToArray())
        {
        }

        /*
        * Builds an analyzer with the given stop words.
        * @deprecated use {@link #BrazilianAnalyzer(Version, Set)} instead
        */

        public BrazilianAnalyzerCustom(Lucene.Net.Util.Version matchVersion, FileInfo stopwords)
            : this(matchVersion, WordlistLoader.GetWordSet(stopwords))
        {
        }

        /*
         * Builds an exclusionlist from an array of Strings.
         * @deprecated use {@link #BrazilianAnalyzer(Version, Set, Set)} instead
         */

        /// <summary>
        /// Returns an unmodifiable instance of the default stop-words set.
        /// </summary>
        /// <returns>Returns an unmodifiable instance of the default stop-words set.</returns>
        public static ISet<string> GetDefaultStopSet()
        {
            return DefaultSetHolder.DefaultStopSet;
        }

        public void SetStemExclusionTable(params string[] exclusionlist)
        {
            this.excltable = StopFilter.MakeStopSet(exclusionlist);
            this.PreviousTokenStream = null; // force a new stemmer to be created
        }

        /*
         * Builds an exclusionlist from a {@link Map}.
         * @deprecated use {@link #BrazilianAnalyzer(Version, Set, Set)} instead
         */

        public void SetStemExclusionTable(IDictionary<string, string> exclusionlist)
        {
            this.excltable = SetFactory.CreateHashSet(exclusionlist.Keys);
            this.PreviousTokenStream = null; // force a new stemmer to be created
        }

        /*
         * Builds an exclusionlist from the words contained in the given file.
         * @deprecated use {@link #BrazilianAnalyzer(Version, Set, Set)} instead
         */

        public void SetStemExclusionTable(FileInfo exclusionlist)
        {
            this.excltable = WordlistLoader.GetWordSet(exclusionlist);
            this.PreviousTokenStream = null; // force a new stemmer to be created
        }
        
        public override TokenStream TokenStream(string fieldName, TextReader reader)
        {
            TokenStream result = new StandardTokenizer(this.matchVersion, reader);
            result = new LowerCaseFilter(result);
            result = new StandardFilter(result);
            result = new StopFilter(StopFilter.GetEnablePositionIncrementsVersionDefault(this.matchVersion), result, this.stoptable);
            result = new BrazilianStemFilterCustom(result, this.excltable);
            
            return result;
        }

        /*
         * Returns a (possibly reused) {@link TokenStream} which tokenizes all the text 
         * in the provided {@link Reader}.
         *
         * @return  A {@link TokenStream} built from a {@link StandardTokenizer} filtered with
         *          {@link LowerCaseFilter}, {@link StandardFilter}, {@link StopFilter}, and 
         *          {@link BrazilianStemFilter}.
         */

        public override TokenStream ReusableTokenStream(string fieldName, TextReader reader)
        {
            var streams = (SavedStreams)this.PreviousTokenStream;

            if (streams == null)
            {
                streams = new SavedStreams();
                streams.Source = new StandardTokenizer(this.matchVersion, reader);

                streams.Result = new LowerCaseFilter(streams.Source);
                streams.Result = new StandardFilter(streams.Result);
                streams.Result = new StopFilter(StopFilter.GetEnablePositionIncrementsVersionDefault(this.matchVersion), streams.Result, this.stoptable);
                streams.Result = new BrazilianStemFilterCustom(streams.Result, this.excltable);
                this.PreviousTokenStream = streams;
            }
            else
            {
                streams.Source.Reset(reader);
            }

            return streams.Result;
        }

        private static class DefaultSetHolder
        {
            internal static readonly ISet<string> DefaultStopSet = CharArraySet.UnmodifiableSet(new CharArraySet(brazilianStopWords, false));
        }

        private class SavedStreams
        {
            protected internal Tokenizer Source;
            protected internal TokenStream Result;
        }
    }
}