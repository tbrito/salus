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
 * Based on GermanStemFilter
 *
 */
namespace Salus.Infra.Search.Analyzers
{
    using System.Collections.Generic;
    using Lucene.Net.Analysis;
    using Lucene.Net.Analysis.Tokenattributes;

    public sealed class BrazilianStemFilterCustom : TokenFilter
    {
        /*
         * The actual token in the input stream.
         */
        private readonly BrazilianStemmerCustom stemmer;
        private readonly ISet<string> exclusions;
        private readonly ITermAttribute termAtt;

        public BrazilianStemFilterCustom(TokenStream input)
            : base(input)
        {
            this.stemmer = new BrazilianStemmerCustom();
            this.termAtt = this.AddAttribute<ITermAttribute>();
        }

        public BrazilianStemFilterCustom(TokenStream input, ISet<string> exclusiontable)
            : this(input)
        {
            this.exclusions = exclusiontable;
        }

        /*
         * <returns>Returns the next token in the stream, or null at EOS.</returns>
         */
        public override bool IncrementToken()
        {
            if (this.input.IncrementToken())
            {
                string term = this.termAtt.Term;

                // Check the exclusion table.
                if (this.exclusions == null || !this.exclusions.Contains(term))
                {
                    string s = this.stemmer.Stem(term);
                    
                    // If not stemmed, don't waste the time adjusting the token.
                    if ((s != null) && !s.Equals(term))
                    {
                        this.termAtt.SetTermBuffer(s);
                    }
                }

                return true;
            }
            
            return false;
        }
    }
}